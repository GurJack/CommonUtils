using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CommonUtils.ExpressionVisitors;

namespace CommonUtils
{
    /// <summary>
    /// Class for lambda comparing.
    /// </summary>
    public static class LambdaConverter
    {
        /// <summary>
        /// Gets the constant for expression = true.
        /// </summary>
        public static readonly Expression ExpressionTrue = Expression.Constant(true);
        /// <summary>
        /// Gets the constant for expression = false.
        /// </summary>
        public static readonly Expression ExpressionFalse = Expression.Constant(false);


        private static readonly ExpressionVisitor _visitor = new ReplaceVisitor();

        /// <summary>
        /// Gets the Visitor.
        /// </summary>
        public static ExpressionVisitor Visitor => _visitor;

        /// <summary>
        /// Check that expression is constant.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static bool? ExpressionIsBoolConstant(Expression exp)
        {
            if (exp == null) return null;

            switch (exp.NodeType)
            {
                case ExpressionType.Lambda:
                    return ProcessLambda(exp as LambdaExpression);
                case ExpressionType.Constant:
                    var value = ProcessConstant(exp as ConstantExpression) as bool?;
                    return value;
                case ExpressionType.AndAlso:
                case ExpressionType.OrElse:
                    return ProcessBinary(exp as BinaryExpression);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Executes <see cref="Expression"/> and returns the result.
        /// </summary>
        /// <param name="initialExpression"></param>
        /// <returns></returns>
        public static object ExecuteExpression(Expression initialExpression)
        {
            var e = initialExpression;
            if (initialExpression.NodeType == ExpressionType.Lambda)
            {
                if (((LambdaExpression) initialExpression).Body.NodeType == ExpressionType.Constant)
                    return ((ConstantExpression) ((LambdaExpression) initialExpression).Body).Value;

                e = ((LambdaExpression) initialExpression).Body;
            }

            var member = Expression.Convert(e, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            var value = getter();

            return value;
        }

        /// <summary>
        /// Executes <see cref="Expression"/> and returns the result as <see cref="ConstantExpression"/>.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static ConstantExpression ExecuteExpressionAsConstantExpression(Expression m)
        {
            var value = ExecuteExpression(m);

            return Expression.Constant(value, m.Type);
        }

        private static bool? ProcessBinary(BinaryExpression binaryExpression)
        {
            if (binaryExpression.NodeType == ExpressionType.AndAlso)
            {
                var value = ExpressionIsBoolConstant(binaryExpression.Left);
                if (value == false)
                    return false;

                value = ExpressionIsBoolConstant(binaryExpression.Right);
                if (value == false)
                    return false;
            }
            else if (binaryExpression.NodeType == ExpressionType.OrElse)
            {
                var value = ExpressionIsBoolConstant(binaryExpression.Left);
                if (value == true)
                    return true;

                value = ExpressionIsBoolConstant(binaryExpression.Right);
                if (value == true)
                    return true;
            }

            return null;
        }

        private static bool? ProcessLambda(LambdaExpression lambda)
        {
            return ExpressionIsBoolConstant(lambda.Body);
        }

        private static bool? ProcessConstant(ConstantExpression c)
        {
            if (c == null)
                return null;

            return c.Value as bool?;
        }

        /// <summary>
        /// Determines whether two object values are equals
        /// </summary>
        /// <param name="firstValue"></param>
        /// <param name="secondValue"></param>
        /// <returns></returns>
        public new static bool Equals(object firstValue, object secondValue)
        {
            if (firstValue is Expression && secondValue is Expression)
                return ExpressionsEqual((Expression) firstValue, (Expression) secondValue, null, null);

            return Object.Equals(firstValue, secondValue);
        }

        /// <summary>
        /// Determines whether two object values are equals
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool Eq<TSource, TValue>(
            Expression<Func<TSource, TValue>> x,
            Expression<Func<TSource, TValue>> y)
        {
            return ExpressionsEqual(x, y, null, null);
        }

        /// <summary>
        /// Determines whether two object values are equals
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Expression<Func<Expression<Func<TSource, TValue>>, bool>> Eq<TSource, TValue>(Expression<Func<TSource, TValue>> y)
        {
            return x => ExpressionsEqual(x, y, null, null);
        }

        private static bool ExpressionsEqual(Expression x, Expression y, LambdaExpression rootX, LambdaExpression rootY)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;

            var valueX = TryCalculateConstant(x);
            var valueY = TryCalculateConstant(y);

            if (valueX.IsDefined && valueY.IsDefined)
                return ValuesEqual(valueX.Value, valueY.Value);

            if (x.NodeType != y.NodeType
                || x.Type != y.Type)
            {
                if (IsAnonymousType(x.Type) && IsAnonymousType(y.Type))
                    throw new NotImplementedException("Comparison of Anonymous Types is not supported");

                return false;
            }

            if (x is LambdaExpression)
            {
                var lx = (LambdaExpression)x;
                var ly = (LambdaExpression)y;
                var paramsX = lx.Parameters;
                var paramsY = ly.Parameters;
                return CollectionsEqual(paramsX, paramsY, lx, ly) && ExpressionsEqual(lx.Body, ly.Body, lx, ly);
            }
            if (x is MemberExpression)
            {
                var mex = (MemberExpression)x;
                var mey = (MemberExpression)y;
                return Equals(mex.Member, mey.Member) && ExpressionsEqual(mex.Expression, mey.Expression, rootX, rootY);
            }
            if (x is BinaryExpression)
            {
                var bx = (BinaryExpression)x;
                var by = (BinaryExpression)y;
                return bx.Method == @by.Method && ExpressionsEqual(bx.Left, @by.Left, rootX, rootY) &&
                       ExpressionsEqual(bx.Right, @by.Right, rootX, rootY);
            }
            if (x is ParameterExpression)
            {
                var px = (ParameterExpression) x;
                var py = (ParameterExpression) y;
                if (rootX != null && rootY != null)
                {
                    return rootX.Parameters.IndexOf(px) == rootY.Parameters.IndexOf(py);
                }

                return px.IsByRef == py.IsByRef
                       && px.Type == py.Type;
            }
            if (x is MethodCallExpression)
            {
                var cx = (MethodCallExpression)x;
                var cy = (MethodCallExpression)y;
                return cx.Method == cy.Method
                       && ExpressionsEqual(cx.Object, cy.Object, rootX, rootY)
                       && CollectionsEqual(cx.Arguments, cy.Arguments, rootX, rootY);
            }
            if (x is MemberInitExpression)
            {
                var mix = (MemberInitExpression)x;
                var miy = (MemberInitExpression)y;
                return ExpressionsEqual(mix.NewExpression, miy.NewExpression, rootX, rootY)
                   && MemberInitsEqual(mix.Bindings, miy.Bindings, rootX, rootY);
            }

            if (x is UnaryExpression)
            {
                var ux = (UnaryExpression) x;
                var uy = (UnaryExpression) y;
                return ux.Method == uy.Method && ExpressionsEqual(ux.Operand, uy.Operand, rootX, rootY);
            }
            if (x is NewArrayExpression)
            {
                var nx = (NewArrayExpression)x;
                var ny = (NewArrayExpression)y;
                return CollectionsEqual(nx.Expressions, ny.Expressions, rootX, rootY);
            }

            if (x is NewExpression)
            {
                var nx = (NewExpression)x;
                var ny = (NewExpression)y;
                return
                    Equals(nx.Constructor, ny.Constructor)
                    && CollectionsEqual(nx.Arguments, ny.Arguments, rootX, rootY)
                    && (nx.Members == null && ny.Members == null
                        || nx.Members != null && ny.Members != null && CollectionsEqual(nx.Members, ny.Members));
            }

            if (x is ConditionalExpression)
            {
                var cx = (ConditionalExpression)x;
                var cy = (ConditionalExpression)y;
                return
                    ExpressionsEqual(cx.Test, cy.Test, rootX, rootY)
                    && ExpressionsEqual(cx.IfFalse, cy.IfFalse, rootX, rootY)
                    && ExpressionsEqual(cx.IfTrue, cy.IfTrue, rootX, rootY);
            }

            if (x is TypeBinaryExpression)
            {
                var tbx = (TypeBinaryExpression) x;
                var tby = (TypeBinaryExpression) y;
                return tbx.TypeOperand == tby.TypeOperand
                       && tbx.Type == tby.Type
                       && Equals(tbx.Expression, tby.Expression);
            }

            throw new NotImplementedException(x.ToString());
        }

        private static bool IsAnonymousType(Type type)
        {
            var hasCompilerGeneratedAttribute = type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any();
            var nameContainsAnonymousType = type.FullName.Contains("AnonymousType");
            var isAnonymousType = hasCompilerGeneratedAttribute && nameContainsAnonymousType;

            return isAnonymousType;
        }

        private static bool MemberInitsEqual(MemberInitExpression mix, MemberInitExpression miy)
        {
            if (mix.Bindings.Count != miy.Bindings.Count)
            {
                return false;
            }

            foreach (var memberBinding in mix.Bindings)
            {

                var ybinding = miy.Bindings.Single(b => b.Member.Name == memberBinding.Member.Name);
                var val = TryCalculateConstant(((MemberAssignment)memberBinding).Expression);
                var val2 = TryCalculateConstant(((MemberAssignment)ybinding).Expression);
                if (!val.Value.Equals(val2.Value))
                {
                    return false;
                }

            }
            return true;
        }

        private static bool MemberInitsEqual(ICollection<MemberBinding> bx, ICollection<MemberBinding> by, LambdaExpression rootX, LambdaExpression rootY)
        {
            if (bx.Count != by.Count)
            {
                return false;
            }

            if (bx.Concat(by).Any(b => b.BindingType != MemberBindingType.Assignment))
                throw new NotImplementedException("Only MemberBindingType.Assignment is supported");

            return
                bx.Cast<MemberAssignment>().OrderBy(b => b.Member.Name).Select((b, i) => new { Expr = b.Expression, b.Member, Index = i })
                .Join(
                      by.Cast<MemberAssignment>().OrderBy(b => b.Member.Name).Select((b, i) => new { Expr = b.Expression, b.Member, Index = i }),
                      o => o.Index, o => o.Index, (xe, ye) => new { XExpr = xe.Expr, XMember = xe.Member, YExpr = ye.Expr, YMember = ye.Member })
                       .All(o => Equals(o.XMember, o.YMember) && ExpressionsEqual(o.XExpr, o.YExpr, rootX, rootY));
        }

        private static bool ValuesEqual(object x, object y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (x is ICollection && y is ICollection)
                return CollectionsEqual((ICollection)x, (ICollection)y);

            return Equals(x, y);
        }

        private static ConstantValue TryCalculateConstant(Expression e)
        {
            if (e is ConstantExpression)
                return new ConstantValue(true, ((ConstantExpression)e).Value);

            if (e is MemberExpression)
            {
                var me = (MemberExpression)e;
                var parentValue = TryCalculateConstant(me.Expression);
                if (parentValue.IsDefined)
                {
                    var result =
                        me.Member is FieldInfo
                            ? ((FieldInfo)me.Member).GetValue(parentValue.Value)
                            : ((PropertyInfo)me.Member).GetValue(parentValue.Value);
                    return new ConstantValue(true, result);
                }
            }
            if (e is NewArrayExpression)
            {
                var ae = ((NewArrayExpression)e);
                var result = ae.Expressions.Select(TryCalculateConstant);
                if (result.All(i => i.IsDefined))
                    return new ConstantValue(true, result.Select(i => i.Value).ToArray());
            }
            if (e is ConditionalExpression)
            {
                var ce = (ConditionalExpression)e;
                var evaluatedTest = TryCalculateConstant(ce.Test);
                if (evaluatedTest.IsDefined)
                {
                    if (evaluatedTest.Value.Equals(true))
                    {
                        return TryCalculateConstant(ce.IfTrue);
                    }
                    else
                    {
                        return TryCalculateConstant(ce.IfFalse);
                    }
                }
            }

            if (e is MethodCallExpression)
            {
                var me = (MethodCallExpression) e;

                if (MethodIsConstant(me))
                    return new ConstantValue(true, ExecuteExpression(me));
            }

            return default(ConstantValue);
        }

        private static bool MethodIsConstant(MethodCallExpression m)
        {
            if (m.Object != null &&
                (m.Object.NodeType == ExpressionType.Constant ||
                 (m.Object.NodeType == ExpressionType.MemberAccess
                  && ((MemberExpression) m.Object).Member is FieldInfo
                     )
                    )
                )
            {
                foreach (var argExp in m.Arguments)
                {
                    var exp = argExp;
                    while (exp != null)
                    {
                        if (exp.NodeType == ExpressionType.Parameter ||
                            exp.NodeType == ExpressionType.Convert)
                        {
                            return false;
                        }

                        exp = (exp as MemberExpression)?.Expression;
                    }
                }

                return true;
            }

            return false;
        }

        private static bool CollectionsEqual(IEnumerable<Expression> x, IEnumerable<Expression> y, LambdaExpression rootX, LambdaExpression rootY)
        {
            return x.Count() == y.Count()
                   && x.Select((e, i) => new { Expr = e, Index = i })
                       .Join(y.Select((e, i) => new { Expr = e, Index = i }),
                             o => o.Index, o => o.Index, (xe, ye) => new { X = xe.Expr, Y = ye.Expr })
                       .All(o => ExpressionsEqual(o.X, o.Y, rootX, rootY));
        }

        private static bool CollectionsEqual(ICollection x, ICollection y)
        {
            return x.Count == y.Count
                   && x.Cast<object>().Select((e, i) => new { Expr = e, Index = i })
                       .Join(y.Cast<object>().Select((e, i) => new { Expr = e, Index = i }),
                             o => o.Index, o => o.Index, (xe, ye) => new { X = xe.Expr, Y = ye.Expr })
                       .All(o => Equals(o.X, o.Y));
        }

        private struct ConstantValue
        {
            public ConstantValue(bool isDefined, object value) : this()
            {
                IsDefined = isDefined;
                Value = value;
            }

            public bool IsDefined { get; private set; }

            public object Value { get; private set; }
        }

        /// <summary>
        /// Determines whether the expression contains the specified expression types.
        /// </summary>
        /// <returns></returns>
        public static bool CheckExpressionForTypes(Expression e, ExpressionType[] types)
        {
            while (e != null)
            {
                if (types.Contains(e.NodeType))
                {
                    var subUnaryExpr = e as UnaryExpression;
                    var isSubExprAccess = subUnaryExpr?.Operand is IndexExpression;
                    if (!isSubExprAccess)
                        return true;
                }

                if (e is BinaryExpression binaryExpr)
                {
                    if (CheckExpressionForTypes(binaryExpr.Left, types))
                        return true;

                    if (CheckExpressionForTypes(binaryExpr.Right, types))
                        return true;
                }

                if (e is MethodCallExpression methodCallExpr)
                {
                    for (var i = 0; i < methodCallExpr.Arguments.Count; i++)
                    {
                        if (CheckExpressionForTypes(methodCallExpr.Arguments[i], types))
                            return true;
                    }

                    if (CheckExpressionForTypes(methodCallExpr.Object, types))
                        return true;
                }

                if (e is UnaryExpression unaryExpr)
                {
                    if (CheckExpressionForTypes(unaryExpr.Operand, types))
                        return true;
                }

                if (e is ConditionalExpression condExpr)
                {
                    if (CheckExpressionForTypes(condExpr.Test, types))
                        return true;

                    if (CheckExpressionForTypes(condExpr.IfTrue, types))
                        return true;

                    if (CheckExpressionForTypes(condExpr.IfFalse, types))
                        return true;
                }

                var memberExpr = e as MemberExpression;
                e = memberExpr?.Expression;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the expression is the parameter.
        /// </summary>
        /// <returns>Returns true if the specified expression is parameter;
        /// otherwise, false.</returns>
        public static bool IsParameterAccess(Expression e)
        {
            return CheckExpressionForTypes(e, new[] { ExpressionType.Parameter });
        }
    }
}