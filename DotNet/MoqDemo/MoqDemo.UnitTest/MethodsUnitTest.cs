using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MoqDemo.Domain.Abstract;

namespace MoqDemo.UnitTest
{
    [TestClass]
    public class MethodsUnitTest
    {
        [TestMethod]
        public void Methods()
        {
            // Arrange
            var mock = new Mock<IMethods>();
            mock.Setup(m => m.DoSomething("ping")).Returns(true);

            // out arguments
            var outString="ack";
            mock.Setup(m => m.TryParse("ping", out outString)).Returns(true);

            // access invocation arguments when returning a value
            mock.Setup(m => m.DoSomething(It.IsAny<string>()))
                .Returns((string s) => s.Length > 5);

            // returning different values on each invocation
            var calls = 0;
            mock.Setup(m => m.GetCountThing())
                .Returns(() => calls)
                .Callback(() => calls++);
        }
    }
}
