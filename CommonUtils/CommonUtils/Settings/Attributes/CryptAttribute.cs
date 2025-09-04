namespace CommonUtils.Settings.Attributes
{
    /// <summary>
    /// Указывает, должно ли свойство/поле шифроваться при сохранении
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CryptAttribute : Attribute
    {
        /// <summary>
        /// Получает значение, указывающее, требуется ли шифрование
        /// </summary>
        public bool IsCrypt { get; }

        /// <summary>
        /// Инициализирует новый экземпляр атрибута шифрования
        /// </summary>
        /// <param name="isCrypt">Требуется ли шифрование</param>
        public CryptAttribute(bool isCrypt)
        {
            IsCrypt = isCrypt;
        }


    }

}
