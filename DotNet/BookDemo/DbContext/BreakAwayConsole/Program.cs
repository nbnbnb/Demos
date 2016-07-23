using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Demo();
            Console.WriteLine("End");
            Console.ReadKey();
        }

        private static void Demo()
        {
            // 添加测试数据
            // Database.SetInitializer(new InitializeBagaDatabaseWithSeedData());

            using (BreakAwayContext context = new BreakAwayContext())
            {
                // 在控制台中显示日志相关信息
                //context.Database.Log = Console.WriteLine;

                var canyonQuery = from d in context.Destinations
                                  where d.Name == "Grand Canyon"
                                  select d;

                var canyon = canyonQuery.Single();

                var lodgingQuery = context.Entry(canyon).Collection(d => d.Lodgings).Query();  // 返回的是一个 IQueryable<T> 查询对象

                var distanceQuery = from l in lodgingQuery where l.MilesFromNearestAirport <= 10 select l;

                foreach (var lodging in distanceQuery)
                {
                    Console.WriteLine(lodging.Name);
                }
            }
        }
    }
}