using CommonUtils.Settings.Providers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace KnowledgeBase
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static void Main()
        {
            TestAppConfig();
            Console.WriteLine("Hello, World!");
        }

        static void TestAppConfig()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = (ApplicationConfigSection)cfg.GetSection("AppSettings");
            section.ApplicationSettings.set
            

            if (section != null)
            {
                Console.WriteLine(section.ApplicationSettings[0].Name);
                Console.WriteLine(section.ApplicationSettings[0].Value);
            }
            else
            {
                section = new ApplicationConfigSection();
                cfg.Sections.Add("AppSettings", section);
                
            }
            section.ApplicationSettings[]
            section.ApplicationSettings[0].Value = "Name1";
            //section.FolderItems[0].Path = "C:\\Nanook";
            cfg.Save(); //устанавливает перенос на новую строку и производит проверку <exename>.vshost.exe.config файла в вашей отладочной папке.





            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }


        }
    }
}