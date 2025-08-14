namespace CommonUtils.Settings.Attributes
{
    /// <summary>
    /// Указывает, должно ли свойство/поле шифроваться при сохранении
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CryptAttribute : Attribute
    {
        /// <param name="isCrypt">Требуется ли шифрование</param>
        public bool IsCrypt { get; }

        public CryptAttribute(bool isCrypt)
        {
            IsCrypt = isCrypt;
        }

        
    }

}
