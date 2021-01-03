using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigDomain
{
    public class UsingConfiguration
    {
        // Show how to create an instance of the Configuration class
        // that represents this application configuration file.  
        public static void CreateConfigurationFile()
        {
            try
            {

                // Create a custom configuration section.
                CustomSection customSection = new CustomSection();

                // Get the current configuration file.
                System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);

                // Create the custom section entry  
                // in <configSections> group and the 
                // related target section in <configuration>.
                if (config.Sections["CustomSection"] == null)
                {
                    config.Sections.Add("CustomSection", customSection);
                }

                // Create and add an entry to appSettings section.

                string conStringname = "LocalSqlServer";
                string conString =
                    @"data source=.\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
                string providerName = "System.Data.SqlClient";

                ConnectionStringSettings connStrSettings = new ConnectionStringSettings();
                connStrSettings.Name = conStringname;
                connStrSettings.ConnectionString = conString;
                connStrSettings.ProviderName = providerName;

                config.ConnectionStrings.ConnectionStrings.Add(connStrSettings);

                // Add an entry to appSettings section.
                int appStgCnt =
                    ConfigurationManager.AppSettings.Count;
                string newKey = "NewKey" + appStgCnt.ToString();

                string newValue = DateTime.Now.ToLongDateString() +
                                  " " + DateTime.Now.ToLongTimeString();

                config.AppSettings.Settings.Add(newKey, newValue);

                // Save the configuration file.
                customSection.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Full);

                Console.WriteLine("Created configuration file: {0}",
                    config.FilePath);
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("CreateConfigurationFile: {0}", err.ToString());
            }
        }

        // Show how to use the GetSection(string) method.
        public static void GetCustomSection()
        {
            try
            {

                CustomSection customSection;

                // Get the current configuration file.
                System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                customSection =
                    config.GetSection("CustomSection") as CustomSection;

                Console.WriteLine("Section name: {0}", customSection.Name);
                Console.WriteLine("Url: {0}", customSection.Url);
                Console.WriteLine("Port: {0}", customSection.Port);
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("Using GetSection(string): {0}", err.ToString());
            }
        }


        // Show how to use different modalities to save 
        // a configuration file.
        public static void SaveConfigurationFile()
        {
            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Save the full configuration file and force save even if the file was not modified.
                config.SaveAs("MyConfigFull.config", ConfigurationSaveMode.Full, true);
                Console.WriteLine("Saved config file as MyConfigFull.config using the mode: {0}",
                    ConfigurationSaveMode.Full.ToString());

                config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Save only the part of the configuration file that was modified. 
                config.SaveAs("MyConfigModified.config", ConfigurationSaveMode.Modified, true);
                Console.WriteLine("Saved config file as MyConfigModified.config using the mode: {0}",
                    ConfigurationSaveMode.Modified.ToString());

                config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Save the full configuration file.
                config.SaveAs("MyConfigMinimal.config");
                Console.WriteLine("Saved config file as MyConfigMinimal.config using the mode: {0}",
                    ConfigurationSaveMode.Minimal.ToString());
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("SaveConfigurationFile: {0}", err.ToString());
            }
        }

        // Show how use the AppSettings and ConnectionStrings 
        // properties.
        public static void GetSections(string section)
        {
            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                // Get the selected section.
                switch (section)
                {
                    case "appSettings":
                        try
                        {
                            AppSettingsSection appSettings =
                                config.AppSettings as AppSettingsSection;
                            Console.WriteLine("Section name: {0}",
                                appSettings.SectionInformation.SectionName);

                            // Get the AppSettings section elements.
                            Console.WriteLine();
                            Console.WriteLine("Using AppSettings property.");
                            Console.WriteLine("Application settings:");
                            // Get the KeyValueConfigurationCollection 
                            // from the configuration.
                            KeyValueConfigurationCollection settings =
                                config.AppSettings.Settings;

                            // Display each KeyValueConfigurationElement.
                            foreach (KeyValueConfigurationElement keyValueElement in settings)
                            {
                                Console.WriteLine("Key: {0}", keyValueElement.Key);
                                Console.WriteLine("Value: {0}", keyValueElement.Value);
                                Console.WriteLine();
                            }
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine("Using AppSettings property: {0}",
                                e.ToString());
                        }

                        break;

                    case "connectionStrings":
                        ConnectionStringsSection
                            conStrSection =
                                config.ConnectionStrings as ConnectionStringsSection;
                        Console.WriteLine("Section name: {0}",
                            conStrSection.SectionInformation.SectionName);

                        try
                        {
                            if (conStrSection.ConnectionStrings.Count != 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Using ConnectionStrings property.");
                                Console.WriteLine("Connection strings:");

                                // Get the collection elements.
                                foreach (ConnectionStringSettings connection in
                                    conStrSection.ConnectionStrings)
                                {
                                    string name = connection.Name;
                                    string provider = connection.ProviderName;
                                    string connectionString = connection.ConnectionString;

                                    Console.WriteLine("Name:               {0}",
                                        name);
                                    Console.WriteLine("Connection string:  {0}",
                                        connectionString);
                                    Console.WriteLine("Provider:            {0}",
                                        provider);
                                }
                            }
                        }
                        catch (ConfigurationErrorsException e)
                        {
                            Console.WriteLine("Using ConnectionStrings property: {0}",
                                e.ToString());
                        }

                        break;

                    default:
                        Console.WriteLine(
                            "GetSections: Unknown section (0)", section);
                        break;
                }
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("GetSections: (0)", err.ToString());
            }
        }

        // Show how to use the Configuration object properties 
        // to obtain configuration file information.
        public static void GetConfigurationInformation()
        {
            try
            {

                // Get the current configuration file.
                System.Configuration.Configuration config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                Console.WriteLine("Reading configuration information:");

                ContextInformation evalContext =
                    config.EvaluationContext as ContextInformation;
                Console.WriteLine("Machine level: {0}",
                    evalContext.IsMachineLevel.ToString());

                string filePath = config.FilePath;
                Console.WriteLine("File path: {0}", filePath);

                bool hasFile = config.HasFile;
                Console.WriteLine("Has file: {0}", hasFile.ToString());

                ConfigurationSectionGroupCollection
                    groups = config.SectionGroups;
                Console.WriteLine("Groups: {0}", groups.Count.ToString());
                foreach (ConfigurationSectionGroup group in groups)
                {
                    Console.WriteLine("Group Name: {0}", group.Name);
                    // Console.WriteLine("Group Type: {0}", group.Type);
                }

                ConfigurationSectionCollection
                    sections = config.Sections;
                Console.WriteLine("Sections: {0}", sections.Count.ToString());
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("GetConfigurationInformation: {0}", err.ToString());
            }
        }

    }
}
