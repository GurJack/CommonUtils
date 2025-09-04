//using System;
//using System.IO;
//using System.Xml;
//using System.Xml.Serialization;

//namespace CommonUtils.Helpers
//{
//    public static class SerializeHelper
//    {
//        public static string SerializeObject<T>(T value) where T: class 
//        {
//            if (value == null) return null;

//            var serializer = new XmlSerializer(value.GetType());

//            using (var textWriter = new StringWriter())
//            {
//                using (var xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
//                {
//                    serializer.Serialize(xmlWriter, value);
//                }
//                return textWriter.ToString();
//            }
//        }


//        public static void SerializeObjectToFile<T>(T value, string fileName) where T : class
//        {
//            if (value == null) 
//                throw new ApplicationException("Value is null");
//            if (string.IsNullOrEmpty(fileName))
//                throw new ApplicationException("Путь к файлу не задан");
            
//            var serializer = new XmlSerializer(typeof(T));

//            using (var file = File.Create(fileName))
//            {
//                using (var xmlWriter = XmlWriter.Create(file, new XmlWriterSettings { OmitXmlDeclaration = true }))
//                {
//                    serializer.Serialize(xmlWriter, value);
//                }
//                file.Close();
//            }

//        }


//        public static T DeserializeObjectFromString<T>(string xml) where T : class 
//        {
//            if (string.IsNullOrEmpty(xml)) return null;

//            var serializer = new XmlSerializer(typeof(T));

//            using (var textReader = new StringReader(xml))
//            {
//                using (var xmlReader = XmlReader.Create(textReader))
//                {
//                    return (T)serializer.Deserialize(xmlReader);
//                }
//            }
//        }

//        public static T DeserializeObjectFromFile<T>(string fileName) where T : class
//        {
//            if (string.IsNullOrEmpty(fileName)) return null;
//            if (!File.Exists(fileName)) return null;
//            var serializer = new XmlSerializer(typeof(T));
//            T result;

//            using (var file = new StreamReader(fileName))
//            using (var xmlReader = XmlReader.Create(file))
//            {
//                result = (T) serializer.Deserialize(xmlReader);
//                file.Close();
//            }

//            return result;
//        }
//    }
//}