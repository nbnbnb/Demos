using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeLib.Fakes;
using FakeLib;
using Microsoft.QualityTools.Testing.Fakes.Stubs;
using Microsoft.QualityTools.Testing.Fakes;
using System.Fakes;
using System.IO.Fakes;
using System.IO;
using System.Collections.Generic;

namespace FakesDemoUnitTest
{
    // 类名的命名方式为： 测试类名+Test
    // 测试方法的命名方式为： 测试方法名_执行结果_得到此结果的条件/原因
    // Fakes 视频
    // https://channel9.msdn.com/Series/Visual-Studio-2012-Premium-and-Ultimate-Overview/Visual-Studio-Ultimate-2012-Improving-quality-with-unit-tests-and-fakes
    [TestClass]
    public class FakeLibTest
    {
        private IFileSystem _fs;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void InitClass()
        {
            // Arrange
            var stub = new StubIFileSystem();

            // 方法
            stub.MyMethodString = str => 1;

            // 属性
            // 返回值
            stub.MyAgeGet = () => 30;

            // 事件                        
            // 订阅事件
            stub.MyChangeEventEvent = (o, e) =>
            {
                Console.WriteLine("I am Get the event");
            };

            //  泛型对象
            stub.GetValueOf1<int>(() => 123);
            stub.GetValueOf1<string>(() => "123");

            // 为所有的未实现的Fake对象使用默认值
            // StubBehaviors.Current = StubBehaviors.DefaultValue;

            //**** 未实现 ******
            // 1, 带有指针类型的参数签名方法
            // 2, 对于sealed类或静态方法，需要使用填充隔离技术 (isolate calls) 

            // Act
            // 可以隐式转换
            _fs = stub;
        }

        #region Basic
        [TestMethod]
        public void ReadAllText_FileIsNotEmpty()
        {
            // Arrange
            var fileName = "MyFile.log";
            var content = "hello world";

            var fileSystem = new StubIFileSystem()
            {
                ReadAllTextString = fname =>
                {
                    Assert.AreEqual(fileName, fname);
                    return content;
                }
            };

            // Act
            // 在调用IsEmpty方法时，将会执行 ReadAllTextString方法
            bool result = FileHelper.IsEmpty(fileSystem, fileName);

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void FakeMethod()
        {
            // Assert
            Assert.AreEqual(_fs.MyMethod("abc"), 1);
        }

        [TestMethod]
        public void FakeProperty()
        {
            int age = _fs.MyAge;
            Assert.AreEqual(age, 30);

            _fs.MyAge = 40;
            // 调用此方法时，就会调用MyAgeGet委托，所以，总是返回的30
            Assert.AreEqual(30, _fs.MyAge);
        }

        [TestMethod]
        public void FakeEvent()
        {
            // 此处进行强制转换
            StubIFileSystem stub = (StubIFileSystem)_fs;

            Console.WriteLine("I am going raise the event");

            // 触发事件
            stub.MyChangeEventEvent(stub, EventArgs.Empty);
        }

        [TestMethod]
        public void FakeGeneric()
        {
            int i = _fs.GetValue<int>();
            string s = _fs.GetValue<string>();

            // Assert
            Assert.AreEqual(123, i);
            Assert.AreEqual("123", s);
        }

        [TestMethod]
        public void PartialStubs()
        {
            var stub = new StubBaseClass()   // StubBaseClass 会继承自BaseClass
            {
                // 此参数表明，是否该调用基方法而不调用回退行为
                // 如果此参数设置为false
                // 这 stub对象的方法将会返回null(因为还没有设置)
                CallBase = true
            };

            Assert.AreEqual("ZhangJin", stub.GetName());
        }

        [TestMethod]
        public void StubBehaviorTest_DefaultValue()
        {
            StubIFileSystem stub = new StubIFileSystem();

            // 为默认实现
            // 表示对于值类型，如int，会返回0，引用类型会返回null
            stub.BehaveAsDefaultValue();

            // 默认未实现
            // 表示对于没有赋值的对象，会引发 NotImplementedException 异常
            // stub.BehaveAsNotImplemented();

            // 此两个设置将会进行覆盖
            // 后一个设置将会覆盖前一个设置，两者只能二选一

            // 将会使用此属性的默认值,int 类型返回的就为0
            int age = ((IFileSystem)stub).MyAge;

            Assert.AreEqual(0, age);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void StubBehaviorTest_NotImplemented()
        {
            StubIFileSystem stub = new StubIFileSystem();

            // 表示对于没有赋值的对象，会引发 NotImplementedException 异常
            stub.BehaveAsNotImplemented();

            // 读取此属性时，将会抛出异常 NotImplementedException
            int age = ((IFileSystem)stub).MyAge;

            Assert.AreEqual(0, age);
        }

        #endregion

        #region Isolate Calls

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void Y2KChecker_ApplicationException_IsY2k()
        {
            using (ShimsContext.Create())
            {
                // 对 System.dll程序集进行 Fake
                ShimDateTime.NowGet = () => new DateTime(2000, 1, 1);

                // 当在 Check() 方法中调用System.DateTime.Now 时，返回的是 ShimDateTime.NewGet函数返回的值
                Y2KChecker.Check();

            }
        }

        [TestMethod]
        public void ShimStaticMethod()
        {
            using (ShimsContext.Create())
            {
                // 设置静态方法的返回值
                ShimMyStaticClass.GetAge = () => 30;
                Assert.AreEqual(30, MyStaticClass.GetAge());
            }
        }

        [TestMethod]
        public void ShimInstanceMethodForAll()
        {
            using (ShimsContext.Create())
            {
                // 在此上下文中，所有的实例调用此方法时，都会返回这个值
                ShimMyClass.AllInstances.MyMethod = (self) => 5;

                MyClass my = new MyClass();

                MyClass my2 = new MyClass();

                // MyMethod 方法默认情况下返回 0
                Assert.AreEqual(5, my.MyMethod());

                Assert.AreEqual(5, my2.MyMethod());
            }
        }

        [TestMethod]
        public void ShimInstanceMethodForSingle()
        {
            using (ShimsContext.Create())
            {
                // 此处使用了隐式转换
                MyClass my = new ShimMyClass()
                {
                    MyMethod = () => 5
                };

                // 未使用代码填入(Shim)
                MyClass my2 = new MyClass();

                Assert.AreEqual(5, my.MyMethod());
                Assert.AreEqual(0, my2.MyMethod());

            }
        }

        [TestMethod]
        public void ShimConstructors()
        {
            using (ShimsContext.Create())
            {

                // 构造函数 代码填入
                ShimMyClass.ConstructorString = (self, value) =>
                {
                    // 填入的代码实例化方法
                    // 需要执行代码填入的对象为 self,当调用self的Name属性时，就会返回NameGet的填充码
                    // 初始化给定实例的新填充码
                    new ShimMyClass(self)
                   {
                       NameGet = () => "ZhangJin"
                   };

                    // 或使用下面方式
                    // 直接设置self的属性
                    //self.Name = "ZhangJin";
                };

                // 不管此构造函数传递怎样的值
                // 最后都会使用上面的填入代码进行实例化
                MyClass my = new MyClass("haha");

                Assert.AreEqual("ZhangJin", my.Name);

            }
        }

        [TestMethod]
        public void ShimExecute()
        {
            using (ShimsContext.Create())
            {
                ShimFile.WriteAllTextStringString = (path, content) =>
                {

                    // 注意，不能在此方法中再次调用
                    // File.WriteAllText方法
                    // 这样将会导致死循环 

                    Console.WriteLine("I am in Console Write");
                };

                // 在此方法中使用的对象，将不会使用代码填充
                ShimsContext.ExecuteWithoutShims(() =>
                {
                    // 调用原始的方法
                    File.WriteAllText(@"c:\log.txt", "I am OK");
                });

                // 使用Shim（填充）方法
                File.WriteAllText(@"c:\log.txt", "I am not write");

            }
        }

        [TestMethod]
        public void ShimBaseMember()
        {
            using (ShimsContext.Create())
            {

                var child = new ShimMyChild()
                {
                    MyAddressGet = () => "China HuBei WuHan"
                };

                MyChild my = child;

                // 注意，此处是如何填充抽象类的
                MyBase mybase = new ShimMyBase(child)
                {
                    // 注意，此处同时对 child 和本身进行了代码填充
                    MyMethod = () => "Fake Method"
                };

                Assert.AreEqual(mybase.MyMethod(), "Fake Method");
                Assert.AreEqual(my.MyAddress, "China HuBei WuHan");
                Assert.AreEqual(my.MyMethod(), "Fake Method");
            }

        }

        [TestMethod]
        public void ShimBindingInterface()
        {
            using (ShimsContext.Create())
            {
                var shimMyInterface = new ShimMyInterface();

                var stubMyInfo = new StubIInfo
                {
                    YouNameGet = () => "ZhangJin"
                };

                // 将接口的成员绑定到此填充码
                shimMyInterface.Bind(new[] { 1, 2, 3, 4, 5 });

                // 将接口的成员绑定到此填充码，类似与此对象实现了此接口
                shimMyInterface.Bind(stubMyInfo);

                MyInterface itf = shimMyInterface;

                int sum = 0;
                foreach (int i in itf)
                {
                    sum += i;
                }
                Assert.AreEqual(15, sum);
                Assert.AreEqual("ZhangJin", itf.YouName);

            }
        }

        [TestMethod]
        public void TempTest()
        {

        }

        [TestCleanup]
        public void MethodCleanup()
        {
            //MyClass child = new MyClass("abc");
            //File.WriteAllText(@"c:\log.txt", child.Name);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {

        }
        #endregion

    }


}
