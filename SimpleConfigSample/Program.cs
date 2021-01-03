using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigDomain;

namespace SimpleConfigSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Config Sample");

            var customSection = ConfigurationManager.GetSection("CustomSection") as CustomSection;

            Console.WriteLine($"Section name: {customSection.Name}");
            Console.WriteLine($"Url: {customSection.Url}");
            Console.WriteLine($"Port: {customSection.Port}");

            Console.Read();
        }
    }
}
