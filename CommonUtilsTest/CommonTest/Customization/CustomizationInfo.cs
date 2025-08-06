
namespace CommonUtils.Customization
{
    /// <summary>
    /// Class for customization information.
    /// </summary>
    public class CustomizationInfo
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public CustomizationInfo(string name, string value)
        {
            Name = name;
            Value = CustomizationHelper.GetActualValue(name, value);
        }


        /// <summary>
        /// Gets or sets target property name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets value of target property.
        /// </summary>
        public object Value { get; }
    }
}