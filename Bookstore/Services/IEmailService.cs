using Bookstore.Models;
using System.Threading.Tasks;

namespace Bookstore.Services
{
    public interface IEmailService
    {
        string GetmailBody(string templateName);
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailforEmailConfirmation(UserEmailOptions userEmailOptions);
    }
}