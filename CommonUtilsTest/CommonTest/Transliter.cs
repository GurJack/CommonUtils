using System.Collections.Generic;
using System.Linq;

namespace CommonUtils
{
    public static class Transliter
    {
        private static readonly Dictionary<string, string> Words;
        private static readonly Dictionary<string, string> WordsUrl;

        static Transliter()
        {
            Words=new Dictionary<string, string>
            {
                {"а", "a"},
                {"б", "b"},
                {"в", "v"},
                {"г", "g"},
                {"д", "d"},
                {"е", "e"},
                {"ё", "yo"},
                {"ж", "zh"},
                {"з", "z"},
                {"и", "i"},
                {"й", "j"},
                {"к", "k"},
                {"л", "l"},
                {"м", "m"},
                {"н", "n"},
                {"о", "o"},
                {"п", "p"},
                {"р", "r"},
                {"с", "s"},
                {"т", "t"},
                {"у", "u"},
                {"ф", "f"},
                {"х", "h"},
                {"ц", "c"},
                {"ч", "ch"},
                {"ш", "sh"},
                {"щ", "sch"},
                {"ъ", "j"},
                {"ы", "i"},
                {"ь", "j"},
                {"э", "ye"},
                {"ю", "yu"},
                {"я", "ya"},
                {"А", "A"},
                {"Б", "B"},
                {"В", "V"},
                {"Г", "G"},
                {"Д", "D"},
                {"Е", "E"},
                {"Ё", "Yo"},
                {"Ж", "Zh"},
                {"З", "Z"},
                {"И", "I"},
                {"Й", "J"},
                {"К", "K"},
                {"Л", "L"},
                {"М", "M"},
                {"Н", "N"},
                {"О", "O"},
                {"П", "P"},
                {"Р", "R"},
                {"С", "S"},
                {"Т", "T"},
                {"У", "U"},
                {"Ф", "F"},
                {"Х", "H"},
                {"Ц", "C"},
                {"Ч", "Ch"},
                {"Ш", "Sh"},
                {"Щ", "Sch"},
                {"Ъ", "J"},
                {"Ы", "I"},
                {"Ь", "J"},
                {"Э", "Ye"},
                {"Ю", "Yu"},
                {"Я", "Ya"},
                {" ", "_"},
                {"+", "Plus"}
            };
            WordsUrl = new Dictionary<string, string>
            {
                {"\\", "-"},
                {"\"", ""},
                {"/", "-"},
                {"\0", "\\0"},
                {"'", "''"},
                {".", "_"},
                {"!", ""},
                {"~", ""},
                {"?", ""},
                {"{", ""},
                {"}", ""},
                {"(", ""},
                {")", ""},
                {",", "_"}
            };
        }

        public static string GetTranslit(string value)
        {
            return Words.Aggregate(value, (current, pair) => current.Replace(pair.Key, pair.Value));
        }

        public static string GetUrl(string value)
        {
            return WordsUrl.Aggregate(GetTranslit(value), (current, pair) => current.Replace(pair.Key, pair.Value)).ToLower();
            
        }
    }
}
