using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MVC.Dtos;
using MVC.Entities;
using MVC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class DestinatarioCategoryController : ControllerBase
    {
        IDestinatarioCategoryService _destinatarioCategoryService;
        public DestinatarioCategoryController(IDestinatarioCategoryService destinatarioCategoryService)
        {
            _destinatarioCategoryService = destinatarioCategoryService;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<DestinatarioCategory>>> GetDestinatarioCategories()
        {
            var model = new List<DestinatarioCategoryDto>();
            var categoriesAsync = await _destinatarioCategoryService.GetDestinatarioCategories();
            var categories = categoriesAsync.ToList().OrderByDescending(x=>x.Id);

            if (categories == null)
                return BadRequest("Not found");

            foreach (var item in categories)
            {
                model.Add(new DestinatarioCategoryDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Description = item.Description
                });
            }
            return Ok(model);
        }
  
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetDestinatarioCategoryDetails(int id)
        {
            var model = new DestinatarioCategoryDto();
            var category = await _destinatarioCategoryService.GetDestinatarioCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            model.Id = category.Id;
            model.Name = category.Name;
            model.Code = category.Code;
            model.Description = category.Description;
      
            return Ok(model);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddDestinatarioCategory(DestinatarioCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            var destinatarioCategory = new DestinatarioCategory();

            destinatarioCategory.Name = model.Name;
            destinatarioCategory.CreatedOn = DateTime.Now;
            destinatarioCategory.CreatedBy = email;
            destinatarioCategory.Code = model.Code;
            destinatarioCategory.Description = model.Description;
            

            await _destinatarioCategoryService.AddDestinatarioCategory(destinatarioCategory);
            await _destinatarioCategoryService.SaveChanges();

            return Ok(new { status = 200});
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateDestinatarioCategory(DestinatarioCategoryDto model)
        {
            var category = await _destinatarioCategoryService.GetDestinatarioCategoryById(model.Id);

            if (category == null)
                return BadRequest("Not found");
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            category.Name = model.Name;
            category.ModifiedOn = DateTime.Now;
            category.ModifiedBy = email;
            category.Code = model.Code;
            category.Description = model.Description;
           

            _destinatarioCategoryService.UpdateDestinatarioCategory(category);
            await _destinatarioCategoryService.SaveChanges();

            return Ok();
        }


        // DELETE api/values/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDestinatarioCategory(int id)
        {
            var category = await _destinatarioCategoryService.GetDestinatarioCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            await _destinatarioCategoryService.DeleteDestinatarioCategory(category);

            return Ok(new { status = 200, message = "Destinatario Category deleted successfully"});
        }
    }
}
