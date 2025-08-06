using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using CommonUtils.Event;
using CommonUtils.Objects;

namespace CommonUtils.Expressions
{
    /// <summary>
    /// Class represents the any type expression
    /// </summary>
    public static class CommonExpression
    {
        /// <summary>
        /// Cache for string expression.
        /// </summary>
        private static readonly Dictionary<string, Delegate> _delegateCache = new Dictionary<string, Delegate>();

        /// <summary>
        /// Объект, в контексте которого необходимо вычислить значение выражения
        /// </summary>
        public const string ObjectCallerName = "@Caller";

        private static readonly ParsingConfig ParsingConfig = new ParsingConfig {CustomTypeProvider = new DynamicTypeProvider()};

        static CommonExpression()
        {
            DynamicAddSupportedType(typeof(CF));
        }

        /// <summary>
        /// DynamicAddSupportedType
        /// </summary>
        /// <param name="supportedType"></param>
        public static void DynamicAddSupportedType(Type supportedType)
        {
            if (ParsingConfig.CustomTypeProvider is DynamicTypeProvider dynamicTypeProvider)
            {
                dynamicTypeProvider.AddCustomType(supportedType);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Parse String to LambdaExpression
        /// </summary>
        /// <returns></returns>
        public static LambdaExpression ParseLambda(Type type, string expression)
        {
            return ParseLambda(type, null, expression);
        }

        /// <summary>
        /// Parse String to LambdaExpression
        /// </summary>
        /// <returns></returns>
        public static LambdaExpression ParseLambda(Type type, Type resultType, string expression)
        {
            if (expression == null) return null;

            expression = expression.Replace(ObjectCallerName, "it");

            return System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(ParsingConfig, type, resultType, expression);
        }

        /// <summary>
        /// Parse String to LambdaExpression
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static LambdaExpression ParseLambda(string expression, params ParameterExpression[] parameters)
        {
            if (expression == null) return null;

            expression = expression.Replace(ObjectCallerName, "it");

            return System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(ParsingConfig, parameters, null, expression);
        }

        public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            if (expression == null) return null;

            expression = expression.Replace(ObjectCallerName, "it");

            return System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(parameters, resultType, expression, values);
        }

        /// <summary>
        /// CompileLambda
        /// </summary>
        /// <param name="type"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Delegate CompileLambda(Type type, string expression)
        {
            var key = type.Name + expression;
            if (!_delegateCache.TryGetValue(key, out var del))
            {
                del = ParseLambda(type, expression).Compile();
                _delegateCache[key] = del;
            }

            return del;
        }

        /// <summary>
        /// Executes the expression and returns the result value
        /// </summary>
        /// <param name="type">The execution context type</param>
        /// <param name="expression">Text presentation of the expression</param>
        /// <returns>The result value</returns>
        public static object Execute(Type type, string expression)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (String.IsNullOrWhiteSpace(expression)) return null;

            var defValue = GetEmptyValue(type);

            return Execute(defValue, expression);
        }

        /// <summary>
        /// Executes the expression and returns the result value
        /// </summary>
        /// <param name="procedureCallback"></param>
        /// <param name="contextType">The execution context type</param>
        /// <param name="args"></param>
        /// <returns>The result value</returns>
        public static object Execute(ProcedureCallbackWithParams procedureCallback, Type contextType, params object[] args)
        {
            var res = procedureCallback.Invoke(args);

            return ObjectConverter.ChangeType(res, contextType);
        }

        /// <summary>
        /// Executes the expression and returns the result value
        /// </summary>
        /// <param name="objContext">The execution context</param>
        /// <param name="expression">Text presentation of the expression</param>
        /// <returns>The result value</returns>
        public static object Execute(object objContext, string expression)
        {
            if (objContext == null)
                throw new ArgumentNullException(nameof(objContext));

            if (String.IsNullOrWhiteSpace(expression)) return null;

            var compiledLamda = CompileLambda(objContext.GetType(), expression);

            try
            {
                return compiledLamda.DynamicInvoke(objContext);
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Cannot calculate expression{2}<{0}>{2}for object{2}<{1}>", expression, objContext, Environment.NewLine), e);
            }
        }

        /// <summary>
        /// Executes the expression and returns the result value
        /// </summary>
        /// <param name="objContext">The execution context</param>
        /// <param name="expression">Expression</param>
        /// <returns>The result value</returns>
        public static object Execute(object objContext, Delegate expression)
        {
            return expression.DynamicInvoke(objContext);
        }


        /// <summary>
        /// Executes the expression and returns the result value
        /// </summary>
        /// <param name="objContext">The execution context</param>
        /// <param name="expression">Text presentation of the expression</param>
        /// <returns>The result value</returns>
        public static object Execute<T>(T objContext, LambdaExpression expression)
        {
            if (objContext == null)
                throw new ArgumentNullException(nameof(objContext));

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (expression is Expression<Func<T, bool>>)
            {
                var compiledExpression = (expression as Expression<Func<T, bool>>).Compile();
                var bRes = compiledExpression(objContext);

                return bRes;
            }

            if (expression is Expression<Func<T, object>>)
            {
                var compiledExpression = (expression as Expression<Func<T, object>>).Compile();
                var bRes = compiledExpression(objContext);

                return bRes;
            }

            if (expression is Expression<Func<T, string>>)
            {
                var compiledExpression = (expression as Expression<Func<T, string>>).Compile();
                var bRes = compiledExpression(objContext);

                return bRes;
            }

            if (expression is Expression<Func<T, LocalizableString>>)
            {
                var compiledExpression = (expression as Expression<Func<T, LocalizableString>>).Compile();
                var bRes = compiledExpression(objContext);

                return bRes;
            }

            if (expression is Expression<Func<T, Guid>>)
            {
                var compiledExpression = (expression as Expression<Func<T, Guid>>).Compile();
                var bRes = compiledExpression(objContext);

                return bRes;
            }

            if (expression is Expression<Func<T, int>>)
            {
                var compiledExpression = (expression as Expression<Func<T, int>>).Compile();
                var bRes = compiledExpression(objContext);

                return bRes;
            }

            LambdaExpression lambda = expression;
            var compiledLambda = lambda.Compile();
            var res = compiledLambda.DynamicInvoke(objContext);

            return res;
        }

        /// <summary>
        /// Convert text expression to System.Linq.Expressions.Expression for ORDER BY
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<T, object>> ParseLambdaOrder<T>(string expression)
        {

            return System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<T, object>(ParsingConfig, true, expression);
        }

        /// <summary>
        /// Convert text expression to System.Linq.Expressions.Expression with const (caller) for WHERE
        /// If caller is not null, then object context in expression must be named as "@0" or "@Caller", for example (@0.ContextField = SomeProperty)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="objContext"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> ParseLambdaWhere<T>(string expression, object objContext = null)
        {
            if (String.IsNullOrEmpty(expression))
                return null;

            expression = expression.Replace(ObjectCallerName, "@0");

            //expression = expression.Replace("\"\"", "\"");

            if (objContext == null)
            {
                if (expression.Contains("@0"))
                    return null;
            }

            
            return System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda<T, bool>(ParsingConfig, true, expression, objContext);
        }

        /// <summary>
        /// Prepare value for parsing expression.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subQueryLevel"></param>
        /// <returns></returns>
        public static string PrepareValueForExpression(object value, int subQueryLevel = 1)
        {
            if (value == null) return "NULL";

            if (value is Guid)
            {
                var quote = GetQuote(subQueryLevel);
                return $"Guid.Parse({quote}{value}{quote})";
            }

            if (value is String)
            {
                var quote = GetQuote(subQueryLevel);
                var quotePlus = GetQuote(subQueryLevel + 1);
                return $"{quote}{value.ToString().Replace("\"", quotePlus)}{quote}";
            }

            if (value is DateTime)
            {
                var quote = GetQuote(subQueryLevel);
                return $"DateTime.Parse({quote}{((DateTime) value).ToString("yyyy-MM-dd HH:mm:ss.fff")}{quote})";
            }

            if (value is DatePeriodic)
            {
                var quote = GetQuote(subQueryLevel);
                return $"CF.DatePeriodicParse({quote}{((DatePeriodic) value).Begin.ToString("yyyy-MM-dd HH:mm:ss.fff")}{quote})";
            }

            if (value is LocalizableString)
            {
                var quote = GetQuote(subQueryLevel);
                var quotePlus = GetQuote(subQueryLevel + 1);
                return $"{quote}{((LocalizableString) value).OriginalString?.Replace("\"", quotePlus)}{quote}";
            }

            return value.ToString();
        }

        private static string GetQuote(int subQueryLevel)
        {
            var quoteCount = (int) Math.Pow(2, subQueryLevel - 1);
            var quote = String.Concat(System.Linq.Enumerable.Repeat("\"", quoteCount));
            return quote;
        }

        /// <summary>
        /// Get empty value.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetEmptyValue(Type type)
        {
            var method = GetEmptyValueMethod;
            var tempres = method.MakeGenericMethod(type).Invoke(null, new object[0]);
            return tempres;
        }

        private static MethodInfo GetEmptyValueMethod => _getEmptyValueMethod ?? (_getEmptyValueMethod = typeof(CommonExpression).GetMethod(nameof(GetEmptyValue), BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));

        private static MethodInfo _getEmptyValueMethod;

        private static object GetEmptyValue<T>()
        {
            return default(T);
        }
    }
}