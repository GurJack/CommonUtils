using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Represents a visitor or rewriter for expression trees.
    /// </summary>
    public class ReplaceBinaryVisitor : ExpressionVisitorBase
    {
        private readonly PropertyInfo _property;
        private readonly Expression _expressionToCompare;
        private bool _isNeedReturnTrue = false;

        /// <summary>
        /// Rewrites expression without current property.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static Expression Rewrite(Expression expression, PropertyInfo property)
        {
            return new ReplaceBinaryVisitor(property).Visit(expression);
        }

        /// <summary>
        /// Rewrites expression without current property.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="expressionToCompare"></param>
        /// <returns></returns>
        public static Expression Rewrite(Expression expression, Expression expressionToCompare)
        {
            return new ReplaceBinaryVisitor(expressionToCompare).Visit(expression);
        }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="property"></param>
        public ReplaceBinaryVisitor(PropertyInfo property)
        {
            _property = property;
        }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="expressionToCompare"></param>
        public ReplaceBinaryVisitor(Expression expressionToCompare)
        {
            _expressionToCompare = expressionToCompare;
        }

        /// <inheritdoc />
        public override Expression Visit(Expression node)
        {
            var newExpression = CompareExpressions(node);
            if (newExpression != null)
                return newExpression;

            return base.Visit(node);
        }

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.BinaryExpression.
        /// </summary>
        /// <param name="b">The expression to visit.</param>
        /// <returns> The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression b)
        {
            var left = Visit(b.Left);
            if (_isNeedReturnTrue
                && Object.ReferenceEquals(left, LambdaConverter.ExpressionTrue))
            {
                _isNeedReturnTrue = false;
                return left;
            }

            var right = Visit(b.Right);
            if (_isNeedReturnTrue
                && Object.ReferenceEquals(right, LambdaConverter.ExpressionTrue))
            {
                _isNeedReturnTrue = false;
                return right;
            }

            var conversion = Visit(b.Conversion);

            var newExpression = BinaryWithCurrentProperty(left);
            if (_isNeedReturnTrue
                && newExpression != null)
            {
                _isNeedReturnTrue = false;
                return newExpression;
            }

            newExpression = BinaryWithCurrentProperty(right);
            if (_isNeedReturnTrue
                && newExpression != null)
            {
                _isNeedReturnTrue = false;
                return newExpression;
            }


            if (left != b.Left || right != b.Right || conversion != b.Conversion)
            {
                if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
                    return Expression.Coalesce(left, right, conversion as LambdaExpression);
                else
                    return Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
            }

            return b;
        }

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.MethodCallExpression.
        /// </summary>
        /// <param name="m">The expression to visit.</param>
        /// <returns> The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            var obj = Visit(m.Object);
            // var args = VisitExpressionList(m.Arguments);

            var newExpression = BinaryWithCurrentProperty(obj);
            if (newExpression != null)
                return newExpression;

            if (obj?.NodeType == ExpressionType.Constant)
            {
                return obj;
            }

            if (obj != m.Object)// || args != m.Arguments)
            {
                return Expression.Call(obj, m.Method, m.Arguments);
            }

            return m;
        }

        private Expression BinaryWithCurrentProperty(Expression e)
        {
            if (_property != null)
            {
                if ((e as MemberExpression)?.Member.MemberType == MemberTypes.Property)
                {
                    var propertyInfo = ((MemberExpression) e).Member as PropertyInfo;
                    if (PropertyIsEqual(propertyInfo, _property))
                    {
                        _isNeedReturnTrue = true;
                        return LambdaConverter.ExpressionTrue;
                    }
                }
            }

            return null;
        }

        private bool PropertyIsEqual(PropertyInfo p1, PropertyInfo p2)
        {
            return p1 == p2 ||
                   (p1 != null && p2 != null &&
                    p1.Name == p2.Name &&
                    p1.DeclaringType == p2.DeclaringType &&
                    p1.PropertyType == p2.PropertyType);
        }

        private Expression CompareExpressions(Expression e)
        {
            if (_expressionToCompare != null)
            {
                if (LambdaConverter.Equals(_expressionToCompare, e))
                {
                    _isNeedReturnTrue = true;
                    return LambdaConverter.ExpressionTrue;
                }
            }

            return null;
        }
    }
}