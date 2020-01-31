using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class PickupRequestController : ControllerBase

    {
        IPickupRequestService _pickupRequestService;

        public PickupRequestController(IPickupRequestService pickupRequestService)
        {
            _pickupRequestService = pickupRequestService;
        }

       // GET api/values
       [HttpGet]
        public async Task<ActionResult<IEnumerable<PickupRequest>>> GetPickupRequests()
        {
            var model = new List<PickupRequestDto>();

            var requestsAsync = await _pickupRequestService.GetPickupRequests();

            var requests = requestsAsync.ToList();

            if (requests == null)
                return BadRequest("Not found");

            foreach (var item in requests)
            {
                model.Add(new PickupRequestDto
                {
                    Id = item.Id,
                    Location = item.Location,
                    Note = item.Note,
                    NumberOfObjects = item.NumberOfObjects,
                    PickupDate = item.PickupDate
                });

            }


            return Ok(model);
        }

       // GET api/values/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetPickupRequestDetails(int id)
        {
            var model = new PickupRequestDto();

            var request = await _pickupRequestService.GetPickupRequestById(id);

            if (request == null)
                return BadRequest("Not found");

            model.Id = request.Id;
            model.Location = request.Location;
            model.Note = request.Note;
            model.NumberOfObjects = request.NumberOfObjects;
            model.PickupDate = request.PickupDate;

            return Ok(model);

        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddPickupRequest(PickupRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pickupRequest = new PickupRequest();

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;


            pickupRequest.CreatedOn = DateTime.Now;
            pickupRequest.CreatedBy = email;
            pickupRequest.Location = model.Location;
            pickupRequest.Note = model.Note;
            pickupRequest.NumberOfObjects = model.NumberOfObjects;
            pickupRequest.PickupDate = model.PickupDate;

            await _pickupRequestService.AddPickupRequest(pickupRequest);
            await _pickupRequestService.SaveChanges();

            return Ok(new { status = 200});
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePickupRequest(PickupRequestDto model)
        {
            var pickupRequest = await _pickupRequestService.GetPickupRequestById(model.Id);

            if (pickupRequest == null)
                return BadRequest("Not found");

            var claimsIdentity = User.Identity as ClaimsIdentity;
            var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;


            pickupRequest.ModifiedOn = DateTime.Now;
            pickupRequest.ModifiedBy = email;
            pickupRequest.Location = model.Location;
            pickupRequest.Note = model.Note;
            pickupRequest.NumberOfObjects = model.NumberOfObjects;
            pickupRequest.PickupDate = model.PickupDate;

            _pickupRequestService.UpdatePickupRequest(pickupRequest);
            await _pickupRequestService.SaveChanges();

            return Ok();
        }


        // DELETE api/values/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePickupRequest(int id)
        {
            var pickupRequest = await _pickupRequestService.GetPickupRequestById(id);

            if (pickupRequest == null)
                return BadRequest("Not found");

            await _pickupRequestService.DeletePickupRequest(pickupRequest);

            return Ok(new { status = 200, message = "Pickup Request deleted successfully"});
        }


    }
}
