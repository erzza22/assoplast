using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.Dtos;

using MVC.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Cors;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class ApplianceCategoryController : ControllerBase
    {
        IApplianceCategoryService _applianceCategoryService;
        public ApplianceCategoryController(IApplianceCategoryService applianceCategoryService)
        {
            _applianceCategoryService = applianceCategoryService;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ApplianceCategory>>> GetApplianceCategories()
        {
            var model = new List<ApplianceCategoryDto>();
            var categoriesAsync = await _applianceCategoryService.GetApplianceCategories();
            var categories = categoriesAsync.ToList().OrderByDescending(x=>x.Id);

            if (categories == null)
                return BadRequest("Not found");

            foreach (var item in categories)
            {
                model.Add(new ApplianceCategoryDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Code = item.Code,
                    Description = item.Description,
                    Volume = item.Volume
                });
            }
            return Ok(model);
        }
  
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetApplianceCategoryDetails(int id)
        {
            var model = new ApplianceCategoryDto();
            var category = await _applianceCategoryService.GetApplianceCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            model.Id = category.Id;
            model.Name = category.Name;
            model.Code = category.Code;
            model.Description = category.Description;
            model.Volume = category.Volume;

            return Ok(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddApplianceCategory(ApplianceCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;


            var applianceCategory = new ApplianceCategory();

            applianceCategory.Name = model.Name;
            applianceCategory.CreatedOn = DateTime.Now;
            applianceCategory.CreatedBy = email;
            applianceCategory.Code = model.Code;
            applianceCategory.Description = model.Description;
            applianceCategory.Volume = model.Volume;

            await _applianceCategoryService.AddApplianceCategory(applianceCategory);
            await _applianceCategoryService.SaveChanges();

            return Ok(new { status = 200});
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateApplianceCategory(ApplianceCategoryDto model)
        {
            var category = await _applianceCategoryService.GetApplianceCategoryById(model.Id);

            if (category == null)
                return BadRequest("Not found");
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            category.Name = model.Name;
            category.ModifiedOn = DateTime.Now;
            category.ModifiedBy = email;
            category.Code = model.Code;
            category.Description = model.Description;
            category.Volume = model.Volume;

            _applianceCategoryService.UpdateApplianceCategory(category);
            await _applianceCategoryService.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteApplianceCategory(int id)
        {
            var category = await _applianceCategoryService.GetApplianceCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            await _applianceCategoryService.DeleteApplianceCategory(category);

            return Ok(new { status = 200, message = "Appliance Category deleted successfully"});
        }
    }
}
