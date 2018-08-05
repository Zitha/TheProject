using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using TheProject.Data;
using TheProject.Model;
using TheProject.ReportGenerator;
using Excel = Microsoft.Office.Interop.Excel;

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

                //ReadExcelData();
                Console.WriteLine("Done...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ReadExcelData()
        {
            string path = @"C:\Projects\TheProject\TheProject.Web\TheProject.Web\Data\EMM list 20180507.xls";

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(path);
            var workSheets = xlWorkbook.Sheets.Count;
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    //new line
                    if (j == 1)
                        Console.Write("\r\n");

                    //write the value to the console
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
                }
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
                                        .Include("Location.GPSCoordinates")
                                        .Include("Location.BoundryPolygon")
                                        .ToList();
                //facility.ClientCode== "N14000000003740000000000000" || facility.ClientCode== "R22000000004960000000000000"
                    //|| facility.ClientCode == "N14000000006110000000000000" ||
            foreach (var facility in facilities)
            {
                if (facility.ClientCode == "H21005000022300000000000000"||facility.ClientCode== "H21000000001280000000000000")
                {
                    //string facilityLocation = facilityReport.GenerateFacilityReport(facility);
                }
            }
        }
    }
}
