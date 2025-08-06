using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using CommonUtils.Expressions;
using CommonUtils.Extensions;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// The static class for inner collection expand to string.
    /// </summary>
    internal static class CustomValueToStringHelper
    {
        /// <summary>
        /// Pattern for constants.
        /// </summary>
        public const string PatternConstant = "value\\(.+?\\)";


        /// <summary>
        /// Supports for inner collections (inside arrays).
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string LambdaToString(LambdaExpression expression)
        {
            var replacements = new Dictionary<string, string>();
            WalkExpression(replacements, expression);

            var body = expression.Body.ToString();

            foreach (var replacement in replacements)
            {
                body = body.Replace(replacement.Key, replacement.Value);
            }

            return body;
        }

        public static string ConstantToString(ConstantExpression expression)
        {
            return expression.Value.ToString();
        }

        private static void WalkExpression(Dictionary<string, string> replacements, Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    var replacementExpression = expression.ToString();
                    if (Regex.IsMatch(replacementExpression, CustomValueToStringHelper.PatternConstant))
                    {
                        var value = LambdaConverter.ExecuteExpression(expression);
                        var replacementValue = value.ToString();
                        if (!replacements.ContainsKey(replacementExpression))
                        {
                            replacements.Add(replacementExpression, replacementValue);
                        }
                    }
                    break;

                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.OrElse:
                case ExpressionType.AndAlso:
                case ExpressionType.Equal:
                    var bexp = expression as BinaryExpression;
                    WalkExpression(replacements, bexp.Left);
                    WalkExpression(replacements, bexp.Right);
                    break;

                case ExpressionType.Call:
                    var mcexp = expression as MethodCallExpression;
                    foreach (var argument in mcexp.Arguments)
                    {
                        WalkExpression(replacements, argument);
                    }
                    break;

                case ExpressionType.Lambda:
                    var lexp = expression as LambdaExpression;
                    WalkExpression(replacements, lexp.Body);
                    break;

                case ExpressionType.Constant:
                    var strExpr = expression.ToString();

                    if (!replacements.ContainsKey(strExpr))
                    {
                        if (Regex.IsMatch(strExpr, CustomValueToStringHelper.PatternConstant))
                        {
                            var replacementValue = LambdaConverter.ExecuteExpression(expression);

                            string strReplacementValue;
                            if (replacementValue.GetType().IsArray)
                            {
                                if (replacementValue.GetType().GetElementType().IsAssignableFrom(typeof (Guid)) ||
                                    (replacementValue.GetType().GetElementType().IsGenericType && replacementValue.GetType().GetElementType().GetGenericArguments().Single().IsAssignableFrom(typeof (Guid))))
                                {
                                    strReplacementValue = "(new Guid?[] {" + $"{((IEnumerable) replacementValue).ToConcatenatedString(t => "Guid.Parse(\"" + t + "\")", ",")}" + "})";
                                }
                                else
                                {
                                    strReplacementValue = "(new [] {" + $"{((IEnumerable) replacementValue).ToConcatenatedString(t => t.ToString(), ",")}" + "})";
                                }
                            }
                            else
                            {
                                strReplacementValue = replacementValue.ToString();
                            }

                            replacements.Add(strExpr, strReplacementValue);
                        }
                        else if ((expression as ConstantExpression)?.Value?.GetType().IsAssignableFrom(typeof (Guid)) == true)
                        {
                            replacements.Add(strExpr, CommonExpression.PrepareValueForExpression((expression as ConstantExpression).Value));
                        }
                    }

                    break;

                default:

                    break;
            }
        }
    }
}