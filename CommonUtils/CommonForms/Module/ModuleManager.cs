using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonForms.Module
{
    public class ModuleManager
    {
        private readonly AppCore _appCore;
        private readonly List<IModule> _modules = new List<IModule>();

        public IEnumerable<IModule> Modules => _modules.AsReadOnly().OrderBy(w => (w.ModuleName == "Базовый модуль") ? 0 : 1)
                .ThenBy(p => Math.Abs(p.Order + 1)).ThenBy(q => q.ModuleName);

        public ModuleManager(AppCore appCore)
        {
            _appCore = appCore;
        }

        public void InitModules()
        {
            try
            {
                foreach (IModule module in Modules)
                    module.InitModule();
            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex, "Ошибка инициализации модулей");
            }
            
            
        }

        public void LoadModules()
        {
            try
            {
                //string modulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
                string modulesPath = AppDomain.CurrentDomain.BaseDirectory;
                if (!Directory.Exists(modulesPath)) Directory.CreateDirectory(modulesPath);

                foreach (string file in Directory.GetFiles(modulesPath, "module*.dll"))
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(file);
                        LoadModulesFromAssembly(assembly);
                    }
                    catch (Exception ex)
                    {
                        LoggerHandler.Fatal(ex, $"Ошибка загрузки сборки {file}");
                    }
                }
                
            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex, "Ошибка загрузки модулей");
            }
        }

        private void LoadModulesFromAssembly(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract))
            {
                try
                {
                    IModule module = Activator.CreateInstance(type) as IModule;
                    module.Initialize(_appCore);
                    _modules.Add(module);
                    //module.InitModule();
                    LoggerHandler.Info($"Загружен модуль: {module.ModuleName} v{module.ModuleVersion}");
                }
                catch (Exception ex)
                {
                    LoggerHandler.Fatal (ex, $"Ошибка создания модуля {type.Name}");
                }
            }
        }
    }
}
