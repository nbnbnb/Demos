using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{
    public class LocationFinder : ExpressionVisitor
    {
        private Expression expression;
        private List<String> locations;

        public LocationFinder(Expression exp)
        {
            this.expression = exp;
        }

        public List<String> Locations
        {
            get
            {
                if (locations == null)
                {
                    locations = new List<String>();
                    this.Visit(this.expression);
                }

                return this.locations;
            }
        }

        protected override Expression VisitBinary(BinaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Equal)
            {
                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(expression, typeof(Place), "Name"))
                {
                    locations.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(expression, typeof(Place), "Name"));
                    return expression;
                }
                else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(expression, typeof(Place), "State"))
                {
                    locations.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(expression, typeof(Place), "State"));
                    return expression;
                }
            }

            return base.VisitBinary(expression);
        }
    }
}
