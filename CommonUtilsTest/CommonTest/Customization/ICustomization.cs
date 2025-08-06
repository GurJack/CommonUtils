using System.Collections.Generic;

namespace CommonUtils.Customization
{
    /// <summary>
    /// Base interface for controls which have customization support. 
    /// This interface has meta-system support.
    /// Name of properties are available to use in meta-system.
    /// </summary>
    public interface ICustomization
    {
        /// <summary>
        /// Loads customization.
        /// </summary>
        void LoadCustomization(List<CustomizationInfo> customizationInfos);
    }
}