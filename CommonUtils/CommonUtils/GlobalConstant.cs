namespace CommonUtils
{
    /// <summary>
    /// Глобальные константы и настройки приложения
    /// </summary>

    public static class GlobalConstant
    {
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