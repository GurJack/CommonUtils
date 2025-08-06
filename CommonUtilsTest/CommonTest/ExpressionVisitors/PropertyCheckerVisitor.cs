using System.Linq.Expressions;
using System.Reflection;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Represents a visitor for checks specified property.
    /// </summary>
    public class PropertyCheckerVisitor : ExpressionVisitorBase
    {
        private PropertyInfo _property;
        private bool _expressionContainsProperty;
        private static readonly PropertyCheckerVisitor Visitor = new PropertyCheckerVisitor();

        private static readonly object _lockObject = new object();

        /// <summary>
        /// Checks for contains property in specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool CheckProperty(Expression expression, PropertyInfo property)
        {
            lock (_lockObject)
            {
                var visitor = Visitor;
                visitor._expressionContainsProperty = false;
                visitor._property = property;

                var exp = visitor.Visit(expression);
                visitor._property = null;

                return visitor.ExpressionContainsProperty;
            }
        }

        /// <summary>
        /// Checks for contains any property in specified expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool CheckAnyProperty(Expression expression)
        {
            lock (_lockObject)
            {
                var visitor = Visitor;
                visitor._expressionContainsProperty = false;
                visitor._property = null;

                var exp = visitor.Visit(expression);
                visitor._property = null;

                return visitor.ExpressionContainsProperty;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PropertyCheckerVisitor()
        {
        }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="property"></param>
        public PropertyCheckerVisitor(PropertyInfo property)
        {
            _property = property;
        }

        /// <summary>
        /// Gets the expression contains property flag.
        /// </summary>
        public bool ExpressionContainsProperty => _expressionContainsProperty;

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.MemberExpression.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (!_expressionContainsProperty &&
                node.Member.MemberType == MemberTypes.Property)
            {
                var propertyInfo = node.Member as PropertyInfo;
                if (_property == null ||
                    propertyInfo == _property ||
                    (propertyInfo != null &&
                     propertyInfo.Name == _property.Name &&
                     propertyInfo.DeclaringType == _property.DeclaringType &&
                     propertyInfo.PropertyType == _property.PropertyType))
                {
                    _expressionContainsProperty = true;
                }
            }

            return base.VisitMember(node);
        }
    }
}