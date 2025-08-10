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
        int Order { get; }
        //AppCore AppCore { get; private set; }
        void Initialize(AppCore appCore);
        /// <summary>
        /// Установка параметров программы/модуля
        /// </summary>
        void InitModule();
        RibbonPage CreateRibbonPage();
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
