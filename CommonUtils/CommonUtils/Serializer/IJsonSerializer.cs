using System.Threading.Tasks;

namespace CommonUtils.Serializer
{
    /// <summary>
    /// The json serializer.
    /// </summary>
    public interface IJsonSerializer
    {
        /// <summary>
        /// Converts to json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        string ToJsonString<T>(T value);

        /// <summary>
        /// Async converts to json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<string> ToJsonStringAsync<T>(T value);

        /// <summary>
        /// Converts from json to object.
        /// </summary>
        object FromJson(string json);

        /// <summary>
        /// Converts from json to object.
        /// </summary>
        T FromJson<T>(string json);

        /// <summary>
        /// Async converts from json to object.
        /// </summary>
        Task<T> FromJsonAsync<T>(string json);
    }
}