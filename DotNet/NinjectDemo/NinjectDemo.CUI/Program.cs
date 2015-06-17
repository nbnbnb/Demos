using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;
using NinjectDemo.Domain.Abstract;
using NinjectDemo.Domain.Concrete;

namespace NinjectDemo.CUI
{
    class Program
    {
        static void Main(string[] args)
        {

            #region With No Parameters

            IKernel ninjectKernel_No_Parameters = new StandardKernel();

            ninjectKernel_No_Parameters.Bind<IValueCalculator>().To<LinqValueCalculator>();

            // get the interface of implementation
            IValueCalculator calcImpl = ninjectKernel_No_Parameters.Get<IValueCalculator>();

            // create the instance of ShoppingCart and inject the dependency
            ShoppingCart cart = new ShoppingCart(calcImpl);

            // perform the calculation and write out the result
            Console.WriteLine("Total : {0:c}", cart.CalculateStockValue());

            #endregion

            #region With Property

            IKernel ninject_With_Constructor = new StandardKernel();

            // 注意，此处如何指定 参数名称，并且如果对类型进行转换 decimal (以M结尾)
            ninject_With_Constructor.Bind<IValueCalculator>().To<CustomValueCalculator>().
                WithConstructorArgument("discount", 10M);

            IValueCalculator calcImpl2 = ninject_With_Constructor.Get<IValueCalculator>();
            ShoppingCart cart2 = new ShoppingCart(calcImpl2);
            Console.WriteLine("Total : {0:c}", cart2.CalculateStockValue());

            #endregion

            #region With Constructor

            IKernel ninject_With_Property = new StandardKernel();

            // 注意，此处添加了一个默认的构造函数，无参数
            // 由于需要实例化，这个不能省
            // 如果使用WithConstructorArgument先实例化，再设置属性，也可以
            ninject_With_Property.Bind<IValueCalculator>().To<CustomValueCalculator>()
                .WithPropertyValue("DisCount", 88M);

            IValueCalculator calcImpl3 = ninject_With_Property.Get<IValueCalculator>();
            ShoppingCart cart3 = new ShoppingCart(calcImpl3);
            Console.WriteLine("Total : {0:c}", cart3.CalculateStockValue());

            #endregion

            #region Using Self Binding

            IKernel ninject_Using_Self_Binding = new StandardKernel();
            ninject_Using_Self_Binding.Bind<IValueCalculator>().To<CustomValueCalculator>();

            // 此处将从 ninject_Using_Self_Binding 中得到的 CustomValueCalculator 对象
            // 传递给 ShoppingCart 构造函数的参数   
            //ShoppingCart cart4 = ninject_Using_Self_Binding.Get<ShoppingCart>();

            // 同样也可以传递参数，如构造参数，属性参数
            //ShoppingCart cart4 = ninject_Using_Self_Binding.Get<ShoppingCart>(
            //    new Ninject.Parameters.PropertyValue("UserName", "ZhangJin"));

            // 同样可以使用下面的方式
            ninject_Using_Self_Binding.Bind<ShoppingCart>().ToSelf()
                .WithPropertyValue("UserName", "ZhangJin");
            ShoppingCart cart4 = ninject_Using_Self_Binding.Get<ShoppingCart>();

            Console.WriteLine("Total : {0:c}", cart4.CalculateStockValue());

            #endregion

            #region Bind a Derived Type

            IKernel ninject_Bind_A_Derived_Type = new StandardKernel();
            ninject_Bind_A_Derived_Type.Bind<IValueCalculator>().To<LinqValueCalculator>();

            // 将基对象绑定到子对象上
            ninject_Bind_A_Derived_Type.Bind<ShoppingCart>().To<LimitShoppingCart>()
                .WithPropertyValue("ItemLimit", 100M);

            // 注意，此处是如何获得 LimitShoppingCart 的
            // 两边的类型一定要是  ShoppingCart
            // 此次得到的实例其实为 LimitShoppingCart
            ShoppingCart cart5 = ninject_Bind_A_Derived_Type.Get<ShoppingCart>();

            Console.WriteLine("Total : {0:c}", cart5.CalculateStockValue());

            #endregion

            #region Condition Bind

            IKernel ninject_Condition_Bind = new StandardKernel();

            // 注意，此处的条件
            // 当需要 注入 LimitShoppingCart 类型时，
            // 创建 IterativeValueCalculator 实例进行构造函数的调用
            ninject_Condition_Bind.Bind<IValueCalculator>()
                .To<IterativeValueCalculator>()
                .WhenInjectedInto<LimitShoppingCart>();

            // 此处使用默认的注入 LinqValueCalculator
            ninject_Condition_Bind.Bind<IValueCalculator>()
                .To<LinqValueCalculator>();

            // 此次将会创建IterativeValueCalculator的实例，
            LimitShoppingCart cart6 = ninject_Condition_Bind.Get<LimitShoppingCart>(
                new Ninject.Parameters.PropertyValue("ItemLimit", 200M));

            // 此次将会创建 默认的 LinqValueCalculator 的实例
            ShoppingCart cart7 = ninject_Condition_Bind.Get<ShoppingCart>();

            Console.WriteLine("Total : {0:c}", cart6.CalculateStockValue());
            Console.WriteLine("Total : {0:c}", cart7.CalculateStockValue());

            #endregion

            Console.ReadKey(true);
        }
    }
}
