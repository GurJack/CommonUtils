using System.Linq.Expressions;


namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// The visitor for replace expressions with their constant values.
    /// </summary>
    public class ConstantReplacerVisitor : ExpressionVisitorBase
    {
        /// <summary>
        /// Default constructor. 
        /// </summary>
        private ConstantReplacerVisitor()
        {
        }

        /// <summary>
        /// Rewrites expression with constant values.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression Rewrite(Expression expression)
        {
            return new ConstantReplacerVisitor().Visit(expression);
        }

        /// <summary>
        ///  Visits the children of the System.Linq.Expressions.MemberExpression.
        /// </summary>
        /// <param name="m">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression m)
        {
            if (LambdaConverter.IsParameterAccess(m))
            {
                return base.VisitMember(m);
            }

            var constant = LambdaConverter.ExecuteExpressionAsConstantExpression(m);

            return constant;
        }

        /// <summary>
        /// VisitMethodCall.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (LambdaConverter.IsParameterAccess(m))
            {
                return base.VisitMethodCall(m);
            }

            var constant = LambdaConverter.ExecuteExpressionAsConstantExpression(m);

            if (constant.Type.IsInterface &&
                typeof(System.Collections.IEnumerable).IsAssignableFrom(constant.Type))
            {
                var method = typeof(System.Linq.Enumerable).GetMethod(nameof(System.Linq.Enumerable.ToList));
                method = method.MakeGenericMethod(constant.Type.GenericTypeArguments);
                var newM = Expression.Call(null, method, constant);

                constant = LambdaConverter.ExecuteExpressionAsConstantExpression(newM);
            }

            return constant;
        }
    }
}