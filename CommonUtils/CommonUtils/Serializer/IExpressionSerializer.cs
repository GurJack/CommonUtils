using System.Linq.Expressions;

namespace CommonUtils.Serializer
{
    /// <summary>
    /// The serializer for <see cref="System.Linq.Expressions.Expression"/>.
    /// </summary>
    public interface IExpressionSerializer
    {
        /// <summary>
        /// Deserializes from value to the expression.
        /// </summary>
        Expression Deserialize(object value);

        /// <summary>
        /// Deserializes from generic value to the expression.
        /// </summary>
        TExp Deserialize<TExp>(object value) where TExp : Expression;

        /// <summary>
        /// Serializes expression to the value.
        /// </summary>
        object SerializeAsObject(Expression expression);
    }

    /// <summary>
    /// The generic serializer for <see cref="System.Linq.Expressions.Expression"/>.
    /// </summary>
    public interface IExpressionSerializer<T> : IExpressionSerializer
    {
        /// <summary>
        /// Deserializes from generic value to the expression.
        /// </summary>
        Expression Deserialize(T value);

        /// <summary>
        /// Deserializes from generic value to the expression.
        /// </summary>
        TExp Deserialize<TExp>(T value) where TExp : Expression;

        /// <summary>
        /// Serializes expression to the generic value.
        /// </summary>
        T Serialize(Expression expression);
    }
}