using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CommonUtils.ExpressionVisitors;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// Expression extensions.
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// Returns a <see cref="string"/> that represents the specified expression.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the specified expression.</returns>
        public static string ToCommonString(this Expression expression)
        {
            // locally evaluate as much of the query as possible
            var eval = Evaluator.PartialEval(expression, CanBeEvaluatedLocally);

            // support local collections
            eval = LocalCollectionExpander.Rewrite(eval);

            if (eval.NodeType == ExpressionType.Lambda)
            {
                var exp = (LambdaExpression) eval;
                var newParamNames = new string[exp.Parameters.Count];
                for (var i = 0; i < exp.Parameters.Count; i++)
                {
                    var oldParamater = exp.Parameters[i];
                    newParamNames[i] = Guid.NewGuid().ToString("N") + RandomGenerator.Next(10, 99);
                    var newParameter = Expression.Parameter(oldParamater.Type, newParamNames[i]);
                    exp = (LambdaExpression) ReplaceParameterVisitor.Rewrite(exp, oldParamater, newParameter);
                }

                var str = CustomValueToStringHelper.LambdaToString(exp);

                foreach (var newParamName in newParamNames)
                {
                    str = str.Replace(newParamName + ".", String.Empty);
                }

                return str;
            }

            if (eval.NodeType == ExpressionType.Constant)
            {
                return CustomValueToStringHelper.ConstantToString((ConstantExpression) eval);
            }

            return eval.ToString();
        }



        private static Func<Expression, bool> CanBeEvaluatedLocally
        {
            get
            {
                return expression =>
                {
                    // don't evaluate parameters
                    if (expression.NodeType == ExpressionType.Parameter)
                        return false;

                    // can't evaluate queries
                    if (typeof(IQueryable).IsAssignableFrom(expression.Type))
                        return false;

                    return true;
                };
            }
        }


        /// <summary>
        /// Determinates is the expression empty.
        /// </summary>
        public static bool IsEmpty(this Expression expression) => expression == null;

        /// <summary>
        /// Determinates is the true result filter.
        /// </summary>
        public static bool IsTrue(this Expression expression) => LambdaConverter.Equals(expression, LambdaConverter.ExpressionTrue);

        /// <summary>
        /// Determinates is the false result filter.
        /// </summary>
        public static bool IsFalse(this Expression expression) => LambdaConverter.Equals(expression, LambdaConverter.ExpressionFalse);

        /// <summary>
        /// Converts expression to expression with derived type.
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Expression<Func<TTo, bool>> TypeConvert<TFrom, TTo>(
            this Expression<Func<TFrom, bool>> from)
        {
            if (from == null) return null;

            if (!(typeof(TTo) == typeof(TFrom) || typeof(TTo).IsSubclassOf(typeof(TFrom))))
            {
                throw new ArgumentException("Cannot copies from SqlServerExpressionExtended with not base type for current type.");
            }

            return ConvertImpl<Func<TFrom, bool>, Func<TTo, bool>>(from);
        }

        /// <summary>
        /// Converts expression to expression with derived type.
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Expression<Func<TTo, object>> TypeConvert<TFrom, TTo>(
            this Expression<Func<TFrom, object>> from)
        {
            if (from == null) return null;

            if (!(typeof(TTo) == typeof(TFrom) || typeof(TTo).IsSubclassOf(typeof(TFrom))))
            {
                throw new ArgumentException("Cannot copies from SqlServerExpressionExtended with not base type for current type.");
            }

            return ConvertImpl<Func<TFrom, object>, Func<TTo, object>>(from);
        }

        private static Expression<TTo> ConvertImpl<TFrom, TTo>(this Expression<TFrom> from)
            where TFrom : class
            where TTo : class
        {
            if (from == null) return null;

            // figure out which types are different in the function-signature
            var fromTypes = from.Type.GetGenericArguments();
            var toTypes = typeof (TTo).GetGenericArguments();

            if (fromTypes.Length != toTypes.Length)
            {
                throw new ArgumentException("Incompatible lambda function-type signatures");
            }

            var typeMap = new Dictionary<Type, Type>();
            for (var i = 0; i < fromTypes.Length; i++)
            {
                if (fromTypes[i] != toTypes[i])
                    typeMap[fromTypes[i]] = toTypes[i];
            }

            var newParams = GenerateNewParameters(from, typeMap);
            var body = from.Body;
            for (var i = 0; i < newParams.Length; i++)
            {
                body = new ReplaceTypeVisitor(from.Parameters[i], newParams[i]).Visit(body);
            }

            // rebuild the lambda
            return Expression.Lambda<TTo>(body, newParams);
        }

        private static ParameterExpression[] GenerateNewParameters<TFrom>(
            Expression<TFrom> from,
            Dictionary<Type, Type> typeMap
        )
            where TFrom : class
        {
            var newParams = new ParameterExpression[from.Parameters.Count];

            for (var i = 0; i < newParams.Length; i++)
            {
                Type newType;
                if (typeMap.TryGetValue(from.Parameters[i].Type, out newType))
                {
                    newParams[i] = Expression.Parameter(newType, from.Parameters[i].Name);
                }
            }
            return newParams;
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static Expression<Func<T, bool>> EasyAnd<T>(this Expression<Func<T, bool>> first,
                                                       Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// Combines the first predicate with the second using the logical "and".
        /// </summary>
        public static Expression<Func<T, bool>> EasyOr<T>(this Expression<Func<T, bool>> first,
                                                       Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }


        /// <summary>
        /// Combines the first expression with the second using the specified merge function.
        /// </summary>
        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge)
        {
            if (first == null) return second;

            if (second == null) return first;

            // figure out which types are different in the function-signature
            var fromTypes = first.Type.GetGenericArguments();
            var toTypes = second.Type.GetGenericArguments();

            if (fromTypes.Length != toTypes.Length)
            {
                throw new ArgumentException("Incompatible lambda function-type signatures");
            }

            var secondBody = second.Body;
            for (var i = 0; i < first.Parameters.Count; i++)
            {
                secondBody = ReplaceParameterVisitor.Rewrite(secondBody, second.Parameters[i], first.Parameters[i]);
            }

            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// Boxing lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<T, object>> BoxingLambda<T, TKey>(this Expression<Func<T, TKey>> expression)
        {
            return BoxingLambda<T, TKey>((LambdaExpression) expression);
        }


        /// <summary>
        /// Boxing lambda expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression<Func<T, object>> BoxingLambda<T, TKey>(this LambdaExpression expression)
        {
            // Add the boxing operation, but get a weakly typed expression
            var converted = Expression.Convert(expression.Body, typeof(object));

            // Use Expression.Lambda to get back to strong typing
            var boxProperty = Expression.Lambda<Func<T, object>>(converted, expression.Parameters);

            return boxProperty;
        }

        /// <summary>
        /// From A.B.C and D.E.F makes A.B.C.D.E.F. D must be a member of C.
        /// </summary>
        /// <param name="memberExpression1"></param>
        /// <param name="memberExpression2"></param>
        /// <returns></returns>
        public static MemberExpression JoinExpression(this Expression memberExpression1, MemberExpression memberExpression2)
        {
            var stack = new Stack<MemberInfo>();
            Expression current = memberExpression2;
            while (current.NodeType != ExpressionType.Parameter &&
                   current.NodeType != ExpressionType.Convert)
            {
                if (current is ConstantExpression)
                {
                    return memberExpression2;
                }
                if (current is MemberExpression memberAccess)
                {
                    if (memberAccess.Expression == null)
                    {
                        return memberExpression2;
                    }

                    current = memberAccess.Expression;
                    stack.Push(memberAccess.Member);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            var jointMemberExpression = memberExpression1;
            foreach (var memberInfo in stack)
            {
                jointMemberExpression = Expression.MakeMemberAccess(jointMemberExpression, memberInfo);
            }

            return (MemberExpression)jointMemberExpression;
        }

        /// <summary>
        /// Returns name of inner member expression If the memberexpression is single in whole expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetSingleMemberName(this LambdaExpression expression)
        {
            var name = (expression.Body as MemberExpression)?.Member.Name ??
                          ((expression.Body as UnaryExpression)?.Operand as MemberExpression)?.Member.Name;

            return name;
        }
    }
}