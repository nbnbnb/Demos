using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using DataAccess;
using Model;
using System.Collections.Specialized;

namespace BreakAwayConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new InitializeBagaDatabaseWithSeedData());
            Console.WriteLine("Start...");
            Temp();
            Console.WriteLine("End...");
            Console.ReadKey();
        }

        static void Temp()
        {
            using (var context = new BreakAwayContext())
            {

                context.Database.Log = Console.WriteLine;
                context.Departments.Add(new Department { Name = DepartmentNames.English });

                context.SaveChanges(); 

            }

        }
    }
}