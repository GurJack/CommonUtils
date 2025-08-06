using System;
using System.Linq.Expressions;
using CommonUtils.Extensions;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Represents a visitor for replacement members in expression.
    /// Example: old member expression: a.b; new member expression: V. Result: V.a.b.
    /// </summary>
    public sealed class ReplaceMemberVisitor : ReplaceParameterVisitor
    {
        private readonly MemberExpression _newMemberExpression;

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        private ReplaceMemberVisitor(ParameterExpression oldParameter, ParameterExpression newParameter, MemberExpression newMemberExpression) : base(oldParameter, newParameter)
        {
            _newMemberExpression = newMemberExpression;
        }

        /// <summary>
        /// Rewrites expression with replacing member expressions.
        /// </summary>
        /// <returns></returns>
        public static Expression Rewrite(Expression expression, ParameterExpression oldParameter, ParameterExpression newParameter, MemberExpression newMemberExpression)
        {
            return new ReplaceMemberVisitor(oldParameter, newParameter, newMemberExpression).Visit(expression);
        }

        /// <inheritdoc />
        protected override Expression VisitMember(MemberExpression node)
        {
            ParameterExpression innerParameter = null;
            var exp = node.Expression;
            while (exp != null)
            {
                if (exp.NodeType == ExpressionType.Parameter)
                {
                    innerParameter = (ParameterExpression) exp;
                    break;
                }

                exp = (exp as MemberExpression)?.Expression;
            }

            if (innerParameter != null &&
                Object.ReferenceEquals(innerParameter, OldParameter))
            {
                var joinedExpression = _newMemberExpression.JoinExpression(node);
                return joinedExpression;
            }

            return base.VisitMember(node);
        }

        /// <summary>
        /// VisitMethodCall.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(System.Linq.Enumerable))
            {
                var arg0 = node.Arguments[0];
                if (arg0.NodeType == ExpressionType.Constant ||
                    (arg0 as MemberExpression)?.Expression is ConstantExpression)
                {
                    return node;
                }
            }

            return base.VisitMethodCall(node);
        }
    }
}