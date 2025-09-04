using System;
using System.IO;
using System.Text;

namespace CommonUtils
{
    /// <summary>
    /// Глобальные константы и настройки приложения
    /// </summary>
    public static class GlobalConstant
    {
        /// <summary>
        /// Имя приложения
        /// </summary>
        public static string ApplicationName { get; set; } = "CommonUtils";

        /// <summary>
        /// Версия приложения
        /// </summary>
        public static string Version { get; set; } = "1.0.0";

        /// <summary>
        /// Строка подключения по умолчанию
        /// </summary>
        public static string DefaultConnectionString { get; set; } = "Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;AttachDbFilename=|DataDirectory|\\CommonUtils.mdf;Initial Catalog=CommonUtils;";

        /// <summary>
        /// Формат даты и времени
        /// </summary>
        public static string DateTimeFormat { get; set; } = "dd.MM.yyyy HH:mm:ss";

        /// <summary>
        /// Формат даты
        /// </summary>
        public static string DateFormat { get; set; } = "dd.MM.yyyy";

        /// <summary>
        /// Формат времени
        /// </summary>
        public static string TimeFormat { get; set; } = "HH:mm:ss";

        /// <summary>
        /// Кодировка по умолчанию
        /// </summary>
        public static Encoding DefaultEncoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// Максимальное количество попыток
        /// </summary>
        public static int MaxRetryAttempts { get; set; } = 3;

        /// <summary>
        /// Таймаут по умолчанию
        /// </summary>
        public static int DefaultTimeout { get; set; } = 30000;

        /// <summary>
        /// Временная директория
        /// </summary>
        public static string TempDirectory { get; set; } = Path.GetTempPath();

        /// <summary>
        /// Директория логов
        /// </summary>
        public static string LogDirectory { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        /// <summary>
        /// Путь к каталогу данных приложения
        /// </summary>
        public static string AppDataPath { get; set; } = "AppData";

        /// <summary>
        /// Ключ API по умолчанию, используется для шифрования при отключенной БД
        /// </summary>
        public static string APIKey { get; set; } = "klkdf[pogjBCFLIhfcder89766jjnlolomng333^&*gbo_)ASHGM";

        /// <summary>
        /// Флаг использования базы данных
        /// </summary>
        /// <remarks>
        /// Если true - настройки хранятся в БД
        /// Если false - настройки хранятся в файле
        /// </remarks>
        public static bool UseDataBase { get; set; } = true;

        /// <summary>
        /// Название главной формы приложения
        /// </summary>
        public static string MainFormName { get; set; } = "База знаний";

        /// <summary>
        /// Путь к файлу базы данных (актуально при UseDataBase = true)
        /// </summary>
        public static string DatabaseFilePath { get; set; } = "KnolageBaseDB.mdf";

        /// <summary>
        /// Название базы данных (актуально при UseDataBase = true)
        /// </summary>
        public static string DatabaseName { get; set; } = "KnolageBaseDB";
        /// <summary>
        /// Путь к файлу базы данных (актуально при UseDataBase = true)
        /// </summary>
        public static string DatabaseDirectoryPath { get; set; } = "Database";


        /// <summary>
        /// Путь к файлу настроек (актуально при UseDataBase = false)
        /// </summary>
        public static string SettingsFilePath { get; set; } = "appsettings.json";

        /// <summary>
        /// Получает строку подключения к базе данных
        /// </summary>
        public static string GetConnectionString()
        {
            return $@"Server=(LocalDB)\MSSQLLocalDB; Initial Catalog={GlobalConstant.DatabaseName};
                      AttachDbFilename=
            {Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GlobalConstant.AppDataPath, GlobalConstant.DatabaseDirectoryPath, GlobalConstant.DatabaseFilePath)};
                      Integrated Security=True;
                      Connect Timeout=130
            ";
        }

    }
}
