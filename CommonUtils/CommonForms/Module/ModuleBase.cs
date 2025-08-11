using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.Serialization;
using CommonUtils.Settings;
using CommonUtils.Settings.Attributes;
using System.Reflection;

namespace CommonForms.Module
{
    /// <summary>
    /// Базовый класс для модулей с поддержкой настроек
    /// </summary>
    /// <typeparam name="T">Тип настроек модуля (наследник BaseSettings)</typeparam>
    public abstract class ModuleBase<T>: IModule where T : BaseSettings, new()
    {
        protected AppCore AppCore { get; private set; }

        private readonly T _moduleSettings = new T();
        /// <summary>
        /// Настройки модуля
        /// </summary>
        public T Settings => _moduleSettings;

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public T DefaultSettings { get; protected set; }
        public abstract string ModuleName { get; }
        public abstract string DisplayName { get; }
        public abstract string ModuleVersion { get; }
        public abstract string ModuleDescription { get; }
        public Type SettingsType => typeof(T);

        public object GetSettings() => _moduleSettings;

        public abstract int Order { get; }

        public virtual void Initialize(AppCore appCore)
        {
            AppCore = appCore;
        }
        
        public virtual RibbonPage CreateRibbonPage(RibbonBarItems barItems)
        {
            RibbonPage page = new RibbonPage(DisplayName);
            
            return page;
        }

        public virtual XtraUserControl CreateSettingsControl()
        {
            //return new ModuleSettingsControl(this);
            return null;
            
        }

        protected BarButtonItem CreateButtonItem(string caption, Action action,
            BarItemPaintStyle style = BarItemPaintStyle.CaptionGlyph)
        {
            var btn = new BarButtonItem
            {
                Caption = caption,
                PaintStyle = style
            };
            btn.ItemClick += (s, e) => SafeExecute(action);
            return btn;
        }

        /// <summary>
        /// Безопасное выполнение действия с обработкой ошибок
        /// </summary>
        protected void SafeExecute(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                LoggerHandler.Error(ex, $"Ошибка в модуле {ModuleName}");
            }
        }

        public void ApplySettings(object settings)
        {
            if (settings is T typedSettings)
            {
                // Копируем значения свойств
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.CanWrite && prop.GetCustomAttribute<DoNotSaveToFileAttribute>() == null)
                    {
                        prop.SetValue(_moduleSettings, prop.GetValue(typedSettings));
                    }
                }
            }
        }

        public abstract void InitModule();

        public virtual void OnGlobalSettingsChanged(GlobalSettings settings)
        {
            
        }

    }
}
