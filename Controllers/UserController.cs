using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVC.Dtos;
using Newtonsoft.Json;
using MVC.Interfaces;
using MVC.Entities;
using MVC.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersService;
        private readonly IUserUploadService _userUploadService;
        private readonly IHostingEnvironment _env;

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration, IUsersService usersService, 
            IUserUploadService userUploadService,
            IHostingEnvironment env
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _usersService = usersService;
            _userUploadService = userUploadService;
            _env = env;
        }

        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<object> Login([FromBody]LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    false);
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                    LoginResponse loginResponse = new LoginResponse { token = GenerateJwtToken(model.Email, appUser) };
                    return Ok(loginResponse); 
                }

                if (result.IsLockedOut)
                {
                    return BadRequest("Lockout");
                }
            }

            // If we got this far, something failed, redisplay form
            return BadRequest(model);
        }

        // POST: /Account/Register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody]RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok("User created a new account with password.");
                }

                AddErrors(result);
            }

            return BadRequest(model);
        }

        // POST: /Account/LogOff
        [Authorize]
        [HttpPost("Logoff")]
        
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "User logged out!" });
        }

        [Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult<UserModel>> GetUserProfile()
        {
            var model = new UserModel();
           var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest();
            }
            var image = await _usersService.GetImageAsByteStringByUserId(user.Id);
            if (image != null)
            {
                model.Image = image;
            }
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Username = user.UserName;
          
            return Ok(model);
        }

        [Authorize]
        [HttpPut("ChangeUserProfile")]
        public async Task<ActionResult<UserModel>> ChangeUserProfile([FromForm]ChangeProfileDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                var isValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.CurrentPassword);
                if (isValid == PasswordVerificationResult.Success)
                {
                    if (model.NewPassword != null)
                    {
                        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                        if (result.Succeeded)
                            await _signInManager.RefreshSignInAsync(user);
                        else return BadRequest(new { message = result.Errors });
                    }

                    if (model.FirstName != user.FirstName || model.LastName != model.LastName)
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded) return Ok(user);
                        else return BadRequest();
                    }

                    if (model.Image != null && model.Image.Length > 0)
                    {
                        await _usersService.UploadImage(model.Image, user.Id);
                    }
                    else if (model.Image == null)
                    {
                        await _usersService.RemoveImage(user.Id);
                    }
                }
                else
                {
                    return BadRequest();
                }


                return Ok();

            }
            else
            {
                return BadRequest(new { message = "Model State not valid" });
            }  
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public string GetCurrentUserEmail()
        {
            var username = User.Identity.Name;
            return username;
        }

        [AllowAnonymous]
        [HttpGet("Ping")]
        public JsonResult Ping()
        {
            return new JsonResult("Pong");
        }

        [Authorize]
        [HttpGet("ControlledPing")]
        public JsonResult ControlledPing()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            return new JsonResult($"Pong {email}");
        }

        #region Helpers

        private object GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.UserData, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Secret")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(1));

            var token = new JwtSecurityToken(
                "issuer",
                "issuer",
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        }

        public class LoginResponse
        {
            public object token { get; set; }
        }

        #endregion
    }
}
