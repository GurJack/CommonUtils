using System.Collections.Generic;

namespace CommonUtils.Customization
{
    /// <summary>
    /// Interface for user-interface customization provider.
    /// </summary>
    public interface ICustomizationProvider
    {
        /// <summary>
        /// Gets the need to apply customization flag.
        /// </summary>
        bool IsApplies(ICustomization sender);

        /// <summary>
        /// Gets actual value.
        /// </summary>
        object GetActualValue(string name, string value);

        /// <summary>
        /// Applies customization info for specifed control.
        /// </summary>
        void LoadCustomization(ICustomization sender, List<CustomizationInfo> customizationInfos);
    }
}
