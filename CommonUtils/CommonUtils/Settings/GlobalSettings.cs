using CommonUtils.Settings.Attributes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommonUtils.Settings
{
    /// <summary>
    /// Глобальные настройки приложения, доступные всем модулям
    /// </summary>
    public class GlobalSettings : BaseSettings, INotifyPropertyChanged
    {
        private string _theme;
        private string _apiKey;
        
        /// <summary>
        /// Путь к исполняемому файлу программы (не сохраняется в файл настроек)
        /// </summary>
        [DoNotSaveToFile]
        public string ProgramPath { get; set; }

        /// <summary>
        /// API Key
        /// </summary>
        [DisplaySettings(
        DisplayName = "API Key",
        Order = 1,
        Category = "API Settings",
        Description = "Секретный ключ для доступа к API")]
        [Crypt(true)]
        public string ApiKey
        {
            get => _apiKey;
            set => SetField(ref _apiKey, value);
        }

        /// <summary>
        /// Текущая тема оформления
        /// </summary>
        public string Theme
        {
            get => _theme;
            set => SetField(ref _theme, value);
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        public override void InitializeDefaultValues()
        {
            _theme = "Office 2019 Colorful";
            _apiKey= "default-key";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
