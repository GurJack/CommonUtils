using System;

namespace CommonUtils.Attributes
{
    /// <summary>
    /// Атрибут для ссылки для BackLink
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BackLinkAttribute : AttributeBase
    {
               /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public BackLinkAttribute(string name)
            : base(name, false)
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get => (string) Value;
            set => Value = value;
        }
    }
}