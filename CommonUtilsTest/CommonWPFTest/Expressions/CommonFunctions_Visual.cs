//using System;
//using CommonUtils;
//using CommonUtils.Helpers;
//using CommonUtils.Expressions;

//namespace CommonWPF.Expressions
//{
//    /// <summary>
//    /// The second part of the class. It contains functions that cannot be translated to SQL. 
//    /// </summary>
//    public static class CFF
//    {
//        /// <summary>
//        /// Gets the name of the caller method.
//        /// </summary>
//        /// <param name="memberName"></param>
//        /// <returns></returns>
//        public static string GetCaller([System.Runtime.CompilerServices.CallerMemberName]
//            string memberName = "")
//        {
//            return memberName;
//        }

//        private static bool ShowMessageBox(object owner, string messageBoxText, string caption, CommonMessageBoxImage messageBoxImage,
//            [System.Runtime.CompilerServices.CallerMemberName]
//            string functionName = "")
//        {
//            var func = CF.GetFunction(functionName);
//            if (func == null)
//            {
//                throw new NotSupportedException(functionName);
//            }

//            return ThreadHelper.ExecuteUI<bool>(() => (bool) func(owner, messageBoxText, caption, messageBoxImage));
//        }

//        /// <summary>
//        /// Displays a message box that has a message.
//        /// </summary>
//        /// <param name="messageBoxText">The message.</param>
//        /// <param name="messageBoxImage">The default value is <see cref="CommonMessageBoxImage.Information"/></param>
//        public static void MessageBox(string messageBoxText, CommonMessageBoxImage messageBoxImage = CommonMessageBoxImage.Information)
//        {
//            ShowMessageBox(null, messageBoxText, Information.ProductNameWithMajorVersion, messageBoxImage);
//        }

//        /// <summary>
//        /// Displays a message box that has a message.
//        /// </summary>
//        /// <param name="messageBoxText">the message.</param>
//        /// <param name="caption">The default value is <see cref="Information.ProductNameWithMajorVersion"/></param>
//        /// <param name="messageBoxImage">The default value is <see cref="CommonMessageBoxImage.Information"/></param>
//        public static void MessageBox(string messageBoxText, string caption, CommonMessageBoxImage messageBoxImage = CommonMessageBoxImage.Information)
//        {
//            ShowMessageBox(null, messageBoxText, caption, messageBoxImage);
//        }

//        /// <summary>
//        /// Displays a message box that has a message.
//        /// </summary>
//        /// <param name="owner">The default value is <see cref="Information.ProductNameWithMajorVersion"/></param>
//        /// <param name="messageBoxText">the message.</param>
//        /// <param name="caption">The default value is <see cref="Information.ProductNameWithMajorVersion"/></param>
//        /// <param name="messageBoxImage">The default value is <see cref="CommonMessageBoxImage.Information"/></param>
//        public static void MessageBox(object owner, string messageBoxText, string caption = null, CommonMessageBoxImage messageBoxImage = CommonMessageBoxImage.Information)
//        {
//            ShowMessageBox(owner, messageBoxText, caption, messageBoxImage);
//        }

//        /// <summary>
//        /// Displays a message box that has a message and that returns a boolean result.
//        /// </summary>
//        /// <param name="messageBoxText">The message.</param>
//        /// <param name="messageBoxImage">The default value is <see cref="CommonMessageBoxImage.Question"/></param>
//        /// <returns>Returns true if user will press Ok button; else returns false.</returns>
//        public static bool MessageBoxWithResult(string messageBoxText, CommonMessageBoxImage messageBoxImage = CommonMessageBoxImage.Question)
//        {
//            return ShowMessageBox(null, messageBoxText, Information.ProductNameWithMajorVersion, messageBoxImage);
//        }

//        /// <summary>
//        /// Displays a message box that has a message and that returns a boolean result.
//        /// </summary>
//        /// <param name="messageBoxText">The message.</param>
//        /// <param name="caption">The default value is <see cref="Information.ProductNameWithMajorVersion"/></param>
//        /// <param name="messageBoxImage">The default value is <see cref="CommonMessageBoxImage.Question"/></param>
//        /// <returns>Returns true if user will press Ok button; else returns false.</returns>
//        public static bool MessageBoxWithResult(string messageBoxText, string caption, CommonMessageBoxImage messageBoxImage = CommonMessageBoxImage.Question)
//        {
//            return ShowMessageBox(null, messageBoxText, caption, messageBoxImage);
//        }

//        /// <summary>
//        /// Displays a message box that has a message and that returns a boolean result.
//        /// </summary>
//        /// <param name="owner">The object that owns the current MessageBox.</param>
//        /// <param name="messageBoxText">The message.</param>
//        /// <param name="caption">The default value is <see cref="Information.ProductNameWithMajorVersion"/></param>
//        /// <param name="messageBoxImage">The default value is <see cref="CommonMessageBoxImage.Question"/></param>
//        /// <returns>Returns true if user will press Ok button; else returns false.</returns>
//        public static bool MessageBoxWithResult(object owner, string messageBoxText, string caption = null, CommonMessageBoxImage messageBoxImage = CommonMessageBoxImage.Question)
//        {
//            return ShowMessageBox(owner, messageBoxText, caption, messageBoxImage);
//        }

//        /// <summary>
//        /// Check if string is Color
//        /// </summary>
//        /// <param name="colorName"></param>
//        /// <returns></returns>
//        public static bool IsColorRgb(string colorName)
//        {
//            try
//            {

//                return System.Windows.Media.ColorConverter.ConvertFromString(colorName) is System.Windows.Media.Color;

//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }


///// <summary>
///// Determine font color based on background color
///// </summary>
///// <param name="color">background color</param>
///// <returns>font color tag(black or white)</returns>
//        public static string ContrastColor(System.Windows.Media.Color? color)
//        {
//            if (color == null)
//                return "#000000";

//            int d = 0;
//            double a = 1 - (0.299 * color.Value.R + 0.587 * color.Value.G + 0.114 * color.Value.B) / 255;
//            d = a < 0.5 ? 0 : 255;

//            var result = $"#{System.Drawing.Color.FromArgb(d, d, d).Name.Substring(2)}";
//            return result;
//        }

    
//}
//}