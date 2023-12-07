using Hangfire.Web.Services;
using System.Security.Policy;

namespace Hangfire.Web.BackgroundJobs
{
    public class FireAndForgetJobs
    {
        public static void EmailSendToUserJob(string userId, string message)
        {
             Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(userId, message)); // bu şekilde de kullanılabilir.
           // Hangfire.BackgroundJob.Enqueue(EmailSender.SendEmailToUser(userId, message)); // bu şekilde de kullanılabilir.
        }
    }
}
