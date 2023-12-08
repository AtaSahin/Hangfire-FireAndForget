using System.Diagnostics;

namespace Hangfire.Web.BackgroundJobs
{
    public class RecurringJobs
    {
        public static void ReportingJob()
        {
            Hangfire.RecurringJob.AddOrUpdate("ReportJob1",()=> EmailReport(),Hangfire.Cron.Minutely);
        }
        public static void EmailReport()
        {
            Debug.WriteLine("Rapor email olarak gönderildi...");
        }
    }
}
