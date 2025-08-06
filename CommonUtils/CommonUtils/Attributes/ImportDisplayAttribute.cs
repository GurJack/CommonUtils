using System;

namespace CommonUtils.Attributes
{

    /// <summary>
    /// Название для отображения при импорте данных
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ImportDisplayNameAttribute : AttributeBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public ImportDisplayNameAttribute(string name, bool required = true) : base(name)
        {
            Required = required;
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get => (string) Value;
            set => Value = value;
        }

        /// <summary>
        /// Является ли поле обязательным
        /// </summary>
        public bool Required { get; set; }
    }
}