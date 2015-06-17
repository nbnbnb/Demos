using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace LinqToNHibernateSample
{
    class Program
    {
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            // 设置 NHibernate Profiler 追踪
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            // 创建配置对象
            var configuration = new Configuration();
            // 调用此方法时，将会查找 名称为 【hibernate.cfg.xml】的配置文件
            // 根据此文件，获得数据库的信息
            configuration.Configure();
            // 指定包含实体类的程序集
            // 读取此程序集中的 hbm.xml配置文件
            configuration.AddAssembly(Assembly.GetExecutingAssembly());

            // 也可以添加单个实体和配置文件
            //configuration.AddClass(typeof(Star));
            //configuration.AddClass(typeof(Planet));

            // 创建 SessionFactory
            var factory = configuration.BuildSessionFactory();

            // 创建数据库【如果存在则重新创建】
            // 第一个参数表示是否输出创建脚本
            //new SchemaExport(configuration).Execute(false, true, false);
            //CreateData(factory);
            //QueryData(factory);

            TempSession(factory);

            Console.Write("Hit enter to exit");
            Console.ReadLine();
        }

        static void TempSession(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var stars = session.Query<Star>();

                foreach (var star in stars)
                {
                    foreach (var planet in star.Planets)
                    {
                        //Console.WriteLine(planet.Name);
                    }
                }
            }
        }

        static void CreateData(ISessionFactory factory)
        {
            var sun = new Star
            {
                Name = "Sun",
                Mass = 1,
                Class = StarTypes.G,
                Color = SurfaceColor.WhiteToYellow
            };
            var planets = new List<Planet>
                      {
                        new Planet{Name = "Merkur", IsHabitable = false, Sun = sun},
                        new Planet{Name = "Venus", IsHabitable = false, Sun = sun},
                        // please consult the sample code for full list of planets
                      };
            sun.Planets = planets;

            var virginis61 = new Star
            {
                Name = "61 Virginis",
                Mass = 0.95,
                Class = StarTypes.G,
                Color = SurfaceColor.WhiteToYellow
            };
            var planets2 = new List<Planet>
                      {
                        new Planet{Name = "Planet 1", IsHabitable = false, 
                        Sun = virginis61},
                        new Planet{Name = "Planet 2", IsHabitable = true, 
                        Sun = virginis61},
                        new Planet{Name = "Planet 3", IsHabitable = false, 
                            Sun = virginis61},
                      };
            virginis61.Planets = planets2;

            var stars = new List<Star>
                      {
                        sun, 
                        virginis61,
                        new Star{Name = "10 Lacertra", Mass = 60,Class = StarTypes.O, Color = SurfaceColor.Blue},
                        new Star{Name = "Spica", Mass = 18,Class = StarTypes.B, Color = SurfaceColor.Blue},
                      };

            using (var session = factory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                foreach (var star in stars)
                {
                    session.Save(star);
                }
                tx.Commit();
            }
        }

        static void QueryData(ISessionFactory factory)
        {
            using (var session = factory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                PrintListOfStars(session);
                PrintListOfBigBlueStars(session);
                PrintSumOfStarMassPerClass(session);
                PrintListOfHabitablePlanets(session);
                tx.Commit();
            }
        }

        static void PrintListOfStars(ISession session)
        {
            Console.WriteLine("\r\n\nList of stars ------------------\r\n");

            var stars = session.Query<Star>()
               .OrderBy(s => s.Name);

            foreach (var star in stars)
            {
                Console.WriteLine("{0} ({1}, {2})",
                  star.Name, star.Class, star.Color);
            }
        }

        static void PrintListOfBigBlueStars(ISession session)
        {
            Console.WriteLine("\r\n\nList of big blue stars -------\r\n");

            var stars = session.Query<Star>()
               .Where(s => s.Color == SurfaceColor.Blue && s.Mass > 15)
               .OrderByDescending(s => s.Mass)
               .ThenBy(s => s.Name);
            foreach (var star in stars)
            {
                Console.WriteLine("{0} ({1}, {2}, Mass={3})",
                   star.Name, star.Class, star.Color, star.Mass);
            }
        }

        static void PrintSumOfStarMassPerClass(ISession session)
        {
            Console.WriteLine("\r\n\nSum of masses per class -------\r\n");

            var starMasses = session.Query<Star>()
              .GroupBy(s => s.Class)
              .Select(g => new
              {
                  Class = g.Key,
                  TotalMass = g.Sum(s => s.Mass)
              });

            foreach (var mass in starMasses)
            {
                Console.WriteLine("Class={0}, Total Mass={1}",
                   mass.Class, mass.TotalMass);
            }
        }

        static void PrintListOfHabitablePlanets(ISession session)
        {
            Console.WriteLine("\r\n\nList of habitable planets------\r\n");

            var planets = session.Query<Planet>()
             .Where(p => p.IsHabitable)
             .OrderBy(p => p.Sun.Name)
             .ThenBy(p => p.Name);

            foreach (var planet in planets)
            {
                Console.WriteLine("Star='{0}', Planet='{1}'",
                   planet.Sun.Name, planet.Name);
            }
        }
    }
}
