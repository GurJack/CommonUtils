using CommonUtils.Loggers.Appenders;
using CommonUtils.Loggers;
using CommonUtils.Settings;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;


namespace CommonForms.Module
{
    /// <summary>
    /// Центральное ядро приложения, обеспечивающее доступ к основным сервисам
    /// </summary>
    /// <remarks>
    /// Реализует паттерн Singleton. Инициализирует все системные сервисы
    /// в строгом порядке зависимостей.
    /// </remarks>
    public class AppCore
    {
        private static AppCore _instance;
        
        /// <summary>
        /// Экземпляр ядра приложения (Singleton)
        /// </summary>
        public static AppCore Instance => _instance ??= new AppCore();

        ///// <summary>
        ///// Контекст базы данных приложения
        ///// </summary>
        //public LawDbContext DbContext { get; private set; }

        ///// <summary>
        ///// Настройки приложения
        ///// </summary>
        public AppSettings Settings { get; private set; }

        /// <summary>
        /// Глобальные настройки приложения
        /// </summary>
        public GlobalSettings GlobalSettings { get; private set; }

        /// <summary>
        /// Менеджер модулей
        /// </summary>
        public ModuleManager ModuleManager { get; private set; }

        public string CurrentUsername { get; private set; }
        public string CurrentPasswordHash { get; private set; }
        private string _settingsPath;

        private AppCore()
        {
            //Logger = new LoggerHandler(false, false);
            LoggerHandler.Info("Инициализация логера прошла успешно");
            _settingsPath = Path.Combine((new FileInfo(Process.GetCurrentProcess().MainModule.FileName)).DirectoryName,"AppData","settings.json");
            Settings = new AppSettings();
            GlobalSettings = new GlobalSettings();

            //DbContext = new LawDbContext(Settings.DbConnectionString);
            ModuleManager = new ModuleManager(this);
        }

        /// <summary>
        /// Основной метод инициализации приложения
        /// </summary>
        public void Autorization(string username, string passwordHash)
        {
            CurrentUsername = username;
            CurrentPasswordHash = passwordHash;
        }

        /// <summary>
        /// Основной метод инициализации приложения
        /// </summary>
        public void Initialize()
        {
            
            try
            {
                LoggerHandler.Info($"Запуск инициализации для пользователя: {CurrentUsername}");

                // 1. Загрузка модулей
                ModuleManager.LoadModules();
                // 2. Загрузка настроек
                LoadSettings();
                // 3. Инициализация БД
                InitializeDatabase();

                LoggerHandler.Info("Инициализация приложения завершена успешно");
            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex, "Критическая ошибка инициализации приложения");
                
            }
        }


        /// <summary>
        /// Инициализирует базу данных
        /// </summary>
        private void InitializeDatabase()
        {
            LoggerHandler.Debug("Инициализация базы данных");
            try
            {

            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex, "Критическая ошибка инициализации приложения");
            }
            // Автоматическое создание БД при первом запуске
            //if (DbContext.Database.EnsureCreated())
            //{
            //    Logger.Info("База данных успешно создана");
            //    SeedDatabase();
            //}
            //else
            //{
            //    Logger.Info("База данных уже существует");
            //}
        }

        /// <summary>
        /// Заполняет БД начальными данными
        /// </summary>
        private void SeedDatabase()
        {
            try
            {
                LoggerHandler.Info("Заполнение базы данных начальными данными");

            //    // Создание системных иерархий
            //    var lawHierarchy = new Hierarchy { Title = "Законы", Type = HierarchyType.Law };
            //    DbContext.Hierarchies.Add(lawHierarchy);

            //    // ... код создания начальных данных ...

            //    DbContext.SaveChanges();
            //    Logger.Info("Начальные данные успешно добавлены");
            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex,"Ошибка заполнения начальными данными");
                throw;
            }
        }

        
        /// <summary>
        /// Корректно завершает работу приложения
        /// </summary>
        public void Shutdown()
        {
            LoggerHandler.Info("Завершение работы приложения");

            try
            {
                // 1. Сохранение настроек
                SaveSettings();

                // 2. Остановка фоновых процессов
                //ModuleManager.ShutdownModules();

                // 3. Освобождение ресурсов БД
                //    DbContext?.Dispose();

                // 4. Завершение логирования
                NLog.LogManager.Shutdown();
                LoggerHandler.Info("Завершение работы приложения - успешно");
            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex, "Ошибка при завершении работы");
            }
        }

        /// <summary>
        /// Загружает настройки из файла
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                if (!File.Exists(_settingsPath)) return;

                var json = File.ReadAllText(_settingsPath);
                Settings = JsonConvert.DeserializeObject<AppSettings>(json,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        ContractResolver = new SettingsContractResolver(CurrentPasswordHash)
                    });

                // Применяем настройки к модулям
                foreach (var module in ModuleManager.Modules)
                {

                    var typeName = module.SettingsType.FullName;
                    if (Settings.ModuleSettings.TryGetValue(typeName, out var settings))
                    {
                        var fi = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
                        ((BaseSettings)settings).Login = CurrentUsername;
                        ((BaseSettings)settings).ProgramPath = (new FileInfo(Process.GetCurrentProcess().MainModule.FileName)).DirectoryName;
                        module.ApplySettings(settings);
                    }
                    

                }
                SaveSettings();
            }
            catch (Exception ex)
            {
                LoggerHandler.Fatal(ex, "Ошибка загрузки настроек");
            }
        }

        /// <summary>
        /// Сохраняет текущие настройки в файл
        /// </summary>
        public void SaveSettings()
        {
            try
            {
                var directory = Path.GetDirectoryName(_settingsPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Собираем настройки со всех модулей
                Settings.ModuleSettings.Clear();
                foreach (var module in ModuleManager.Modules)
                {

                    var typeName = module.SettingsType.FullName;
                    Settings.ModuleSettings[typeName] = module.GetSettings();

                }

                var json = JsonConvert.SerializeObject(Settings, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                        ContractResolver = new SettingsContractResolver(CurrentPasswordHash)
                    });

                File.WriteAllText(_settingsPath, json);
            }
            catch (Exception ex)
            {
                LoggerHandler.Error(ex, "Ошибка сохранения настроек");
            }
        }
    }
}
