using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace CommonUtils.ExpressionVisitors
{
    /// <summary>
    /// Base class for all expression visitors.
    /// </summary>
    public class ExpressionVisitorBase : ExpressionVisitor
    {
        /// <summary>
        /// Visits the expression list.
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        protected ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            List<Expression> list = null;
            for (int i = 0, n = original.Count; i < n; i++)
            {
                var p = Visit(original[i]);
                if (list != null)
                {
                    list.Add(p);
                }
                else if (p != original[i])
                {
                    list = new List<Expression>(n);
                    for (var j = 0; j < i; j++)
                    {
                        list.Add(original[j]);
                    }
                    list.Add(p);
                }
            }
            if (list != null)
            {
                return list.AsReadOnly();
            }
            return original;
        }

        /// <summary>
        /// EqualizeTypes.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected void EqualizeTypes(ref Expression left, ref Expression right)
        {
            if (left.NodeType == ExpressionType.Constant && left.Type != right.Type)
            {
                var origValue = ((ConstantExpression)left).Value;
                var newValue = ObjectConverter.ChangeType(origValue, right.Type);
                left = Expression.Constant(newValue, right.Type);
            }

            if (right.NodeType == ExpressionType.Constant && right.Type != left.Type)
            {
                var origValue = ((ConstantExpression)right).Value;
                var newValue = ObjectConverter.ChangeType(origValue, left.Type);
                right = Expression.Constant(newValue, left.Type);
            }
        }

        /// <summary>
        /// Checks for all arguments are constants.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected bool AllArgumentsAreConstants(ReadOnlyCollection<Expression> args)
        {
            var allConstants = true;
            foreach (var arg in args)
            {
                if (arg.NodeType != ExpressionType.Constant)
                {
                    allConstants = false;
                    break;
                }
            }
            return allConstants;
        }
    }
}