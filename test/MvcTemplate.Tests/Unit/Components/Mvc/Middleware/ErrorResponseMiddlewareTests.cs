using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MvcTemplate.Components.Mvc
{
    public class ErrorResponseMiddlewareTests
    {
        [Theory]
        [InlineData("/sales/order", "/home/not-found")]
        [InlineData("/en/sales/order", "/en-GB/home/not-found")]
        public async Task Invoke_NotFound(String path, String requestPath)
        {
            ILanguages languages = new Languages("en-GB", new[] { new Language { Abbreviation = "en-GB" } });
            ILogger<ErrorResponseMiddleware> logger = Substitute.For<ILogger<ErrorResponseMiddleware>>();
            HttpContext actual = HttpFactory.CreateHttpContext();
            actual.Request.RouteValues.Add("test", "test");
            actual.Request.Path = path;
            Boolean asserted = false;

            await new ErrorResponseMiddleware(actual =>
            {
                if (actual.Response.StatusCode != StatusCodes.Status404NotFound)
                    actual.Response.StatusCode = StatusCodes.Status404NotFound;
                else
                    asserted = true;

                return Task.CompletedTask;
            }, languages, logger).Invoke(actual);

            Assert.True(asserted);
            Assert.Empty(actual.Request.RouteValues);
            Assert.Equal(requestPath, actual.Request.Path);
            Assert.Equal(HttpMethods.Get, actual.Request.Method);
        }

        [Theory]
        [InlineData("/sales/order", "/home/error")]
        [InlineData("/en/sales/order", "/en-GB/home/error")]
        public async Task Invoke_Error(String path, String requestPath)
        {
            ILanguages languages = new Languages("en-GB", new[] { new Language { Abbreviation = "en-GB" } });
            ILogger<ErrorResponseMiddleware> logger = Substitute.For<ILogger<ErrorResponseMiddleware>>();
            HttpContext actual = HttpFactory.CreateHttpContext();
            actual.Request.RouteValues.Add("test", "test");
            actual.Request.Path = path;
            Boolean asserted = false;

            await new ErrorResponseMiddleware(actual =>
            {
                Assert.Equal(requestPath, actual.Request.Path);

                asserted = true;

                return Task.CompletedTask;
            }, languages, logger).Invoke(actual);

            Assert.True(asserted);
            Assert.Empty(actual.Request.RouteValues);
            Assert.Equal(requestPath, actual.Request.Path);
            Assert.Equal(HttpMethods.Get, actual.Request.Method);
        }
    }
}
