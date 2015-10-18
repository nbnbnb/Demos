using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    /// <summary>
    /// 表达式计算器
    /// 立即计算表达式中的哪些子树（如果有）
    /// 然后，它将通过创建 Lambda 表达式、编译该表达式并调用返回的委托来计算那些表达式
    /// 最后，它将子树替换为一个表示常量值的新节点
    /// 把这个过程成为部分计算
    /// </summary>
    public static class Evaluator
    {
        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            // 抽取出可以执行的表达式
            var tp = new Nominator(fnCanBeEvaluated).Nominate(expression);
            return new SubtreeEvaluator(tp).Eval(expression);
        }

        public static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, Evaluator.CanBeEvaluatedLocally);
        }

        /// <summary>
        /// 除参数表达式以外，都可以执行
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        private class SubtreeEvaluator : ExpressionVisitor
        {
            private HashSet<Expression> _candidates;

            internal SubtreeEvaluator(HashSet<Expression> candidates)
            {
                _candidates = candidates;
            }

            internal Expression Eval(Expression exp)
            {
                // 调用重写的 Visit 方法
                return this.Visit(exp);
            }

            public override Expression Visit(Expression exp)
            {
                // 整个范围链路
                //place => ((place.Name == GetName()) AndAlso(place.State == GetState("abc")))
                //((place.Name == GetName()) AndAlso(place.State == GetState("abc")))
                //(place.Name == GetName())
                //place.Name
                //place
                //GetName()
                //(place.State == GetState("abc"))
                //place.State
                //place
                //GetState("abc")
                //place

                if (exp == null)
                {
                    return null;
                }

                //Console.WriteLine(exp);

                // 如果
                if (_candidates.Contains(exp))
                {
                    return this.Evaluate(exp);
                }
                // 将会递归调用自己
                // 表达式树将会从左到右分组逐个解析
                // 每次递归调用，都会分解出一个子表达式
                return base.Visit(exp);
            }

            /// <summary>
            /// 执行表达式
            /// </summary>
            /// <param name="exp"></param>
            /// <returns></returns>
            private Expression Evaluate(Expression exp)
            {
                if (exp.NodeType == ExpressionType.Constant)
                {
                    return exp;
                }

                LambdaExpression lambda = Expression.Lambda(exp);
                Delegate fn = lambda.Compile();
                return Expression.Constant(fn.DynamicInvoke(null), exp.Type);
            }
        }

        private class Nominator : ExpressionVisitor
        {
            private Func<Expression, bool> _fnCanBeEvaluated;
            private HashSet<Expression> _candidates;
            private bool _cannotBeEvaluated;

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                _fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                _candidates = new HashSet<Expression>();

                // 将会调用自身重载的方法
                // 递归填充 HashSet，找出可以被计算的表达式
                this.Visit(expression);

                return _candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool tp = _cannotBeEvaluated;
                    _cannotBeEvaluated = false;
                    Console.WriteLine(expression);
                    // 将会递归调用
                    base.Visit(expression);
                    Console.WriteLine("---" + expression.ToString());
                    if (!_cannotBeEvaluated)
                    {
                        // 如果为参数，或者参数对象的属性，则不进行计算【place,place.Name】
                        // 例如首先读取 place 参数，此时将 _cannotBeEvaluated 设置为 true
                        // 当递归读取 place.Name 时，也同样不进行计算
                        if (_fnCanBeEvaluated(expression))  
                        {
                            _candidates.Add(expression);
                        }
                        else
                        {
                            _cannotBeEvaluated = true;
                        }
                    }
                    _cannotBeEvaluated |= tp;
                }
                return expression;
            }
        }
    }
}
