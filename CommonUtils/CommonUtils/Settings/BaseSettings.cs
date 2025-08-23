using CommonUtils.Settings.Attributes;

namespace CommonUtils.Settings
{

    /// <summary>
    /// Базовый класс для настроек приложения
    /// </summary>
    public abstract class BaseSettings
    {
        
        public abstract void InitializeDefaultValues();
        public BaseSettings()
        {
            InitializeDefaultValues();
        }
    }
}
