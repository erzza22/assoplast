using Microsoft.AspNetCore.Mvc;
using MVC.Entities;
using MVC.Interfaces;
using MVC.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class TransportatoreCategoryController : ControllerBase
    {
        ITransportatoreCategoryService _transportatoreCategoryService;
        public TransportatoreCategoryController(ITransportatoreCategoryService transportatoreCategoryService)
        {
            _transportatoreCategoryService = transportatoreCategoryService;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TransportatoreCategory>>> GetTransportatoreCategories()
        {
            var model = new List<TransportatoreCategoryDto>();
            var categoriesAsync = await _transportatoreCategoryService.GetTransportatoreCategories();
            var categories = categoriesAsync.ToList().OrderByDescending(x=>x.Id);

            if (categories == null)
                return BadRequest("Not found");

            foreach (var item in categories)
            {
                model.Add(new TransportatoreCategoryDto
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
        public async Task<IActionResult> GetTransportatoreCategoryDetails(int id)
        {
            var model = new TransportatoreCategoryDto();
            var category = await _transportatoreCategoryService.GetTransportatoreCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            model.Id = category.Id;
            model.Name = category.Name;
            model.Code = category.Code;
            model.Description = category.Description;

            return Ok(model);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddTransportatoreCategory(TransportatoreCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            var transportatoreCategory = new TransportatoreCategory();

            
            transportatoreCategory.CreatedOn = DateTime.Now;
            transportatoreCategory.CreatedBy = email;
            transportatoreCategory.Name = model.Name;
            transportatoreCategory.Code = model.Code;
            transportatoreCategory.Description = model.Description;
           

            await _transportatoreCategoryService.AddTransportatoreCategory(transportatoreCategory);
            await _transportatoreCategoryService.SaveChanges();

            return Ok(new { status = 200});
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTransportatoreCategory(TransportatoreCategoryDto model)
        {
            var category = await _transportatoreCategoryService.GetTransportatoreCategoryById(model.Id);

            if (category == null)
                return BadRequest("Not found");
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            category.Name = model.Name;
            category.ModifiedOn = DateTime.Now;
            category.ModifiedBy = email;
            category.Code = model.Code;
            category.Description = model.Description;
           

            _transportatoreCategoryService.UpdateTransportatoreCategory(category);
            await _transportatoreCategoryService.SaveChanges();

            return Ok();
        }


        // DELETE api/values/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTransportatoreCategory(int id)
        {
            var category = await _transportatoreCategoryService.GetTransportatoreCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            await _transportatoreCategoryService.DeleteTransportatoreCategory(category);

            return Ok(new { status = 200, message = "Transportatore Category deleted successfully"});
        }
    }
}
