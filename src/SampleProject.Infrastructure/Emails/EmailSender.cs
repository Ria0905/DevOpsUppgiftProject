using System.Threading.Tasks;
using SampleProject.Application.Configuration.Emails;

namespace SampleProject.Infrastructure.Emails
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(EmailMessage message)
        {
            return Task.CompletedTask;
        }
    }
}