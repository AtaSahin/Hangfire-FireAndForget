using Hangfire.Web.BackgroundJobs;
using Hangfire.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hangfire.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignUp()
        {
           //üye kayıt işlemi
           //yeni kullanıcının üyeliğini oluşturduktan sonra kullanıcıya mail gönderme işlemi
            FireAndForgetJobs.EmailSendToUserJob("1", "Merhaba, üyeliğiniz başarıyla oluşturuldu.");
            return View();
        }
       
   public IActionResult PictureSave()
        {
            BackgroundJobs.RecurringJobs.ReportingJob();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PictureSave(IFormFile picture)
        {
            string newFileName = String.Empty;
            if (picture != null && picture.Length > 0)
            {
                newFileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);

                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var path = Path.Combine(directory, newFileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await picture.CopyToAsync(stream);
                }

                string jobID = BackgroundJobs.DelayedJobs.AddWaterMarkJob(newFileName, "www.mysite.com");
                BackgroundJob.ContinueJobWith(jobID, () => BackgroundJobs.ContinuationJobs.WriteWaterMarkStatus(jobID, newFileName));
            }
            return View();
        }
    }
}
