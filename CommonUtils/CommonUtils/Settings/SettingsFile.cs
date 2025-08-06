using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using CommonUtils.Settings.Attributes;


namespace CommonUtils.Settings
{

    public class SettingsFile<T> where T : new()
    {
        private static IMapper _Mapper;
        private static MapperConfiguration _MapperConfig;
        private readonly string _fileName;
        static SettingsFile()
        {
            _MapperConfig = new MapperConfiguration(
                cfg => cfg.CreateMap<T, T>(MemberList.None));
            _MapperConfig.AssertConfigurationIsValid();
            _Mapper = _MapperConfig.CreateMapper();
            
        }
        public SettingsFile(string fileName)
        {
            _fileName = fileName.Trim();
            if (!Attribute.IsDefined(typeof(T), typeof(SerializableAttribute)))
                throw new ApplicationException($"Класс {typeof(T).Name} не Serializable.");
            
            
        }

        private void CreateEmptyFile()
        {
            FileInfo file = new FileInfo(_fileName);
            if (file.Exists)
                return;
            if (!Directory.Exists(file.DirectoryName))
            {
                Directory.CreateDirectory(file.DirectoryName);
            }
            var start = new T();
            SaveParams(start);
        }
        public bool IsFileExist()
        {
            FileInfo file = new FileInfo(_fileName);
            return file.Exists;
                
        }

        public T LoadParams(bool isCrete)
        {
            if (!File.Exists(_fileName))
            {
                if(isCrete)
                    CreateEmptyFile();
                else
                    throw new ApplicationException($"Файл с параметрами не найден {_fileName}");

            }
                
            T result;
            var reader = new XmlSerializer(typeof(T));
            var file = new StreamReader(_fileName);
            result = (T)reader.Deserialize(file);
            file.Close();

            var type = result.GetType();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(propertyInfo, typeof(CryptAttribute)) )
                    continue;
                var attributeValue = Attribute.GetCustomAttribute(propertyInfo, typeof(CryptAttribute)) as CryptAttribute;
                if (!attributeValue.IsCrypt)
                    continue;
                if (propertyInfo.PropertyType.Name.ToLower() != "string")
                    throw new ApplicationException($"Кодированное свойство должно быть string. Поле {propertyInfo.Name} имеет тип {propertyInfo.PropertyType.Name}");
                string str = (string)propertyInfo.GetValue(result, null);
                if (!String.IsNullOrWhiteSpace(str))
                    propertyInfo.SetValue(result, Crypter.Decrypt1(str), null);
            }
            foreach (var fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(fieldInfo, typeof(CryptAttribute)))
                    continue;
                var attributeValue = Attribute.GetCustomAttribute(fieldInfo, typeof(CryptAttribute)) as CryptAttribute;
                if (!attributeValue.IsCrypt)
                    continue;
                if (fieldInfo.FieldType.Name.ToLower() != "string")
                    throw new ApplicationException($"Кодированное поле должно быть string. Поле {fieldInfo.Name} имеет тип {fieldInfo.FieldType.Name}");

                string str = (string)fieldInfo.GetValue(result);
                if (!String.IsNullOrWhiteSpace(str))
                    fieldInfo.SetValue(result, Crypter.Decrypt1(str));

            }

            return result;
        }

        public void LoadParams(bool isCrete, T settings)
        {
            var res = LoadParams(isCrete);
            var typeFrom = res.GetType();
            var typeTo = settings.GetType();
            foreach (var propertyInfo in typeFrom.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(propertyInfo, typeof(CryptAttribute)) && !Attribute.IsDefined(propertyInfo, typeof(ParamAttribute)))
                    continue;
                var propTo = typeTo.GetProperty(propertyInfo.Name);
                if (propTo != null)
                {
                    propTo.SetValue(settings, (string)propertyInfo.GetValue(res, null));
                }
                
                
            }
            foreach (var fieldInfo in typeFrom.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(fieldInfo, typeof(CryptAttribute)) && !Attribute.IsDefined(fieldInfo, typeof(ParamAttribute)))
                    continue;
                var fieldTo = typeTo.GetField(fieldInfo.Name);
                if (fieldTo != null)
                {
                    fieldTo.SetValue(settings, (string)fieldInfo.GetValue(res));
                }
                

            }
        }

        public void SetPassword(string pass)
        {
            Crypter.CalcKey(pass);
        }
        public void SaveParams(T value)
        {
            //var saveResult = _Mapper.Map<T, T>(value);
            var saveResult = new T();
            var typeFrom = value.GetType();
            var typeTo = saveResult.GetType();
            foreach (var propertyInfo in typeFrom.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(propertyInfo, typeof(CryptAttribute)) && !Attribute.IsDefined(propertyInfo, typeof(ParamAttribute)))
                    continue;
                var propTo = typeTo.GetProperty(propertyInfo.Name);
                if (propTo != null)
                {
                    propTo.SetValue(saveResult, (string)propertyInfo.GetValue(value, null));
                }


            }
            foreach (var fieldInfo in typeFrom.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(fieldInfo, typeof(CryptAttribute)) && !Attribute.IsDefined(fieldInfo, typeof(ParamAttribute)))
                    continue;
                var fieldTo = typeTo.GetField(fieldInfo.Name);
                if (fieldTo != null)
                {
                    fieldTo.SetValue(saveResult, (string)fieldInfo.GetValue(value));
                }


            }





            var type = saveResult.GetType();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(propertyInfo, typeof(CryptAttribute)))
                    continue;
                var attributeValue = Attribute.GetCustomAttribute(propertyInfo, typeof(CryptAttribute)) as CryptAttribute;
                if (!attributeValue.IsCrypt)
                    continue;
                if (propertyInfo.PropertyType.Name.ToLower() != "string")
                    throw new ApplicationException($"Кодированное свойство должно быть string. Поле {propertyInfo.Name} имеет тип {propertyInfo.PropertyType.Name}");
                string str = (string) propertyInfo.GetValue(saveResult, null);
                if(!String.IsNullOrWhiteSpace(str))
                    propertyInfo.SetValue(saveResult, Crypter.Encrypt1(str), null);
            }
            foreach (var fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!Attribute.IsDefined(fieldInfo, typeof(CryptAttribute)))
                    continue;
                var attributeValue = Attribute.GetCustomAttribute(fieldInfo, typeof(CryptAttribute)) as CryptAttribute;
                if (!attributeValue.IsCrypt)
                    continue;
                if (fieldInfo.FieldType.Name.ToLower() != "string")
                    throw new ApplicationException($"Кодированное поле должно быть string. Поле {fieldInfo.Name} имеет тип {fieldInfo.FieldType.Name}");
                string str = (string)fieldInfo.GetValue(saveResult);
                if (!String.IsNullOrWhiteSpace(str))
                    fieldInfo.SetValue(saveResult, Crypter.Encrypt1(str));

            }

            var writer =
               new XmlSerializer(typeof(T));

            //var fi = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
            //if (!Directory.Exists(fi.DirectoryName))
            //    Directory.CreateDirectory(fi.DirectoryName);


            var file = File.Create(_fileName);

            

            writer.Serialize(file, saveResult);
            file.Close();


        }
    }
}
