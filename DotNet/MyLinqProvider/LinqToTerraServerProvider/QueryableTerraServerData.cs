using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToTerraServerProvider
{

    /// <summary>
    /// 要创建 LINQ 提供程序，至少必须实现 IQueryable<T> 和 IQueryProvider 接口
    /// 如果要支持诸如 OrderBy 和 ThenBy 等排序查询运算符，还必须实现 IOrderedQueryable<T> 接口
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class QueryableTerraServerData<TData> : IOrderedQueryable<TData>
    {
        public QueryableTerraServerData()
        {
            Provider = new TerraServerQueryProvider();
            Expression = Expression.Constant(this);
        }

        public QueryableTerraServerData(TerraServerQueryProvider provider, Expression expression)
        {
            if (null == provider)
            {
                throw new ArgumentNullException("provider");
            }

            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<TData>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            Provider = provider;
            Expression = expression;
        }

        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public Type ElementType
        {
            get
            {
                return typeof(TData);
            }
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<TData>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }
    }
}
