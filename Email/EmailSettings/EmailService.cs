using MailKit.Net.Pop3;
using MimeKit;
using MVC.Email.EmailDtos;
using MVC.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Email.EmailSettings
{
    public class EmailService : IEmailService
    {
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IGenericRepository<MVC.Entities.Email> _emailRepository;
        public EmailService(IEmailConfiguration emailConfiguration, IGenericRepository<MVC.Entities.Email> emailRepository)
        {
            _emailConfiguration = emailConfiguration;
            _emailRepository = emailRepository;
        }

        public List<EmailMessage> ReceiveEmail()
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();

                for (int i = 0; i < emailClient.Count; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var attachmentList = new List<byte[]>();
                    string attachmentName = null;

                    foreach (MimeEntity attachment in message.Attachments)
                    {
                        var fileName = "D:\\" + attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                        using (var stream = File.Create(fileName))
                        {
                            if (attachment is MessagePart)
                            {
                                var rfc822 = (MessagePart)attachment;
                                rfc822.Message.WriteTo(stream);
                            }
                            else
                            {
                                var part = (MimePart)attachment;
                                attachmentName = part.FileName;
                                //part.Content.DecodeTo(stream);
                                //using (MemoryStream ms = new MemoryStream())
                                //{
                                //    int read;
                                //    byte[] buffer = new byte[16 * 1024];
                                //    while ((read = part.Content.Stream.Read(buffer, 0, buffer.Length)) > 0)
                                //    {
                                //        ms.Write(buffer, 0, read);
                                //    }
                                //    attachmentList.Add(ms.ToArray());
                                //}
                            }
                        }
                    }

                    //var email = new Entities.Email
                    //{
                    //    bodycontent = "ddd"
                    //};

                    //_emailRepository.Add(email);

                    var emailMessage = new EmailMessage
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject,
                        Date = message.Date.DateTime,
                        MessageId = message.MessageId,
                        //Attachments = attachmentList,
                        FileName = attachmentName
                    };
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }
                return emails;
            }
        }

        public async Task<IEnumerable<Entities.Email>> GetAllEmails()
        {
            try
            {
                return await _emailRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entities.Email> GetAvailableEmails()
        {
            try
            {
                return  _emailRepository.Find(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddEmails(List<Entities.Email> emails)
        {
            try
            {
                await _emailRepository.AddRange(emails);
                await _emailRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddEmail(Entities.Email email)
        {
            try
            {
                await _emailRepository.Add(email);
                await _emailRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Entities.Email> GetEmailById(int id)
        {
            try
            {
                return await _emailRepository.FindOne(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateEmail(Entities.Email email)
        {
            try
            {
                _emailRepository.Update(email);
                await _emailRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetExistingMessageIds()
        {
            try
            {
                return _emailRepository.Query().Select(m => m.MessageId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Entities.Email> GetUnreadEmails()
        {
            try
            {
                return _emailRepository.Find(x => x.IsRead == false).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetEmailIdByMessageId(string id)
        {
            try
            {
                return _emailRepository.Query().Where(x => x.MessageId == id).Select(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
