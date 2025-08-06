using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CommonUtils.Extensions;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Enables support for local collection values.
    /// </summary>
    public class LocalCollectionExpander : ExpressionVisitorBase
    {
        /// <summary>
        /// Rewrites expression with local collection values.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static Expression Rewrite(Expression expression)
        {
            return new LocalCollectionExpander().Visit(expression);
        }

        /// <summary>
        /// VisitMethodCall.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // pair the method's parameter types with its arguments
            var map = node.Method.GetParameters()
                .Zip(node.Arguments, (p, a) => new {Param = p.ParameterType, Arg = a}).ToLinkedList();

            // deal with instance methods
            var instanceType = node.Object == null ? null : node.Object.Type;
            map.AddFirst(new {Param = instanceType, Arg = node.Object});

            // for any local collection parameters in the method, make a
            // replacement argument which will print its elements
            var replacements = (from x in map
                where x.Param != null && x.Param.IsGenericType
                let g = x.Param.GetGenericTypeDefinition()
                where g == typeof(IEnumerable<>) || g == typeof(List<>)
                where (x.Arg.NodeType == ExpressionType.Constant
                       || (x.Arg.NodeType == ExpressionType.Call
                           && ((MethodCallExpression) x.Arg).Arguments.Count == 2
                           && ((MethodCallExpression) x.Arg).Arguments[0] is ConstantExpression
                           && ((MethodCallExpression) x.Arg).Method.DeclaringType == typeof(System.Linq.Enumerable)
                           && ((MethodCallExpression) x.Arg).Method.Name == nameof(Enumerable.Select)))
                let elementType = x.Param.GetGenericArguments().Single()
                let printer = (x.Arg.NodeType == ExpressionType.Constant
                    ? MakePrinter((ConstantExpression) x.Arg, elementType)
                    : MakePrinter((MethodCallExpression) x.Arg, elementType))
                select new {x.Arg, Replacement = printer}).ToList();

            if (replacements.Any())
            {
                var args = map.Select(x => (from r in replacements
                                               where r.Arg == x.Arg
                                               select r.Replacement).SingleOrDefault() ?? x.Arg).ToList();

                node = node.Update(args.First(), args.Skip(1));
            }

            return base.VisitMethodCall(node);
        }

        private ConstantExpression MakePrinter(ConstantExpression enumerable, Type elementType)
        {
            var flattenedList = ((IEnumerable) enumerable.Value).Flatten();

            return MakePrinter(flattenedList, elementType);
        }

        private ConstantExpression MakePrinter(MethodCallExpression selectMethod, Type elementType)
        {
            var flattenedList = ((IEnumerable) LambdaConverter.ExecuteExpression(selectMethod)).Flatten();

            return MakePrinter(flattenedList, elementType);
        }

        private ConstantExpression MakePrinter(List<object> flattenedList, Type elementType)
        {
            var printerType = typeof(Printer<>).MakeGenericType(elementType);
            var printer = ClassFactory.CreateInstance(printerType, flattenedList);

            return Expression.Constant(printer);
        }

        /// <summary>
        /// Overrides ToString to print each element of a collection.
        /// </summary>
        /// <remarks>
        /// Inherits List in order to support List.Contains instance method as well
        /// as standard Enumerable.Contains/Any extension methods.
        /// </remarks>
        private class Printer<T> : List<T>
        {
            public Printer(IEnumerable collection)
            {
                AddRange(collection.Cast<T>());
            }

            public override string ToString()
            {
                if (typeof (T).IsAssignableFrom(typeof (Guid)))
                {
                    //TODO: пример результата: (new Guid? [] {Guid.Parse("83027aee-d7c6-43c8-8975-73ec24b9196f")}).Contains(Identity)
                    // Это выражение компилируется, но пока Dynamic.Linq не может его спарсить (тут CommonExpression.ParseLambdaWhere<T>(from.Expression);)
                    var type = typeof(T).IsNullableType() ? "Guid?" : "Guid";
                    return $"(new {type} [] {{{this.ToConcatenatedString<T>(t => "Guid.Parse(\"" + t.ToString() + "\")", ",")}}})";
                }

                return $"(new [] {{{this.ToConcatenatedString<T>(t => (t is IKey ? ((IKey) t).Key.ToString() : t.ToString()), ",")}}})";
            }
        }
    }
}