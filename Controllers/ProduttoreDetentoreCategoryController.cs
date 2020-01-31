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
    public class ProduttoreDetentoreCategoryController : ControllerBase
    {
        private readonly IProduttoreDetentoreCategoryService _produttoreDetentoreCategoryService;
        public ProduttoreDetentoreCategoryController(IProduttoreDetentoreCategoryService produttoreDetentoreCategoryService)
        {
            _produttoreDetentoreCategoryService = produttoreDetentoreCategoryService;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProduttoreDetentoreCategoryDto>>> GetProduttoreDetentoreCategories()
        {
            var model = new List<ProduttoreDetentoreCategoryDto>();
            var categoriesAsync = await _produttoreDetentoreCategoryService.GetProduttoreDetentoreCategories();
            var categories = categoriesAsync.ToList().OrderByDescending(x=>x.Id);

            if (categories == null)
                return BadRequest("Not found");

            foreach (var item in categories)
            {
                model.Add(new ProduttoreDetentoreCategoryDto
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
        public async Task<IActionResult> GetProduttoreDetentoreCategoryDetails(int id)
        {
            var model = new ProduttoreDetentoreCategoryDto();
            var category = await _produttoreDetentoreCategoryService.GetProduttoreDetentoreCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            model.Id = category.Id;
            model.Name = category.Name;
            model.Code = category.Code;
            model.Description = category.Description;
        
            return Ok(model);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddProduttoreDetentoreCategory(ProduttoreDetentoreCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            var produttoreDetentoreCategory = new ProduttoreDetentoreCategory();

            produttoreDetentoreCategory.Name = model.Name;
            produttoreDetentoreCategory.CreatedOn = DateTime.Now;
            produttoreDetentoreCategory.CreatedBy = email;
            produttoreDetentoreCategory.Code = model.Code;
            produttoreDetentoreCategory.Description = model.Description;
   

            await _produttoreDetentoreCategoryService.AddProduttoreDetentoreCategory(produttoreDetentoreCategory);
            await _produttoreDetentoreCategoryService.SaveChanges();

            return Ok(new { status = 200});
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduttoreDentoreCategory(ProduttoreDetentoreCategoryDto model)
        {
            var category = await _produttoreDetentoreCategoryService.GetProduttoreDetentoreCategoryById(model.Id);

            if (category == null)
                return BadRequest("Not found");

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            category.Name = model.Name;
            category.ModifiedOn = DateTime.Now;
            category.ModifiedBy = email;
            category.Code = model.Code;
            category.Description = model.Description;


            _produttoreDetentoreCategoryService.UpdateProduttoreDetentoreCategory(category);
            await _produttoreDetentoreCategoryService.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduttoreDetentoreCategory(int id)
        {
            var category = await _produttoreDetentoreCategoryService.GetProduttoreDetentoreCategoryById(id);

            if (category == null)
                return BadRequest("Not found");

            await _produttoreDetentoreCategoryService.DeleteProduttoreDetentoreCategory(category);

            return Ok(new { status = 200, message = "Produttore/Detentore Category deleted successfully"});
        }
    }
}
