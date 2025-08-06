using System;
using System.Collections.Generic;
using CommonUtils.Helpers;

namespace CommonUtils.Customization
{
    /// <summary>
    /// Abstract base class for all customization providers.
    /// </summary>
    public abstract class CustomizationProviderAbstract : ICustomizationProvider
    {
        /// <summary>
        /// List of customization interfaces.
        /// </summary>
        protected static readonly List<Type> CustomizationInterfaces;

        static CustomizationProviderAbstract()
        {
            CustomizationInterfaces = TypeHelper.GetInrefaces<ICustomization>(TypeHelper.GetAllAssemblies());
        }

        /// <summary>
        /// Initialize method. For calling static constructor.
        /// </summary>
        public static void Initialize()
        {

        }

        /// <summary>
        /// Gets the need to apply customization flag.
        /// </summary>
        public abstract bool IsApplies(ICustomization sender);

        /// <summary>
        /// Gets actual value.
        /// </summary>
        public virtual object GetActualValue(string name, string value)
        {
            foreach (var @interface in CustomizationInterfaces)
            {
                var property = @interface.GetProperty(name);
                if (property == null)
                    continue;

                var actualType = property.PropertyType;
#if NET461 || NET47 || NET471 || NET472
                if (actualType == typeof(System.Windows.Media.ImageSource))
                    return ImageProvider.GetImageSource(value);
                if (actualType == typeof(System.Windows.Media.Brush) || actualType.IsSubclassOf(typeof(System.Windows.Media.Brush)))
                    return new System.Windows.Media.SolidColorBrush(System.Drawing.ColorTranslator.FromHtml(value).ToMediaColor());
#endif
                if (actualType == typeof(double))
                    return Convert.ToDouble(value);
                if (actualType.IsEnum)
                    return Enum.Parse(actualType, value);
                if (actualType.IsValueType)
                    return ObjectConverter.FromString(value, actualType);
            }

            return null;
        }

        /// <summary>
        /// Applies customization info for specifed control.
        /// </summary>
        public abstract void LoadCustomization(ICustomization sender, List<CustomizationInfo> customizationInfos);
    }
}
