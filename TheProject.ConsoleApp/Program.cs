using System;
using System.Collections.Generic;
using System.Linq;
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
                context.Database.Initialize(true);

                context.SaveChanges();
                Console.WriteLine("Done...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
