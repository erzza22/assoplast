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
    public class ReceiverController : ControllerBase
    {
        private readonly IReceiverService _receiverService;
        private readonly IDestinatarioCategoryService _destinatarioCategoryService;
        public ReceiverController(IReceiverService receiverService, IDestinatarioCategoryService destinatarioCategoryService)
        {
            _receiverService = receiverService;
            _destinatarioCategoryService = destinatarioCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ReceiverDto>>> GetReceiver()
        {
            var model = new List<ReceiverDto>();
            var receiverAsync = await _receiverService.GetReceivers();
            var receiverList = receiverAsync.ToList();
            if (!receiverList.Any())
            {
                return BadRequest("Not Found");
            }
            foreach (var entity in receiverList)
            {
                model.Add(await EntityToDtoAsync(entity));
            }
            return Ok(model.OrderByDescending(x => x.Id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ReceiverDto>> GetReceiverById(int id)
        {
            var receiver = await _receiverService.GetReceiverById(id);

            if (receiver == null)
                return BadRequest("Not found");

            var model =await EntityToDtoAsync(receiver);

            return Ok(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddReceiver([FromBody]ReceiverDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var receiver = new Receiver();
            var entity = DtoToEntity(model, receiver);

            await _receiverService.AddReceiver(entity);
            await _receiverService.SaveChanges();

            return Ok(new { status = 200, message = "Receiver added successfully" });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateReceiver([FromBody]ReceiverDto model)
        {
            var receiver = await _receiverService.GetReceiverById(model.Id);

            if (receiver == null)
                return BadRequest("Not found");

            var entity = DtoToEntity(model, receiver);

            _receiverService.UpdateReceiver(entity);
            await _receiverService.SaveChanges();

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteReceiver(int id)
        {
            var receiver = await _receiverService.GetReceiverById(id);

            if (receiver == null)
                return BadRequest("Not found");

            await _receiverService.DeleteReceiver(receiver);

            return Ok(new { status = 200, message = "Receiver deleted successfully" });
        }

        #region Privates
        private async Task<ReceiverDto> EntityToDtoAsync(Receiver entity)
        {
            var dto = new ReceiverDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy,
                ModifiedOn = entity.ModifiedOn,
                ModifiedBy = entity.ModifiedBy,
                TaxNumber = entity.TaxNumber,
                DestinationLocation = entity.DestinationLocation,
                AuthorizationNumber = entity.AuthorizationNumber,
                AuthorizationDate = entity.AuthorizationDate,
                ReceiverCategoryId=entity.ReceiverCategoryId,
                ReceiverCategoryName=await _destinatarioCategoryService.GetReceiverCategoryNameAsync(entity.ReceiverCategoryId)
            };
            return dto;
        }

        private Receiver DtoToEntity(ReceiverDto dto, Receiver entity)
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
            entity.DestinationLocation = dto.DestinationLocation;
            entity.AuthorizationNumber = dto.AuthorizationNumber;
            entity.AuthorizationDate = dto.AuthorizationDate;
            entity.ReceiverCategoryId = dto.ReceiverCategoryId;

            return entity;
        }
        #endregion
    }
}