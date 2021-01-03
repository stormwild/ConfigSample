using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigDomain;

namespace ConfigConsole
{
    class Program
    {
        // Display user's menu.
        public static void UserMenu()
        {
            string applicationName =
                Environment.GetCommandLineArgs()[0] + ".exe";
            StringBuilder buffer = new StringBuilder();

            buffer.AppendLine("Application: " + applicationName);
            buffer.AppendLine("Make your selection.");
            buffer.AppendLine("?    -- Display help.");
            buffer.AppendLine("Q,q  -- Exit the application.");

            buffer.Append("1    -- Instantiate the");
            buffer.AppendLine(" Configuration class.");

            buffer.Append("2    -- Use GetSection(string) to read ");
            buffer.AppendLine(" a custom section.");

            buffer.Append("3    -- Use SaveAs methods");
            buffer.AppendLine(" to save the configuration file.");

            buffer.Append("4    -- Use AppSettings property to read");
            buffer.AppendLine(" the appSettings section.");
            buffer.Append("5    -- Use ConnectionStrings property to read");
            buffer.AppendLine(" the connectionStrings section.");

            buffer.Append("6    -- Use Configuration class properties");
            buffer.AppendLine(" to obtain configuration information.");

            Console.Write(buffer.ToString());
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Config Sample");

            // Define user selection string.
            string selection;

            // Get the name of the application.
            string appName =
                Environment.GetCommandLineArgs()[0];

            // Get user selection.
            while (true)
            {
                UserMenu();
                Console.Write("> ");
                selection = Console.ReadLine();
                if (!string.IsNullOrEmpty(selection))
                    break;
            }

            while (selection.ToLower() != "q")
            {
                // Process user's input.
                switch (selection)
                {
                    case "1":
                        // Show how to create an instance of the Configuration class.
                        UsingConfiguration.CreateConfigurationFile();
                        break;

                    case "2":
                        // Show how to use GetSection(string) method.
                        UsingConfiguration.GetCustomSection();
                        break;

                    case "3":
                        // Show how to use ConnectionStrings.
                        UsingConfiguration.SaveConfigurationFile();
                        break;

                    case "4":
                        // Show how to use the AppSettings property.
                        UsingConfiguration.GetSections("appSettings");
                        break;

                    case "5":
                        // Show how to use the ConnectionStrings property.
                        UsingConfiguration.GetSections("connectionStrings");
                        break;

                    case "6":
                        // Show how to obtain configuration file information.
                        UsingConfiguration.GetConfigurationInformation();
                        break;

                    default:
                        UserMenu();
                        break;
                }
                Console.Write("> ");
                selection = Console.ReadLine();
            }

            //Console.Read();
        }
    }
}
