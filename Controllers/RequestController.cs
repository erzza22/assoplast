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
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;
        private readonly IRequestCategoryService _requestCategoryService;
        public RequestController(IRequestService requestService, IRequestCategoryService requestCategoryService)
        {
            _requestService = requestService;
            _requestCategoryService = requestCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequest()
        {
            var model = new List<RequestDto>();
            var requestAsync = await _requestService.GetRequests();
            var requestList = requestAsync.ToList();

            if (!requestList.Any())
            {
                return BadRequest("Not Found");
            }
            foreach (var entity in requestList)
            {
                model.Add(await EntityToDtoAsync(entity));
            }

            return Ok(model.OrderByDescending(x => x.Id));
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RequestDto>> GetRequestById(int id)
        {
            var request = await _requestService.GetRequestById(id);

            if (request == null)
                return BadRequest("Not found");

            var model = await EntityToDtoAsync(request);

            return Ok(model);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRequest([FromBody]RequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var request = new Request();
            var entity = DtoToEntity(model, request);

            await _requestService.AddRequest(entity);
            await _requestService.SaveChanges();

            return Ok(new { status = 200, message = "Request added successfully" });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRequest([FromBody]RequestDto model)
        {
            var request = await _requestService.GetRequestById(model.Id);

            if (request == null)
                return BadRequest("Not found");

            var entity = DtoToEntity(model, request);

            _requestService.UpdateRequest(entity);
            await _requestService.SaveChanges();

            return Ok(new { status = 200, message = "Request updated successfully" });
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _requestService.GetRequestById(id);

            if (request == null)
                return BadRequest("Not found");

            await _requestService.DeleteRequest(request);

            return Ok(new { status = 200, message = "Request deleted successfully" });
        }

        #region Privates
        private async Task<RequestDto> EntityToDtoAsync(Request entity)
        {
            var dto = new RequestDto
            {
                Id = entity.Id,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy,
                ModifiedOn = entity.ModifiedOn,
                ModifiedBy = entity.ModifiedBy,
                Notes = entity.Notes,

                TransporterId = entity.TransporterId,
                ProducerId = entity.ProducerId,
                ReceiverId = entity.ReceiverId,
                RequestCategoryId = entity.RequestCategoryId,
                RequestCategoryName = await _requestCategoryService.GetRequestCategoryNameAsync(entity.RequestCategoryId),

                CharacteristicsDescription = entity.CharacteristicsDescription,
                CharacteristicsCode = entity.CharacteristicsCode,
                CharacteristicsState = entity.CharacteristicsState,
                CharacteristicsDangerDescription = entity.CharacteristicsDangerDescription,

                CharacteristicsNumberOfContainers = entity.CharacteristicsNumberOfContainers,
                Location = entity.Location,
                Destination = entity.Destination,
                ExpirationDate = entity.ExpirationDate,

                DestinationTypeDescription = entity.DestinationTypeDescription,
                DestinationPhysicalChemicalProprieties = entity.DestinationPhysicalChemicalProprieties,
                DestinationType = entity.DestinationType,

            };
            return dto;
        }

        private Request DtoToEntity(RequestDto dto, Request entity)
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
            entity.Notes = dto.Notes;

            entity.TransporterId = dto.TransporterId;
            entity.ProducerId = dto.ProducerId;
            entity.ReceiverId = dto.ReceiverId;
            entity.RequestCategoryId = dto.RequestCategoryId;

            entity.CharacteristicsDescription = dto.CharacteristicsDescription;
            entity.CharacteristicsCode = dto.CharacteristicsCode;
            entity.CharacteristicsState = dto.CharacteristicsState;
            entity.CharacteristicsDangerDescription = dto.CharacteristicsDangerDescription;

            entity.CharacteristicsNumberOfContainers = dto.CharacteristicsNumberOfContainers;
            entity.Location = dto.Location;
            entity.Destination = dto.Destination;
            entity.ExpirationDate = dto.ExpirationDate;

            entity.DestinationTypeDescription = dto.DestinationTypeDescription;
            entity.DestinationPhysicalChemicalProprieties = dto.DestinationPhysicalChemicalProprieties;
            entity.DestinationType = dto.DestinationType;

            return entity;
        }
        #endregion
    }
}