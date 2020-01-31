using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Email.EmailDtos;

namespace MVC.Email.EmailSettings
{
    public interface IEmailService
    {
        List<EmailMessage> ReceiveEmail();
        Task<IEnumerable<Entities.Email>> GetAllEmails();
        Task AddEmail(Entities.Email email);
        Task AddEmails(List<Entities.Email> emails);
        Task<Entities.Email> GetEmailById(int id);
        Task UpdateEmail(Entities.Email email);
        List<string> GetExistingMessageIds();
        List<Entities.Email> GetUnreadEmails();
        int GetEmailIdByMessageId(string id);
        List<Entities.Email> GetAvailableEmails();
    }
}