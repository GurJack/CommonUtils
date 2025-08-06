//using System;
//using System.Linq.Expressions;
//using CommonUtils.Exceptions;
//using CommonUtils.Expressions;

//namespace CommonUtils.Attributes
//{
//    /// <summary>
//    /// Атрибут для получения имени структурного поля для текущего виртуального поля
//    /// либо для получения константного значения (в этом случае в запрос на SQL не уйдет),
//    /// либо для выражения формулы (например, Name + " " + Note).
//    /// </summary>
//    [AttributeUsage(AttributeTargets.Property)]
//    public class StructAttribute : AttributeBase
//    {
//        /// <summary>
//        /// The constant for the <see cref="Guid.Empty"/>.
//        /// </summary>
//        public const string GuidEmpty = nameof(Guid) + "." + nameof(Guid.Empty);

//        private Type _contextType;
//        private Expression _expression;

//        /// <summary>
//        /// Constructor with array of parameters.
//        /// </summary>
//        /// <param name="nameItems"></param>
//        public StructAttribute(params string[] nameItems)
//            : this(nameItems == null ? null : String.Join(".", nameItems), StructType.AttributeAccess)
//        {
//        }

//        /// <summary>
//        /// Constructor with parameters.
//        /// </summary>
//        public StructAttribute(object value, StructType type = StructType.AttributeAccess)
//            : base(value, false)
//        {
//            Type = type;
//        }

//        /// <summary>
//        /// Gets the expression text.
//        /// </summary>
//        public string ExpressionText => (string) Value;

//        /// <summary>
//        /// Gets the prepared expression for the struct attribute.
//        /// </summary>
//        public Expression Expression => _expression ?? (_expression = GetExpression());

//        /// <summary>
//        /// Sets the context type for the expression.
//        /// </summary>
//        /// <param name="contextType"></param>
//        public void SetContextType(Type contextType)
//        {
//            _contextType = contextType;
//        }

//        private Expression GetExpression()
//        {
//            if (Type == StructType.Constant)
//            {
//                if (Value == null)
//                {
//                    return _expression = Expression.Constant(null);
//                }

//                if (!(Value is string))
//                {
//                    return _expression = Expression.Constant(Value);
//                }

//                if (String.Equals(ExpressionText, bool.TrueString, StringComparison.InvariantCultureIgnoreCase))
//                {
//                    return _expression = Expression.Constant(true, typeof(bool));
//                }

//                if (String.Equals(ExpressionText, bool.FalseString, StringComparison.InvariantCultureIgnoreCase))
//                {
//                    return _expression = Expression.Constant(false, typeof(bool));
//                }

//                switch (ExpressionText)
//                {
//                    case "":
//                        return _expression = Expression.Constant(String.Empty, typeof(string));
//                    case StructAttribute.GuidEmpty:
//                        return Expression.Constant(Guid.Empty, typeof(Guid));
//                    default:
//                        try
//                        {
//                            var contextType = typeof(object);
//                            var lambda = CommonExpression.ParseLambda(contextType, ExpressionText);
//                            var expression = LambdaConverter.Visitor.Visit(lambda.Body);
//                            return _expression = expression;
//                        }
//                        catch (Exception e)
//                        {
//                            ExceptionManager.Log(e);
//                            return _expression = Expression.Constant(null);
//                        }

//                }
//            }
//            else
//            {
//                if (_contextType == null)
//                {
//                    throw new ArgumentNullException("The context type is not initialized!");
//                }

//                var lambda = CommonExpression.ParseLambda(_contextType, ExpressionText);
//                var expression = LambdaConverter.Visitor.Visit(lambda.Body);
//                return _expression = expression;
//            }

//        }

//        /// <summary>
//        /// Constant flag.
//        /// </summary>
//        public StructType Type { get; }

//        /// <summary>
//        /// Gets the constant value.
//        /// </summary>
//        public object ConstantValue
//        {
//            get
//            {
//                if (Type != StructType.Constant) return null;

//                return ((ConstantExpression) Expression).Value;
//            }
//        }
//    }

//    /// <summary>
//    /// The struct attribute types.
//    /// </summary>
//    public enum StructType
//    {
//        /// <summary>
//        /// The attribute access type.
//        /// </summary>
//        AttributeAccess,

//        /// <summary>
//        /// The constant type.
//        /// </summary>
//        Constant,

//        /// <summary>
//        /// The expression type.
//        /// </summary>
//        Expression
//    }
//}