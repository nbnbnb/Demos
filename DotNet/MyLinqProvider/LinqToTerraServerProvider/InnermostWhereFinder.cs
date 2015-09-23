using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    public class InnermostWhereFinder : ExpressionVisitor
    {
        private MethodCallExpression innermostWhereExpression;

        public MethodCallExpression GetInnermostWhere(Expression expression)
        {
            Visit(expression);
            return innermostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
            {
                innermostWhereExpression = expression;
            }

            // 将会再次调用 VisitMethodCall 方法
            // 用以查找 innermostWhereExpression
            Visit(expression.Arguments[0]);

            // 还是返回原始的 expression
            return expression;
        }
    }
}
