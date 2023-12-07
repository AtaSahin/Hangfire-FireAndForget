using System;
using System.Drawing;
using System.IO;

namespace Hangfire.Web.BackgroundJobs
{
    public class DelayedJobs
    {
        public static string AddWaterMarkJob(string filename,string watermarkText)
        {

           return Hangfire.BackgroundJob.Schedule(()=> ApplyWaterMark(filename,watermarkText),TimeSpan.FromSeconds(10));

        }






        public static void ApplyWaterMark(string filename,string watermarkText)
        {
            string path =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/pictures",filename);


            using (var bitmap=System.Drawing.Image.FromFile(path))
            {
               using(Bitmap tempBitmap=new Bitmap(bitmap.Width,bitmap.Height))
                {
                   using(Graphics grp=Graphics.FromImage(tempBitmap))
                    {
                     grp.DrawImage(bitmap,0,0);
                        var font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold, GraphicsUnit.Pixel);
                        var color = Color.FromArgb(128, 255, 255, 255);
                        var brush = new SolidBrush(color);
                        var point = new Point(0, 0);
                        grp.DrawString(watermarkText, font, brush, point);
                        tempBitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", "watermark_" + filename));

                   }
               }
            }




        }


    }
}
