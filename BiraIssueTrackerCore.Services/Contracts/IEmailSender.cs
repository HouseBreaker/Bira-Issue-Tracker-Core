using System.Threading.Tasks;

namespace BiraIssueTrackerCore.Services.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
