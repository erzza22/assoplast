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
    public class TransportatoreController : ControllerBase
    {
        private readonly ITransportatoreService _transportatoreService;
        private readonly ITransportatoreCategoryService _transportatoreCategoryService;

        public TransportatoreController(ITransportatoreService transportatoreService, ITransportatoreCategoryService transportatoreCategoryService)
        {
            _transportatoreService = transportatoreService;
            _transportatoreCategoryService = transportatoreCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TransportatoreDto>>> GetTransportatore()
        {
            var model = new List<TransportatoreDto>();
            var transportatoreAsync = await _transportatoreService.GetTransportatore();
            var transportatoreList = transportatoreAsync.ToList();

            if (!transportatoreList.Any())
            {
                return BadRequest("Not Found");
            }
            foreach (var entity in transportatoreList)
            {
                model.Add(await EntityToDtoAsync(entity));
            }

            return Ok(model.OrderByDescending(x => x.Id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<TransportatoreDto>> GetTransportatoreById(int id)
        {
            var transportatore = await _transportatoreService.GetTransportatoreById(id);

            if (transportatore == null)
                return BadRequest("Not found");

            var model =await EntityToDtoAsync(transportatore);

            return Ok(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTransportatore([FromBody]TransportatoreDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var transportatore = new Transportatore();
            var entity = DtoToEntity(model, transportatore);

            await _transportatoreService.AddTransportatore(entity);
            await _transportatoreService.SaveChanges();

            return Ok(new { status = 200 });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateTransportatore([FromBody]TransportatoreDto model)
        {
            var transportatore = await _transportatoreService.GetTransportatoreById(model.Id);

            if (transportatore == null)
                return BadRequest("Not found");

            var entity = DtoToEntity(model, transportatore);

            _transportatoreService.UpdateTransportatore(entity);
            await _transportatoreService.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTransportatore(int id)
        {
            var transportatore = await _transportatoreService.GetTransportatoreById(id);

            if (transportatore == null)
                return BadRequest("Not found");

            await _transportatoreService.DeleteTransportatore(transportatore);

            return Ok(new { status = 200, message = "Transportatore deleted successfully" });
        }

        #region Privates
        private async Task<TransportatoreDto> EntityToDtoAsync(Transportatore entity)
        {
            var dto = new TransportatoreDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy,
                ModifiedOn = entity.ModifiedOn,
                ModifiedBy = entity.ModifiedBy,
                TaxNumber = entity.TaxNumber,
                Address = entity.Address,
                AuthorizationNumber = entity.AuthorizationNumber,
                AuthorizationDate = entity.AuthorizationDate,
                EstablishmentName = entity.EstablishmentName,
                TransportatoreCategoryId = entity.TransportatoreCategoryId,
                TransportatoreCategoryName = await _transportatoreCategoryService.GetTransportatoreCategoryNameAsync(entity.TransportatoreCategoryId)
            };
            return dto;
        }

        private Transportatore DtoToEntity(TransportatoreDto dto, Transportatore entity)
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
            entity.Address = dto.Address;
            entity.AuthorizationNumber = dto.AuthorizationNumber;
            entity.AuthorizationDate = dto.AuthorizationDate;
            entity.TransportatoreCategoryId = dto.TransportatoreCategoryId;
            entity.EstablishmentName = dto.EstablishmentName;

            return entity;
        }
        #endregion
    }
}