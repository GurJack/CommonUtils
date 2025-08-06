using System.Linq.Expressions;
using CommonUtils.Extensions;

namespace CommonUtils.Expressions
{
    /// <summary>
    ///  Class for writing default expression.
    /// </summary>
    public class DefaultExpressionWriter : IExpressionWriter
    {
        /// <summary>
        /// Writes the <see cref="Expression"/> instance to the string.
        /// </summary>
        /// <returns>The string presentation of this expression.</returns>
        string IExpressionWriter.Write(Expression expression)
        {
            return expression.ToCommonString();
        }
    }
}