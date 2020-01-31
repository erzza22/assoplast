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
    public class ProduttoreDetentoreController : ControllerBase
    {
        IProduttoreDetentoreService _produttoreDetentoreService;
        IProduttoreDetentoreCategoryService _produttoreDetentoreCategoryService;
        public ProduttoreDetentoreController(IProduttoreDetentoreService produttoreDetentoreService, IProduttoreDetentoreCategoryService produttoreDetentoreCategoryService)
        {
            _produttoreDetentoreService = produttoreDetentoreService;
            _produttoreDetentoreCategoryService = produttoreDetentoreCategoryService;
        }

       [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProduttoreDetentoreDto>>> GetProduttoreDetentore()
        {
            var model = new List<ProduttoreDetentoreDto>();
            var produttoreDetentoreAsync = await _produttoreDetentoreService.GetProduttoreDetentore();
            var produttoreDetentore = produttoreDetentoreAsync.ToList().OrderByDescending(x=>x.Id);

            if (!produttoreDetentore.Any())
                return BadRequest("Not found");

            foreach (var entity in produttoreDetentore)
            {
                model.Add(await EntityToDtoAsync(entity));
            }
            return Ok(model);
        }
  
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProduttoreDetentoreDto>> GetProduttoreDetentoreDetails(int id)
        {
            

            var produttoreDetentore = await _produttoreDetentoreService.GetProduttoreDetentoreById(id);

            if (produttoreDetentore == null)
                return BadRequest("Not found");

            var model = await EntityToDtoAsync(produttoreDetentore);

            

            return Ok(model);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddProduttoreDetentore([FromBody]ProduttoreDetentoreDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var produttoreDetentore = new ProduttoreDetentore();
            var entity = DtoToEntity(model, produttoreDetentore);
            await _produttoreDetentoreService.AddProduttoreDetentore(entity);
            await _produttoreDetentoreService.SaveChanges();
            return Ok(new { status = 200 });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduttoreDetentore([FromBody]ProduttoreDetentoreDto model)
        {
            var produttoreDetentore = await _produttoreDetentoreService.GetProduttoreDetentoreById(model.Id);

            if (produttoreDetentore == null)
                return BadRequest("Not found");

            var entity = DtoToEntity(model, produttoreDetentore);
            _produttoreDetentoreService.UpdateProduttoreDetentore(entity);
            await _produttoreDetentoreService.SaveChanges();

            return Ok();
        }


        // DELETE api/values/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProduttoreDetentore(int id)
        {
            var produttoreDetentore = await _produttoreDetentoreService.GetProduttoreDetentoreById(id);

            if (produttoreDetentore == null)
                return BadRequest("Not found");

            await _produttoreDetentoreService.DeleteProduttoreDetentore(produttoreDetentore);

            return Ok(new { status = 200, message = "Produttore Detentore deleted successfully"});
        }



        #region Privates
        private async Task<ProduttoreDetentoreDto> EntityToDtoAsync(ProduttoreDetentore entity)
        {
            var dto = new ProduttoreDetentoreDto
            {
                Id = entity.Id,
                Name = entity.Name,
                TaxNumber = entity.TaxNumber,
                AuthorizationNumber = entity.AuthorizationNumber,
                AuthorizationDate = entity.AuthorizationDate,
                LocalUnit = entity.LocalUnit,
                ProducerCategoryId = entity.ProducerCategoryId,
                ProducerCategoryName = await _produttoreDetentoreCategoryService.GetProduttoreDetentoreCategoryNameAsync(entity.ProducerCategoryId)
            };
            return dto;
        }

        private ProduttoreDetentore DtoToEntity(ProduttoreDetentoreDto dto, ProduttoreDetentore entity)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

            if (dto.Id == 0)
            {
                entity.CreatedOn = DateTime.UtcNow;
                entity.CreatedBy = email;
            }
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = email;
            entity.Name = dto.Name;
            entity.TaxNumber = dto.TaxNumber;
            entity.AuthorizationNumber = dto.AuthorizationNumber;
            entity.AuthorizationDate = dto.AuthorizationDate;
            entity.ProducerCategoryId = dto.ProducerCategoryId;
            entity.LocalUnit = dto.LocalUnit;

            return entity;
        }
        #endregion
    }
}
