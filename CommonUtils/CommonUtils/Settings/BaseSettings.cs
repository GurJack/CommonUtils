using CommonUtils.Settings.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Settings
{
    
    public class BaseSettings
    {
        //[ParamAttribute("None", "ProgramPath","Путь к исполняемому файлу")]
        public string ProgramPath { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
