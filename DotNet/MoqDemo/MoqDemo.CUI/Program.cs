using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Moq;
using MoqDemo.Domain.Abstract;
using MoqDemo.Domain.Entities;

namespace MoqDemo.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Methods
            var mock_methods = new Mock<IMethods>();

            int calls = 0;
            mock_methods.Setup(m => m.GetCountThing())
                .Returns(() => calls)
                .Callback(() => calls++);

            Console.WriteLine(mock_methods.Object.GetCountThing());  // 0
            Console.WriteLine(mock_methods.Object.GetCountThing());  // 1
            Console.WriteLine(mock_methods.Object.GetCountThing());  // 2

            #endregion

            #region Properties
            var mock_properties = new Mock<IProperties>();

            mock_properties.Setup(m => m.ProductId).Returns(0);
            mock_properties.Setup(m => m.Address).Returns(new Address { City = "WuHan" });
            mock_properties.SetupProperty(m => m.ProductName, "MoqDemo");

            Console.WriteLine(mock_properties.Object.ProductId);  // 0
            Console.WriteLine(mock_properties.Object.Address.City);  // WuHan
            Console.WriteLine(mock_properties.Object.ProductName); // MoqDemo
            #endregion

            #region Events
            var mock_events = new Mock<IEvents>();

            // 首选要订阅事件
            mock_events.Object.Send += (int i, bool b) =>
            {
                Console.WriteLine("Send Event Start-> Integer:{0}->Boolean:{1}", i, b);
            };

            // 触发事件
            // 触发事件时，只需 m.Send+=null 即可，无实际作用
            //     Raises the event referenced in eventExpression using the given args argument
            //     for a non-EventHandler typed event.
            mock_events.Raise(m => m.Send += null, 200, false);

            // 设置触发事件的条件
            // 在调用Submit()方法时，触发事件
            mock_events.Setup(m => m.Submit()).Raises(e => e.Send += null, 100, true);
            // 触发事件
            mock_events.Object.Submit();


            #endregion

            #region Callbacks
            var mock_callbacks = new Mock<ICallbacks>();
            mock_callbacks.Setup(m => m.Execute("ping"))
                .Returns(true)
                .Callback(() => { });

            mock_callbacks.Setup(m => m.Execute(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(true)
                .Callback<int, string>((i, s) => { });
            #endregion

            #region Verification
            var mock_verification = new Mock<IVerification> { CallBase = true };

            mock_verification.Verify(m => m.Execute("hello"), Times.Never(), "This Method should never call");

            // 执行方法
            mock_verification.Object.Execute("ping");  // 执行1次
            mock_verification.Object.Execute("ping");  // 执行2次
            // 测试此方法被执行了
            mock_verification.Verify(m => m.Execute("ping"), "this method shoule ping always");
            // 测试此方法只被执行了2次 
            mock_verification.Verify(m => m.Execute("ping"), Times.Exactly(2), "The Method should only call once");

            var age_1 = mock_verification.Object.UserAge;  // 读取属性 1次
            var age_2 = mock_verification.Object.UserAge;  // 读取属性 2次
            mock_verification.VerifyGet(m => m.UserAge, Times.Exactly(2));    // 验证属性被读取过了(2次)

            mock_verification.Object.UserName = "ZhangJin";
            mock_verification.Object.UserName = "A";
            mock_verification.Object.UserName = "B";
            mock_verification.Object.UserName = "C";
            mock_verification.VerifySet(m => m.UserName = "ZhangJin", Times.Once());   // 验证属性被设置过了一次(设置值为 ZhangJin)
            mock_verification.VerifySet(m => m.UserName =
                It.IsInRange<string>("A", "Z", Range.Inclusive), Times.Exactly(3));   // 验证属性被设置过了一次

            #endregion

            Console.ReadLine();
        }
    }
}