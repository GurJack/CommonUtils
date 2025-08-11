using CommonUtils.Settings;
using CommonUtils.Settings.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonForms.Module
{
    /// <summary>
    /// Глобальные настройки приложения, доступные всем модулям
    /// </summary>
    public class GlobalSettings : BaseSettings, INotifyPropertyChanged
    {
        private string _theme;
        private string _language;
        private bool _autoUpdate;
        private bool _showNotifications;


        /// <summary>
        /// Название программы
        /// </summary>
        [DoNotSaveToFile]
        public string ProgramName => "База знаний";

        /// <summary>
        /// Текущая тема оформления
        /// </summary>
        public string Theme
        {
            get => _theme;
            set => SetField(ref _theme, value);
        }

        /// <summary>
        /// Язык интерфейса (например, "ru-RU", "en-US")
        /// </summary>
        public string Language
        {
            get => _language;
            set => SetField(ref _language, value);
        }

        /// <summary>
        /// Включено ли автоматическое обновление
        /// </summary>
        public bool AutoUpdate
        {
            get => _autoUpdate;
            set => SetField(ref _autoUpdate, value);
        }

        /// <summary>
        /// Показывать ли системные уведомления
        /// </summary>
        public bool ShowNotifications
        {
            get => _showNotifications;
            set => SetField(ref _showNotifications, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void InitializeDefaultValues()
        {
            _theme = "Office 2019 Colorful";
            _language = "ru-RU";
            _autoUpdate = true;
            _showNotifications = true;
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
