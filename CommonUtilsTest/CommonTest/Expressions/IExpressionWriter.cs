using System.Linq.Expressions;

namespace CommonUtils.Expressions
{
    /// <summary>
    /// Interface for writing text expressions.
    /// </summary>
    public interface IExpressionWriter
    {
        /// <summary>
        /// Writes the <see cref="Expression"/> instance to the string.
        /// </summary>
        /// <returns>The string presentation of this expression.</returns>
        string Write(Expression expression);
    }
}