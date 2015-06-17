using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using Moq;
using System.Web;
using System.Web.Mvc;
using BasicMvcApp.Areas.Admin;
using BasicMvcApp.Areas.Blog;

namespace BasicMvcApp.Tests
{
    [TestClass]
    public class RoutesTesting
    {
        [TestMethod]
        public void ForwardSlashGoesToHomeIndex()
        {
            TestRoute("~/", new { controller = "Home", action = "Index" });
        }

        [TestMethod]
        public void TestAreaRoutes()
        {
            // Act
            string result = GenerateUrlViaTestMocks(
                new
                {
                    area = "Admin",
                    controller = "Stats",
                    action = "Export"
                });

            // Assert
            Assert.AreEqual("/Admin/Stats/Export", result);
        }

        private RouteData TestRoute(string url, object expectedValues)
        {
            // Arrange
            RouteCollection routeConfig = new RouteCollection();
            // RegisterRoute
            ResigterAllAreas(routeConfig);
            BasicMvcApp.RouteConfig.RegisterRoutes(routeConfig);
            var mockHttpContext = MakeMockHttpContext(url);

            // Act
            // 根据当前的 HttpContext 获取路由数据
            RouteData routeData = routeConfig.GetRouteData(mockHttpContext.Object);

            // Assert
            Assert.IsNotNull(routeData.Route, "No route was matched");
            // 根据 object 对象，生成字典
            var expectedDict = new RouteValueDictionary(expectedValues);

            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                {
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                }
                else
                {
                    Assert.AreEqual(routeData.Values[expectedVal.Key].ToString(),
                        expectedVal.Value.ToString());
                }
            }

            //  返回一个 RouteData 便于后续的测试使用
            return routeData;
        }

        private static Mock<HttpContextBase> MakeMockHttpContext(string url)
        {
            var mockHttpContext = new Mock<HttpContextBase>();

            // Mock the request
            var mockRequest = new Mock<HttpRequestBase>();
            mockHttpContext.Setup(m => m.Request).Returns(mockRequest.Object);
            // 设置 ~/abc 格式的路径
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(url);

            // Mock the response
            var mockResponse = new Mock<HttpResponseBase>();
            mockHttpContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // 在生成 Url 时会使用
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(m => m);

            return mockHttpContext;
        }

        [TestMethod]
        public void TestOutboundUrl()
        {
            string result = GenerateUrlViaTestMocks(
                new { controller = "Products", action = "Edit", id = 50 }
            );

            Assert.AreEqual("/Products/Edit/50", result);
        }

        private string GenerateUrlViaTestMocks(object values)
        {
            // Arrange
            RouteCollection routeConfig = new RouteCollection();
            // Register Area
            ResigterAllAreas(routeConfig);
            BasicMvcApp.RouteConfig.RegisterRoutes(routeConfig);
            var mockContext = MakeMockHttpContext(null);
            RequestContext context = new RequestContext(mockContext.Object, new RouteData());

            // Act
            return UrlHelper.GenerateUrl(null, null, null,
                new RouteValueDictionary(values),
                routeConfig, context, true);
        }

        private void ResigterAllAreas(RouteCollection routes)
        {
            var allAreas = new AreaRegistration[] { 
                new AdminAreaRegistration(),
                new BlogAreaRegistration()
            };

            foreach (AreaRegistration area in allAreas)
            {
                var context = new AreaRegistrationContext(area.AreaName, routes);
                context.Namespaces.Add(area.GetType().Namespace);
                area.RegisterArea(context);
            }
        }
    }
}
