//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CommonUtils.Settings.Providers
//{
//    /// <summary>
//    /// The application menu loader class.
//    /// </summary>
//    public interface IParamProvider
//    {
//        /// <summary>
//        /// Получить полную информацию о параметре
//        /// </summary>
//        /// <param name="paramName"></param>
//        /// <returns></returns>
//        ParamItem GetParamInfo(string paramName);

//        /// <summary>
//        /// Записать полную информацию о параметре
//        /// </summary>
//        /// <param name="paramName"></param>
//        /// <param name="paramItem"></param>
//        /// <param name="insertIfAbsent">Добавить, если параметра не существует</param>
//        void SetParamInfo(string paramName, ParamItem paramItem, bool insertIfAbsent = true);

//        /// <summary>
//        /// Записать значение параметра
//        /// </summary>
//        /// <param name="paramName"></param>
//        /// <param name="isCrypt"></param>
//        /// <param name="cryptoKey"></param>
//        /// <returns></returns>
//        string GetParamValue(string paramName, bool isCrypt = false, string cryptoKey = null);

//        /// <summary>
//        /// Загрузить значение параметра
//        /// </summary>
//        /// <param name="paramName"></param>
//        /// <param name="paramItem"></param>
//        /// <param name="isCrypt"></param>
//        /// <param name="cryptoKey"></param>
//        void SetParamValue(string paramName, string paramItem, bool isCrypt = false, string cryptoKey = null);
//        /// <summary>
//        /// Записать все параметры модуля
//        /// </summary>
//        /// <param name="paramList"></param>
//        /// <param name="insertIfAbsent"></param>
//        void SetAllModulePatams(List<ParamItem> paramList, bool insertIfAbsent = true);
//        /// <summary>
//        /// Загрузить все параметры модуля
//        /// </summary>
//        /// <returns></returns>
//        List<ParamItem> LoadAllModulePatams();
//    }
//}
