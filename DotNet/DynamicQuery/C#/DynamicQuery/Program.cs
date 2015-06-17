//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic;
using System.Windows.Forms;
using NorthwindMapping;

namespace Dynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            // For this sample to work, you need an active database server or SqlExpress.
            // Here is a connection to the Data sample project that ships with Microsoft Visual Studio 2008.
            //string dbPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\..\Data\NORTHWND.MDF"));
            //string sqlServerInstance = @".\SQLEXPRESS";
            //string connString = "AttachDBFileName='" + dbPath + "';Server='" + sqlServerInstance + "';user instance=true;Integrated Security=SSPI;Connection Timeout=60";

            // Here is an alternate connect string that you can modify for your own purposes.
            /*
            string connString = @"server=.\SQLEXPRESS;database=northwind;Integrated Security=SSPI;";

            Northwind db = new Northwind(connString);
            db.Log = Console.Out;

            var query =
                db.Customers.Where("City == @0 and Orders.Count >= @1", "London", 10).
                OrderBy("CompanyName").
                Select("New(CompanyName as Name, Phone)");

            Console.WriteLine(query);
             * */

            //ParseLambdaDemo1();
            //ParseLambdaDemo2();
            //ParseDemo();
            CreateClassDemo();

            Console.ReadLine();

        }

        static void ParseLambdaDemo1()
        {
            ParameterExpression x = Expression.Parameter(typeof(int), "x");
            ParameterExpression y = Expression.Parameter(typeof(int), "y");

            // Expression<Func<int,int,int>>
            LambdaExpression e = DynamicExpression.ParseLambda(
                new ParameterExpression[] { x, y },
                null,  // 如果此处的参数指定为null,这返回值将根据表达式结果进行推断
                "(x+y)*2");

            // Expression<Func<int,int,double>>
            LambdaExpression e2 = DynamicExpression.ParseLambda(
                new ParameterExpression[] { x, y },
                typeof(double),  // 此处也可以显示指定表达式的结果
                "(x+y)*2");

            // 此处需要进行一个显示转换
            GetExpression((Expression<Func<int, int, int>>)e);
            GetExpression2((Expression<Func<int, int, double>>)e2);
        }

        static void GetExpression(Expression<Func<int, int, int>> expression)
        {
            // 注意
            // 此处使用 Compile方法得到一个委托类型
            int res = expression.Compile()(1, 2);
            Console.WriteLine(res);
        }
        static void GetExpression2(Expression<Func<int, int, double>> expression)
        {
            // 注意
            // 此处使用 Compile方法得到一个委托类型
            double res = expression.Compile()(1, 2);
            Console.WriteLine(res);
        }

        static void ParseLambdaDemo2()
        {
            // Expression<Func<Person,int>>
            LambdaExpression e = DynamicExpression.ParseLambda(
                typeof(Person),  // 此参数指定表达式中字段的作用域
                typeof(int),
                "Age");  // 此处也可以使用 it.Age
            GetExpression3((Expression<Func<Person, int>>)e);

            // 结果于上面一样
            LambdaExpression e2 = DynamicExpression.ParseLambda<Person, int>("it.Age");
            GetExpression3((Expression<Func<Person, int>>)e2);
        }

        static void GetExpression3(Expression<Func<Person, int>> expression)
        {
            Person p = new Person
            {
                Name = "ZhangJin",
                Age = 26
            };

            int age = expression.Compile()(p);
            Console.WriteLine(age);
        }

        static void ParseDemo()
        {
            ParameterExpression x = Expression.Parameter(typeof(int), "x");
            ParameterExpression y = Expression.Parameter(typeof(int), "y");

            // 注意，要使用此类型的字典，来传递 表达式参数
            // 在Parse方法中，将会进行自动的解析
            Dictionary<string, object> symbols = new Dictionary<string, object>();

            symbols.Add("x", x);
            symbols.Add("y", y);

            Expression body = DynamicExpression.Parse(null, // 根据表达式结果进行自动推断
                "(x+y)*2", symbols);

            Expression body2 = DynamicExpression.Parse(typeof(double), // 显式设置
            "(x+y)*2", symbols);

            LambdaExpression e = Expression.Lambda(body, // 此处使用推断的 int
                new ParameterExpression[] { x, y });

            LambdaExpression e2 = Expression.Lambda(body2,  // 此处使用 double
                         new ParameterExpression[] { x, y });

            GetExpression((Expression<Func<int, int, int>>)e);
            GetExpression2((Expression<Func<int, int, double>>)e2);
        }

        static void CreateClassDemo()
        {
            DynamicProperty[] props = new DynamicProperty[]{
                new DynamicProperty("Name",typeof(string)),
                new DynamicProperty("Birthday",typeof(DateTime))
            };

            Type type = DynamicExpression.CreateClass(props);

            object obj = Activator.CreateInstance(type);
            type.GetProperty("Name").SetValue(obj, "ZhangJin", null);
            type.GetProperty("Birthday").SetValue(obj, new DateTime(1987, 06, 15), null);
            Console.WriteLine(obj);

            Func<Person, bool> kkk = p => p.Age > 10;
        }



        private class Person
        {
            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}
