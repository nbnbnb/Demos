using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    public class ExpressionTreeModifier : ExpressionVisitor
    {
        private IQueryable<Place> queryablePlaces;

        public ExpressionTreeModifier(IQueryable<Place> places)
        {
            this.queryablePlaces = places;
        }

        /// <summary>
        /// 将最里层标准查询运算符调用的对象替换为 Place 对象的具体列表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression expression)
        {
            if (expression.Type == typeof(QueryableTerraServerData<Place>))
            {
                return Expression.Constant(this.queryablePlaces);
            }
            else
            {
                return expression;
            }
        }
    }
}
