using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonUtils.Customization
{
    /// <summary>
    /// Default class for customization loading and applying.
    /// </summary>
    public class DefaultCustomizationProvider : CustomizationProviderAbstract
    {
        /// <summary>
        /// Gets the need to apply customization flag.
        /// </summary>
        public override bool IsApplies(ICustomization sender) => true;

        /// <summary>
        /// Applies customization info for specified control.
        /// </summary>
        public override void LoadCustomization(ICustomization sender, List<CustomizationInfo> customizationInfos)
        {
            var type = sender.GetType();
            for (var i = 0; i < customizationInfos.Count; i++)
            {
                var customizationInfo = customizationInfos[i];

                var property = type.GetProperty(customizationInfo.Name);

                //Try to find property with explicit implementation:
                if (property == null)
                {
                    var baseType = type;
                    while (property == null)
                    {
                        property = baseType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                            .SingleOrDefault(x => x.Name.EndsWith("." + customizationInfo.Name));

                        baseType = baseType.BaseType;
                        if (baseType == null)
                            break;
                    }
                }

                if (property == null)
                    continue;

                property.SetValue(sender, customizationInfo.Value);

                customizationInfos.RemoveAt(i);
                i--;
            }
        }
    }
}
