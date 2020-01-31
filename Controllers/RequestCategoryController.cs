using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Dtos;
using MVC.Entities;
using MVC.Interfaces;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class RequestCategoryController : ControllerBase
    {
        private readonly IRequestCategoryService _requestCategoryService;
        public RequestCategoryController(IRequestCategoryService requestCategoryService)
        {
            _requestCategoryService = requestCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RequestCategoryDto>>> GetRequestCategory()
        {
            var model = new List<RequestCategoryDto>();
            var requestCategoryAsync = await _requestCategoryService.GetRequestCategories();
            var requestCategoryList = requestCategoryAsync.ToList();

            if (!requestCategoryList.Any())
            {
                return BadRequest("Not Found");
            }
            foreach (var entity in requestCategoryList)
            {
                model.Add(EntityToDto(entity));
            }

            return Ok(model.OrderByDescending(x => x.Id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RequestCategoryDto>> GetRequestCategoryById(int id)
        {
            var requestCategory = await _requestCategoryService.GetRequestCategoryById(id);

            if (requestCategory == null)
                return BadRequest("Not found");

            var model = EntityToDto(requestCategory);

            return Ok(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRequestCategory([FromBody]RequestCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var requestCategory = new RequestCategory();
            var entity = DtoToEntity(model, requestCategory);

            await _requestCategoryService.AddRequestCategory(entity);
            await _requestCategoryService.SaveChanges();

            return Ok(new { status = 200 });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRequestCategory([FromBody]RequestCategoryDto model)
        {
            var requestCategory = await _requestCategoryService.GetRequestCategoryById(model.Id);

            if (requestCategory == null)
                return BadRequest("Not found");

            var entity = DtoToEntity(model, requestCategory);

            _requestCategoryService.UpdateRequestCategory(entity);
            await _requestCategoryService.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRequestCategory(int id)
        {
            var requestCategory = await _requestCategoryService.GetRequestCategoryById(id);

            if (requestCategory == null)
                return BadRequest("Not found");

            await _requestCategoryService.DeleteRequestCategory(requestCategory);

            return Ok(new { status = 200, message = "Request Category deleted successfully" });
        }

        #region Privates
        private RequestCategoryDto EntityToDto(RequestCategory entity)
        {
            var dto = new RequestCategoryDto
            {
                Id = entity.Id,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy,
                ModifiedOn = entity.ModifiedOn,
                ModifiedBy = entity.ModifiedBy,
                Name = entity.Name,
                Code = entity.Code,
                Description = entity.Description
            };
            return dto;
        }

        private RequestCategory DtoToEntity(RequestCategoryDto dto, RequestCategory entity)
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
            entity.Code = dto.Code;
            entity.Description = dto.Description;

            return entity;
        }
        #endregion
    }
}