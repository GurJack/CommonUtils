using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace CommonForms.Module
{
    public interface IModule
    {
        string ModuleName { get; }
        string DisplayName { get; }
        string ModuleVersion { get; }
        string ModuleDescription { get; }
        /// <summary>
        /// Вызывается при изменении глобальных настроек
        /// </summary>
        void OnGlobalSettingsChanged(GlobalSettings settings);
        int Order { get; }
        //AppCore AppCore { get; private set; }
        void Initialize(AppCore appCore);
        /// <summary>
        /// Установка параметров программы/модуля
        /// </summary>
        void InitModule();
        RibbonPage CreateRibbonPage(RibbonBarItems barItems);
        
        XtraUserControl CreateSettingsControl();
        /// <summary>
        /// Тип настроек модуля
        /// </summary>
        Type SettingsType { get; }

        /// <summary>
        /// Возвращает текущие настройки модуля
        /// </summary>
        object GetSettings();

        /// <summary>
        /// Применяет настройки к модулю
        /// </summary>
        void ApplySettings(object settings);
    }
}
