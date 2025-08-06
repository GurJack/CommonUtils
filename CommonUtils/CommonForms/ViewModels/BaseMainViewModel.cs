using CommonUtils.Exceptions;
using CommonUtils.Loggers;
using CommonUtils.Loggers.Appenders;
using CommonUtils.Settings;
using CommonUtils.Settings.Providers;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonForms.ViewModels
{
    public abstract class BaseMainViewModel<T> where T : BaseSettings, new()
    {
        protected readonly SettingsFile<T> SettingsFile;
        private readonly  T _programSettings;
        private readonly LogUtils _logger;
        //private bool _stop;
        //public bool SettingsFileNotFound { get; set; }
        public T Settings => _programSettings;
        public LogUtils Logger => _logger;
        public BaseMainViewModel() 
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            var fi = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
            SettingsFile = new SettingsFile<T>(fi.DirectoryName + "\\AppData\\" + "settings.xml");
            _logger = new LogUtils(GetType());
            _logger.AppenderList.Add(new ConfigureFile());
            _logger.InitLogger();
            _programSettings = new T();
            _programSettings.ProgramPath = fi.DirectoryName;



        }
        public void LoadParams()
        {
            //string dirName = _programSettings.ProgramPath;
            if (File.Exists(_programSettings.ProgramPath + "\\AppData\\" + "settings.xml"))
                SettingsFile.LoadParams(false, _programSettings);
            else
            {
                SettingsFile.LoadParams(true, _programSettings);
                //_programSettings.ProgramPath = dirName;
                SetEmptyParams();
                
                SettingsFile.SaveParams(_programSettings);
            }
            //_programSettings.ProgramPath = dirName;
        }
        public abstract ErrorInfo Init();
        public abstract void SetEmptyParams();
        
    }
}
