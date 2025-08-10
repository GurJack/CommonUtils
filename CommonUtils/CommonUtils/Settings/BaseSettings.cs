using CommonUtils.Settings.Attributes;

namespace CommonUtils.Settings
{

    /// <summary>
    /// Базовый класс для настроек приложения
    /// </summary>
    public abstract class BaseSettings
    {
        /// <summary>
        /// Путь к исполняемому файлу программы (не сохраняется в файл настроек)
        /// </summary>
        [DoNotSaveToFile]
        public string ProgramPath { get; set; }
        [DoNotSaveToFile]
        public string Login { get; set; }
        public abstract void InitializeDefaultValues();
        public BaseSettings()
        {
            InitializeDefaultValues();
        }
    }
}
