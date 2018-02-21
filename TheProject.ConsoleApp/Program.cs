using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheProject.Data;

namespace TheProject.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Initializing Database...");

            try
            {
                var context = new DataContext();
                //context.Database.Initialize(true);

                //context.SaveChanges();

                SerializeImage();
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
            byte[] picture = File.ReadAllBytes(@"C:\Projects\TheProject\TheProject.Web\TheProject.ConsoleApp\bin\Debug\effectuation.jpg");
            using (MemoryStream ms = new MemoryStream())
            {
                using (HttpClient client = new HttpClient())
                {
                    //ConvertFiletToInt(ms);
                    //ConvertFiletToFile(x);
                    List<Picture> pictures = new List<Picture>();

                    pictures.Add(new Picture
                    {
                        Name = "Second Picture",
                        File = picture
                    });
                    StringBuilder callUrll = new StringBuilder(JsonConvert.SerializeObject(pictures));
                    //callUrll.Append(pictures);

                    var content = new StringContent(JsonConvert.SerializeObject(pictures), Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync("http://localhost:63785/api/Building/SaveImage", content);
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
    }
}
