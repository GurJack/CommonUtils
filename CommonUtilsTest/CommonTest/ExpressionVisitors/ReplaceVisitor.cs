using System;
using System.Linq.Expressions;
using CommonUtils.Extensions;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Represents a visitor or rewriter for expression trees.
    /// </summary>
    public class ReplaceVisitor : ExpressionVisitorBase
    {
        private static readonly string ParseMethodName = nameof(Guid.Parse);

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.ConditionalExpression.
        /// </summary>
        /// <param name="ce">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitConditional(ConditionalExpression ce)
        {
            var test = ce.Test;
            var resTest = Visit(test);

            if (resTest.NodeType == ExpressionType.Constant)
            {
                var exp = (ConstantExpression) resTest;

                var bResTest = exp.Value as bool?;

                if (bResTest == true)
                    return Visit(ce.IfTrue);

                if (bResTest == false)
                    return Visit(ce.IfFalse);
            }

            return base.VisitConditional(ce);
        }

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.BinaryExpression.
        /// </summary>
        /// <param name="b">The expression to visit.</param>
        /// <returns> The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression b)
        {
            switch (b.NodeType)
            {
                case ExpressionType.AndAlso:
                {
                    var lValue = Visit(b.Left);
                    var cLeft = ProcessConstant(lValue);
                    if (cLeft == false)
                        return LambdaConverter.ExpressionFalse;
                    if (cLeft == true)
                        return Visit(b.Right);

                    var rValue = Visit(b.Right);
                    var cRight = ProcessConstant(rValue);
                    if (cRight == false)
                        return LambdaConverter.ExpressionFalse;
                    if (cRight == true)
                        return lValue;

                    if (lValue != b.Left || rValue != b.Right)
                    {
                        EqualizeTypes(ref lValue, ref rValue);
                        return Expression.AndAlso(lValue, rValue, b.Method);
                    }
                }
                    break;
                case ExpressionType.OrElse:
                {
                    var lValue = Visit(b.Left);
                    var cLeft = ProcessConstant(lValue);
                    if (cLeft == true)
                        return LambdaConverter.ExpressionTrue;
                    if (cLeft == false)
                        return Visit(b.Right);

                    var rValue = Visit(b.Right);
                    var cRight = ProcessConstant(rValue);
                    if (cRight == true)
                        return LambdaConverter.ExpressionTrue;
                    if (cRight == false)
                        return lValue;

                    if (lValue != b.Left || rValue != b.Right)
                    {
                        EqualizeTypes(ref lValue, ref rValue);
                        return Expression.OrElse(lValue, rValue, b.Method);
                    }
                    }
                    break;

                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                {
                    var left = Visit(b.Left);
                    var right = Visit(b.Right);

                    if (left.NodeType == ExpressionType.Constant &&
                        right.NodeType == ExpressionType.Constant)
                    {
                        if (b.NodeType == ExpressionType.Equal ||
                            b.NodeType == ExpressionType.NotEqual)
                        {
                            var equals = ObjectConverter.Equals(((ConstantExpression) left).Value, ((ConstantExpression) right).Value);
                            if (b.NodeType == ExpressionType.NotEqual) equals = !equals;

                            return Expression.Constant(equals);
                        }

                        if (b.NodeType == ExpressionType.GreaterThan ||
                            b.NodeType == ExpressionType.GreaterThanOrEqual ||
                            b.NodeType == ExpressionType.LessThan ||
                            b.NodeType == ExpressionType.LessThanOrEqual)
                        {
                            var compareResult = ObjectConverter.Compare(((ConstantExpression) left).Value, ((ConstantExpression) right).Value);
                                if (compareResult < 0)
                                {
                                    if (b.NodeType == ExpressionType.LessThan ||
                                        b.NodeType == ExpressionType.LessThanOrEqual)
                                        return Expression.Constant(true);

                                    return Expression.Constant(false);
                                }

                                if (compareResult == 0)
                                {
                                    if (b.NodeType == ExpressionType.LessThanOrEqual ||
                                        b.NodeType == ExpressionType.GreaterThanOrEqual)
                                        return Expression.Constant(true);

                                    return Expression.Constant(false);
                                }

                                if (b.NodeType == ExpressionType.GreaterThan ||
                                    b.NodeType == ExpressionType.GreaterThanOrEqual)
                                    return Expression.Constant(true);

                                return Expression.Constant(false);
                        }
                    }

                    if (left.NodeType == ExpressionType.Constant || right.NodeType == ExpressionType.Constant)
                    {
                        EqualizeTypes(ref left, ref right);

                        if (b.NodeType == ExpressionType.Equal)
                            return Expression.Equal(left, right, b.IsLiftedToNull, b.Method);
                        if (b.NodeType == ExpressionType.NotEqual)
                            return Expression.NotEqual(left, right, b.IsLiftedToNull, b.Method);

                        if (b.NodeType == ExpressionType.GreaterThan)
                            return Expression.GreaterThan(left, right, b.IsLiftedToNull, b.Method);
                        if (b.NodeType == ExpressionType.GreaterThanOrEqual)
                            return Expression.GreaterThanOrEqual(left, right, b.IsLiftedToNull, b.Method);

                        if (b.NodeType == ExpressionType.LessThan)
                            return Expression.LessThan(left, right, b.IsLiftedToNull, b.Method);
                        if (b.NodeType == ExpressionType.LessThanOrEqual)
                            return Expression.LessThanOrEqual(left, right, b.IsLiftedToNull, b.Method);
                    }
                }
                    break;
            }

            return b;
        }

        private bool? ProcessConstant(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                var exp = (ConstantExpression)e;

                var bResTest = exp.Value as bool?;

                if (bResTest == false)
                    return false;

                if (bResTest == true)
                    return true;
            }

            return null;
        }


        /// <summary>
        ///  Visits the children of the System.Linq.Expressions.MemberExpression.
        /// </summary>
        /// <param name="m">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression m)
        {
            var exp = m.Expression;
            while (exp != null)
            {
                if (exp.NodeType == ExpressionType.Parameter ||
                    exp.NodeType == ExpressionType.Convert)
                {
                    return base.VisitMember(m);
                }

                if (exp.NodeType == ExpressionType.TypeAs)
                {
                    var operand = Visit((exp as UnaryExpression)?.Operand);
                    if (!(operand is ConstantExpression))
                    {
                        return base.VisitMember(m);
                    }
                }

                if (exp.NodeType == ExpressionType.Call)
                {
                    var methodExp = Visit(((MethodCallExpression) exp));
                    if (!(methodExp is ConstantExpression))
                    {
                        return base.VisitMember(m);
                    }
                }

                exp = (exp as MemberExpression)?.Expression;
            }

            var constant = LambdaConverter.ExecuteExpressionAsConstantExpression(m);

            return constant;
        }

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.MethodCallExpression.
        /// </summary>
        /// <param name="m">The expression to visit.</param>
        /// <returns> The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.Name == ParseMethodName)
            {
                var constant = LambdaConverter.ExecuteExpressionAsConstantExpression(m);

                return constant;
            }

            return m;
        }

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.UnaryExpression.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            var operand = Visit(node.Operand);
            if (operand != node.Operand)
            {
                if(operand is ConstantExpression)
                {
                    if (node.NodeType == ExpressionType.Convert)
                        return Expression.Constant(((ConstantExpression) operand).Value, node.Type);
                }

                return Expression.MakeUnary(node.NodeType, operand, node.Type, node.Method);
            }

            return node;
        }

        /// <summary>
        /// Visits the children of the System.Linq.Expressions.TypeBinaryExpression.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected override Expression VisitTypeBinary(TypeBinaryExpression b)
        {
            var expr = Visit(b.Expression);
            if (expr is ConstantExpression)
            {
                var valueType = ((ConstantExpression) expr).Value?.GetType() ?? (b.TypeOperand.IsNullableType() ? b.TypeOperand : ((ConstantExpression) expr).Type);
                if (valueType == b.TypeOperand)
                    return Expression.Constant(true);

                return Expression.Constant(false);
            }

            return b;
        }
    }
}