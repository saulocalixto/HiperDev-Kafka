using System.Threading.Tasks;

namespace CalixtosStore.Email.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}