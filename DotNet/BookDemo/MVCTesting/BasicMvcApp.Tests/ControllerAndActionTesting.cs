using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicMvcApp.Controllers;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Routing;

namespace BasicMvcApp.Tests
{
    [TestClass]
    public class ControllerAndActionTesting
    {
        [TestMethod]
        public void Index_Render_MyView()
        {
            // Arrange
            SimpleController controller = new SimpleController();

            // Act
            ViewResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result, "Did not render a view");
            Assert.AreEqual("MyView", result.ViewName);
        }

        [TestMethod]
        public void ShowAge_WhenBornSixYearsTwoDaysAgo_DisplayAge6()
        {
            // Arrange
            SimpleController controller = new SimpleController();
            DateTime birthDate = DateTime.Now.AddYears(-6).AddDays(-2);

            // Act
            ViewResult result = controller.ShowAge(birthDate);

            // Assert
            Assert.AreEqual(6, result.ViewData["age"]);
        }

        [TestMethod]
        public void RegisterForUpdates_AcceptsValidEmailAddress()
        {
            // Arrange
            string validEmail = "zhangjin@live.me";
            SimpleController controller = new SimpleController();

            // Act
            RedirectToRouteResult result = controller.RegisterForUpdates(validEmail);

            // Assert
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("RegisterationCompleted", result.RouteValues["action"]);
        }

        [TestMethod]
        public void RegisterForUpdates_RejectsInvalidEmailAddress()
        {
            // Arrange
            string inValidEmail = "zhangjin";
            SimpleController controller = new SimpleController();

            // Act
            RedirectToRouteResult result = controller.RegisterForUpdates(inValidEmail);

            // Assert
            Assert.IsNotNull(result, "Should have redirected");
            Assert.AreEqual("Register", result.RouteValues["action"]);
        }

        [TestMethod]
        public void HomePage_Recognizes_New_Visitor_And_Sets_Cookie()
        {
            // Arrange
            var controller = new Mock<SimpleController> { CallBase = true };
            var mocks = new ContextMocks(controller.Object);
            controller.Setup(m => m.IncomingHasVisitedBeforeCookie).Returns((HttpCookie)null);


            // Act
            ViewResult result = controller.Object.HomePage();

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsTrue((bool)result.ViewData["IsFirstVisit"]);
            Assert.AreEqual(1, controller.Object.Response.Cookies.Count);
            Assert.AreEqual(bool.TrueString, 
                controller.Object.OutgoingHasVisitedBeforeCookie.Value);
        }

        [TestMethod]
        public void Homepage_Recognizes_Previous_Visitor()
        {
            // Arrange
            var controller = new Mock<SimpleController> { CallBase = true };
            var mocks = new ContextMocks(controller.Object);

            // 设置已经访问过
            controller.Setup(m => m.IncomingHasVisitedBeforeCookie)
                .Returns(new HttpCookie("HasVisitedBefore", bool.TrueString));

            // Act
            ViewResult result = controller.Object.HomePage();

            // Assert
            Assert.IsTrue(result.ViewName == "HomePage" || result.ViewName == "");
            Assert.IsFalse((bool)result.ViewData["IsFirstVisit"]);
        }
    }
}
