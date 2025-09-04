//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace CommonUtils.Settings
//{
//    public static class SettingsData
//    {
//        public static string SettingFilePath { get; set; }
//        static SettingsData()
//        {
//            //AppDomain.CurrentDomain.BaseDirectory
//            var fi = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
//            SettingFilePath = Path.Combine(fi.DirectoryName, "AppData", "settings.xml");
//        }
//    }
//}
