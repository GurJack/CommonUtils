using CommonUtils.Security;
using CommonUtils.Settings.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;

namespace CommonUtils.Settings
{
    /// <summary>
    /// Кастомный резолвер для сериализации настроек
    /// </summary>
    public class SettingsContractResolver : DefaultContractResolver
    {
        private readonly string _encryptionKey;

        public SettingsContractResolver(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            // Игнорируем свойства с атрибутом DoNotSaveToFile
            if (member.GetCustomAttribute<DoNotSaveToFileAttribute>() != null)
            {
                property.Ignored = true;
            }

            // Шифрование значений
            var cryptAttr = member.GetCustomAttribute<CryptAttribute>();
            if (cryptAttr?.IsCrypt == true && property.PropertyType == typeof(string))
            {
                property.ValueProvider = new EncryptedValueProvider(property.ValueProvider, _encryptionKey);
            }

            //// Преобразование имен полей для SQL
            //if (GlobalConstant.UseDataBase)
            //{
            //    property.PropertyName = $"_{char.ToLower(property.PropertyName[0])}{property.PropertyName.Substring(1)}";
            //}

            return property;
        }

        private sealed class EncryptedValueProvider : IValueProvider
        {
            private readonly IValueProvider _innerProvider;
            private readonly string _encryptionKey;

            public EncryptedValueProvider(IValueProvider innerProvider, string encryptionKey)
            {
                _innerProvider = innerProvider;
                _encryptionKey = encryptionKey;
            }

            public object GetValue(object target)
            {
                var value = _innerProvider.GetValue(target) as string;
                if (string.IsNullOrEmpty(value)) return value;

                try
                {
                    return SecureEncryptionHelper.Encrypt(value, _encryptionKey);
                }
                catch
                {
                    return value; // Возвращаем исходное значение при ошибке
                }
            }

            public void SetValue(object target, object value)
            {
                if (value is string encrypted)
                {
                    try
                    {
                        value = SecureEncryptionHelper.Decrypt(encrypted, _encryptionKey);
                    }
                    catch
                    {
                        // Оставляем зашифрованное значение
                    }
                }
                _innerProvider.SetValue(target, value);
            }
        }


    }
}
