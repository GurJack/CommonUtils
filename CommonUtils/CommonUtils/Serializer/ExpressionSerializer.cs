//using System;
//using System.Linq.Expressions;

//namespace CommonUtils.Serializer
//{
//    /// <summary>
//    /// The base class for all expression serializers.
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class ExpressionSerializer<T> : IExpressionSerializer<T>
//    {
//        /// <summary>
//        /// Deserializes from value to the expression.
//        /// </summary>
//        public Expression Deserialize(object value)
//        {
//            if (!(value is T))
//            {
//                throw new NotSupportedException($"Serializer cannot read specified value to Expression. {typeof(T).Name} is required.");
//            }

//            return Deserialize((T) value);
//        }

//        /// <summary>
//        /// Deserializes from generic value to the expression.
//        /// </summary>
//        public TExp Deserialize<TExp>(object value) where TExp : Expression => (TExp) Deserialize(value);

//        /// <summary>
//        /// Serializes expression to the value.
//        /// </summary>
//        public object SerializeAsObject(Expression expression) => Serialize(expression);

//        /// <summary>
//        /// Deserializes from the value to the boolean expression.
//        /// </summary>
//        public Expression<Func<TEntity, bool>> DeserializeToBooleanExpression<TEntity>(object value)
//        {
//            if (!(value is T))
//            {
//                throw new NotSupportedException($"Serializer cannot read specified value to Expression. {typeof(T).Name} is required.");
//            }

//            return DeserializeToBooleanExpression<TEntity>((T) value);
//        }

//        /// <summary>
//        /// Deserializes from generic value to the expression.
//        /// </summary>
//        public abstract Expression Deserialize(T value);

//        /// <summary>
//        /// Deserializes from generic value to the expression.
//        /// </summary>
//        public TExp Deserialize<TExp>(T value) where TExp : Expression => (TExp) Deserialize(value);

//        /// <summary>
//        /// Serializes expression to the generic value.
//        /// </summary>
//        public abstract T Serialize(Expression expression);
//    }
//}
