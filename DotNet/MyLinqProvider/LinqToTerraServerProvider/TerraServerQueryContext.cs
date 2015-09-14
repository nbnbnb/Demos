using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    public class TerraServerQueryContext
    {
        internal static object Execute(Expression expression, bool isEnumerable)
        {
            if (!IsQueryOverDataSource(expression))
            {
                throw new InvalidProgramException("No query over the data source was specified");
            }

            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            // 假定最里层的  Queryable.Where 调用包含要用于查询 Web 服务的位置信息
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);

            // Where 的第一个参数为 IEnumerable<TSource>
            // 第二个参数为 Func<TSource,bool>，例如 lambda: x=>x.Name=="A"，这种类型都为 UnaryExpression
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            LocationFinder locationFinder = new LocationFinder(lambdaExpression.Body);

            List<String> locations = locationFinder.Locations;

            if (locations.Count == 0)
            {
                throw new InvalidQueryException("You must specify at least one place name in your query.");
            }

            Place[] places = WebServiceHelper.GetPlacesFromTerraServer(locations);

            IQueryable<Place> queryablePlaces = places.AsQueryable<Place>();

            ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryablePlaces);

            Expression newExpressionTree = treeCopier.Visit(expression);

            if (isEnumerable)
            {
                return queryablePlaces.Provider.CreateQuery(newExpressionTree);
            }
            else
            {
                return queryablePlaces.Provider.Execute(newExpressionTree);
            }
        }

        private static bool IsQueryOverDataSource(Expression expression)
        {
            return expression is MethodCallExpression;
        }
    }
}
