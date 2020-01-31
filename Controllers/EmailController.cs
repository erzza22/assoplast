using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Email.EmailDtos;
using MVC.Email.EmailSettings;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("GetAllMessagesFromServer")]
        public async Task<ActionResult<IEnumerable<Entities.Email>>> GetEmailMessagesAsync()
        {
            var newEmails = new List<Entities.Email>();
            var messageIds = _emailService.GetExistingMessageIds();
            var emailsFromInbox = _emailService.ReceiveEmail().ToList();
            var userEmail = GetCurrentUser();

            if (!emailsFromInbox.Any())
                return BadRequest("Not found");

            foreach (var email in emailsFromInbox)
            {
                if (!messageIds.Contains(email.MessageId))
                {
                    newEmails.Add(new Entities.Email
                    {
                        Id = email.Id,
                        MessageId = email.MessageId,
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = userEmail,
                        ModifiedOn = DateTime.UtcNow,
                        ModifiedBy = userEmail,
                        Sender = string.Join(' ', email.FromAddresses.Select(fa => fa.Name)),
                        EmailAddress = string.Join(' ', email.FromAddresses.Select(fa => fa.Address)),
                        Subject = email.Subject,
                        BodyContent = email.Content,
                        IsRead = false,
                        IsDeleted = false,
                        Date = email.Date,
                        FileName = email.FileName
                    });
                }
            }

            await _emailService.AddEmails(newEmails);

            return Ok(emailsFromInbox.OrderByDescending(x => x.Date));
        }

        [HttpGet("GetAllEmailsFromDb")]
        public ActionResult<IEnumerable<EmailsDto>> GetAllEmailsFromDb()
        {
            var emailList = _emailService.GetAvailableEmails();
            var dtoList = new List<EmailsDto>();

            if (!emailList.Any())
                return BadRequest("Not found");

            foreach (var entity in emailList)
            {
                dtoList.Add(new EmailsDto
                {
                    Id = entity.Id,
                    MessageId = entity.MessageId,
                    Sender = entity.Sender,
                    EmailAddress = entity.EmailAddress,
                    Subject = entity.Subject,
                    BodyContent = entity.BodyContent,
                    IsRead = entity.IsRead,
                    IsDeleted = entity.IsDeleted,
                    Date = entity.Date,
                    FileName = entity.FileName
                });
            }

            return Ok(dtoList.OrderByDescending(x => x.Date));
        }

        //[HttpGet("GetUnreadEmails")]
        //public ActionResult<List<EmailsDto>> GetUnreadEmails()
        //{
        //    var unreadEmails = _emailService.GetUnreadEmails();
        //    //var emailsFromInbox = _emailService.ReceiveEmail().ToList();
        //    var emailsToReturn = new List<EmailsDto>();

        //    if (!unreadEmails.Any())
        //        return BadRequest("Not found");

        //    foreach (var oldEmail in unreadEmails)
        //    {
        //        emailsToReturn.Add(new EmailsDto
        //        {
        //            Id = oldEmail.Id,
        //            MessageId = oldEmail.MessageId,
        //            BodyContent = oldEmail.BodyContent,
        //            Subject = oldEmail.Subject,
        //            Date = oldEmail.Date,
        //            FileName = oldEmail.FileName,
        //            Sender = oldEmail.Sender,
        //            EmailAddress = oldEmail.EmailAddress,
        //            IsRead = oldEmail.IsRead
        //        });
        //    }
        //    return Ok(emailsToReturn);
        //}

        //[HttpGet("GetEmailById/{id}")]
        //public async Task<ActionResult<EmailsDto>> GetEmailByIdAsync(int id)
        //{
        //    var email = await _emailService.GetEmailById(id);
        //    var dto = new EmailsDto();
        //    if (email == null)
        //        return BadRequest("Not found");

        //    dto.Id = email.Id;
        //    dto.MessageId = email.MessageId;
        //    dto.BodyContent = email.BodyContent;
        //    dto.Subject = email.Subject;
        //    dto.Date = email.Date;
        //    dto.FileName = email.FileName;
        //    dto.Sender = email.Sender;
        //    dto.EmailAddress = email.EmailAddress;
        //    dto.IsRead = email.IsRead;
        //    dto.IsDeleted = email.IsDeleted;

        //    return Ok(dto);
        //}

        [HttpGet("UpdateMessageStatus/{id}")]
        public async Task<IActionResult> UpdateMessageStatusAsync(int id)
        {
            var entity = await _emailService.GetEmailById(id);
            var userEmail = GetCurrentUser();

            if (entity == null)
                return BadRequest("Not found");

            entity.IsRead = true;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = userEmail;

            await _emailService.UpdateEmail(entity);

            return Ok();
        }

        [HttpGet("DeleteEmail/{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var entity = await _emailService.GetEmailById(id);
            var userEmail = GetCurrentUser();

            if (entity == null)
                return BadRequest("Not found");

            entity.IsDeleted = true;
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = userEmail;
            await _emailService.UpdateEmail(entity);

            return Ok();
        }

        #region Privates
        private string GetCurrentUser()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
        }
        #endregion
    }
}