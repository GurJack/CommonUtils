using System;
using System.Collections;
using System.Collections.Generic;

namespace CommonUtils.Customization
{
    /// <summary>
    /// Helper for customization UI.
    /// </summary>
    public static class CustomizationHelper
    {
        /// <summary>
        /// List of <see cref="ICustomizationProvider"/> instances.
        /// </summary>
        private static readonly List<ICustomizationProvider> CustomizationProviderList = new List<ICustomizationProvider>();

        static CustomizationHelper()
        {
            CustomizationHelper.AddCustomizationProvider(new DefaultCustomizationProvider());
        }

        /// <summary>
        /// Inserts customization provider into the top of the list.
        /// </summary>
        /// <param name="customizationProvider">Implementation of the <see cref="ICustomizationProvider"/>.</param>
        public static void AddCustomizationProvider(ICustomizationProvider customizationProvider)
        {
            if (customizationProvider == null)
            {
                throw new ArgumentNullException(nameof(customizationProvider));
            }

            lock (((ICollection) CustomizationProviderList).SyncRoot)
            {
                CustomizationProviderList.Insert(0, customizationProvider);
            }
        }

        /// <summary>
        /// Removes customization provider from the list.
        /// </summary>
        /// <param name="customizationProvider">Implementation of the <see cref="ICustomizationProvider"/>.</param>
        public static void RemoveCustomizationProvider(ICustomizationProvider customizationProvider)
        {
            if (customizationProvider == null)
            {
                throw new ArgumentNullException(nameof(customizationProvider));
            }

            lock (((ICollection) CustomizationProviderList).SyncRoot)
            {
                if (CustomizationProviderList.Contains(customizationProvider))
                {
                    CustomizationProviderList.Remove(customizationProvider);
                }
            }
        }

        /// <summary>
        /// Gets actual value.
        /// </summary>
        public static object GetActualValue(string name, string value)
        {
            lock (((ICollection) CustomizationProviderList).SyncRoot)
            {
                foreach (var customizationProvider in CustomizationProviderList)
                {
                    return customizationProvider.GetActualValue(name, value);
                }
            }

            return null;
        }

        /// <summary>
        /// Applies customization info for specifed control.
        /// </summary>
        public static void LoadCustomization(ICustomization sender, List<CustomizationInfo> customizationInfos)
        {
            lock (((ICollection) CustomizationProviderList).SyncRoot)
            {
                foreach (var customizationProvider in CustomizationProviderList)
                {
                    if (customizationProvider.IsApplies(sender))
                    {
                        customizationProvider.LoadCustomization(sender, customizationInfos);
                    }
                }
            }
        }
    }
}