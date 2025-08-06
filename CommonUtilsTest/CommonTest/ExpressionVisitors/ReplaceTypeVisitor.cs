using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Represents a visitor or rewriter for expression trees.
    /// </summary>
    public class ReplaceTypeVisitor : ReplaceParameterVisitor
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="oldParameter"></param>
        /// <param name="newParameter"></param>
        public ReplaceTypeVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : base(oldParameter, newParameter)
        {
        }



        /// <summary>
        ///  Visits the children of the System.Linq.Expressions.MemberExpression.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.MemberType == MemberTypes.Property &&
                node.Expression != null &&
                !(node.Expression is MemberExpression))
            {
                // re-perform any member-binding
                var expr = Visit(node.Expression);
                if (expr.Type != node.Expression.Type)
                {
                    if (expr.Type.GetMember(node.Member.Name).Any())
                    {
                        MemberInfo newMember = expr.Type.GetMember(node.Member.Name).Single();
                        return Expression.MakeMemberAccess(expr, newMember);
                    }
                }
            }

            return base.VisitMember(node);
        }
    }
}