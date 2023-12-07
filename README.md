# Hangfire-FireAndForget
Hangfire Fire and Forget

Bu proje, Hangfire kullanarak .NET Core 3.1 MVC uygulamasında SendGrid API ile e-posta gönderimini gerçekleştirmek için ölçeklendirilebilir bir kod örneği içermektedir.
![hangfiress_1](https://github.com/AtaSahin/Hangfire-FireAndForget/assets/80812122/c87d94ff-1949-4cbe-8da2-7e6e895a2700)
![hangfiress_2](https://github.com/AtaSahin/Hangfire-FireAndForget/assets/80812122/4afd750b-c695-4513-9378-028fd3ec2dad)

Kullanım:

1. Proje Kurulumu
   git clone https://github.com/AtaSahin/Hangfire-FireAndForget.git


2. Gerekli Paketleri Yükleme
   dotnet restore

3. Konfigürasyon Ayarları
   Projenizdeki appsettings.json dosyasında SendGrid API anahtarınızı tanımlayın:
   {
     "APIs": {
       "SendGridApi": "YOUR_SENDGRID_API_KEY"
     }
   }

4. Proje Çalıştırma
   dotnet run


Proje Yapısı:

- Services: E-posta gönderimi ve diğer iş mantığı hizmetlerini sağlayan servisleri içerir.
- HangfireJobs: Hangfire işlerini tanımlayan sınıfları içerir.

E-posta Gönderimi:

var apiKey = _configuration.GetSection("APIs")["SendGridApi"];
var client = new SendGridClient(apiKey);
var from = new EmailAddress("atas01649@gmail.com", "Example User");
var subject = "www.mysite.com bilgilendirme";
var to = new EmailAddress("adilatasahin@hotmail.com", "Example User");
var htmlContent = $"<strong>{message}</strong>";
var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
var response = await client.SendEmailAsync(msg);

