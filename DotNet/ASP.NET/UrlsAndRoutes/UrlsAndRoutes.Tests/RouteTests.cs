using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Reflection;
using System.Web.Mvc;

namespace UrlsAndRoutes.Tests
{
    [TestClass]
    public class RouteTests
    {
        [TestMethod]
        public void TestIncomingRoutes()
        {
            TestRouteMatch("~/", "Home", "Index");
        }

        [TestMethod]
        public void TestOutgoingRoutes()
        {
            //arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var context = new RequestContext(CreateHttpContext(), new RouteData());

            //act
            string result = UrlHelper.GenerateUrl(null, "Index", "Home", null, routes, context, true);

            //assert
            Assert.AreEqual("/AppDoIndex", result);
        }

        private void TestRouteMatch(string url, string controller, string action,
                                    object routeProperties = null, string httpMethod = "GET")
        {
            //arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //act
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["controller"], controller)
                          && valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propertyInfos = propertySet.GetType().GetProperties();
                foreach(var propertyInfo in propertyInfos)
                {
                    if (!(routeResult.Values.ContainsKey(propertyInfo.Name)
                        && valCompare(routeResult.Values[propertyInfo.Name], propertyInfo.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        private void TestRouteFail(string url)
        {
            //arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //act
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            //assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        private HttpContextBase CreateHttpContext(string targetUrl = null,
                                          string httpMethod = "GET")
        {
            //create mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            //create mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(s => s);

            //create mock context
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //return the mocked context
            return mockContext.Object;
        }
    }
}
