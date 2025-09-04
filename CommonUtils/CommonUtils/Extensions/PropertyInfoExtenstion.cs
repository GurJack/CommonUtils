//using System.Reflection;

//namespace CommonUtils.Extensions
//{
//    /// <summary>
//    /// PropertyInfo extenstion.
//    /// </summary>
//    public static class PropertyInfoExtenstion
//    {
//        /// <summary>
//        /// Gets the virtual (readonly) flag.
//        /// </summary>
//        /// <param name="propertyInfo"></param>
//        /// <returns></returns>
//        public static bool Virtual(this PropertyInfo propertyInfo)
//        {
//            return !(propertyInfo.CanWrite && propertyInfo.GetSetMethod( /*nonPublic*/ true).IsPublic);
//        }
//    }
//}