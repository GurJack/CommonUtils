using System.Linq.Expressions;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Represents a visitor for replacement parameters in expression.
    /// </summary>
    public class ReplaceParameterVisitor : ExpressionVisitorBase
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="oldParameter"></param>
        /// <param name="newParameter"></param>
        protected ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        /// <summary>
        /// Gets the old parameter.
        /// </summary>
        protected ParameterExpression OldParameter => _oldParameter;

        /// <summary>
        /// Gets the new parameter.
        /// </summary>
        protected ParameterExpression NewParameter => _newParameter;

        /// <summary>
        /// Rewrites expression with replacing parameter expressions.
        /// </summary>
        /// <returns></returns>
        public static Expression Rewrite(Expression expression, ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            return new ReplaceParameterVisitor(oldParameter, newParameter).Visit(expression);
        }

        /// <summary>
        /// Visits the System.Linq.Expressions.ParameterExpression.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_oldParameter == null || ReferenceEquals(node, _oldParameter))
                return _newParameter;

            return base.VisitParameter(node);
        }
    }
}