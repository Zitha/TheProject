using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheProject.Data;
using TheProject.Model;
using TheProject.ReportGenerator;

namespace TheProject.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Initializing Database...");

            try
            {
                //var context = new DataContext();
                //context.Database.Initialize(true);

                //context.SaveChanges();

                //SerializeImage();
                GenerateReport();
                Console.WriteLine("Done...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async static void SerializeImage()
        {
            var picture = File.ReadAllBytes(@"C:\Projects\TheProject\TheProject.Web\TheProject.ConsoleApp\bin\Debug\effectuation.jpg");

            var base64 = System.Convert.ToBase64String(picture);
            using (MemoryStream ms = new MemoryStream())
            {
                using (HttpClient client = new HttpClient())
                {
                    List<Picture> pictures = new List<Picture>();

                    pictures.Add(new Picture
                    {
                        Name = "Second Picture",
                        File = base64
                    });
                    StringBuilder callUrll = new StringBuilder(JsonConvert.SerializeObject(pictures));

                    var content = new StringContent(JsonConvert.SerializeObject(pictures), Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync("http://localhost:63785/api/Building/SaveImage", content);
                    //var response = await client.PostAsync("http://154.0.170.81:89/api/Building/SaveImage", content);
                }
            }
        }

        private static void ConvertFiletToFile(byte[] fileByte)
        {
            string _picturePath = Environment.CurrentDirectory;
            if (!Directory.Exists(_picturePath))
            {
                Directory.CreateDirectory(_picturePath);
            }

            using (FileStream fileStream = new FileStream(Path.Combine(_picturePath, "ThemPic" + ".png"), FileMode.Create))
            {
                fileStream.Write(fileByte, 0, fileByte.Length);
                fileStream.Close();
            }
        }

        private static void GenerateReport()
        {
            FacilityReport facilityReport = new FacilityReport();
            ApplicationUnit unit = new ApplicationUnit();

            List<Facility> facilities = unit.Facilities.GetAll()
                                        .Include(b => b.Buildings)
                                        .Include(d => d.DeedsInfo)
                                        .Include(c => c.Portfolio)
                                        .Include(p => p.ResposiblePerson)
                                        .Include(l => l.Location)
                                        .ToList();

            int i = 0;
            foreach (var facility in facilities)
            {
                if (i < 30)
                {
                    string facilityLocation = facilityReport.GenerateInvoice(facility);
                    i++;
                }
            }
        }
    }
}
