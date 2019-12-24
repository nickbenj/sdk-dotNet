﻿using System.Collections.Generic;
using EncoreTickets.SDK.Api.Context;
using EncoreTickets.SDK.Api.Results;
using EncoreTickets.SDK.Api.Results.Exceptions;
using EncoreTickets.SDK.Api.Results.Response;
using EncoreTickets.SDK.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace EncoreTickets.SDK.Tests.UnitTests.Api.Results
{
    internal class ApiResultTests
    {
        [Test]
        public void ConstructorWith5Args_IfSuccessfulResponse_InitializesCommonProperties()
        {
            var data = new[] { new object(), new object(), };
            var response = RestResponseFactory.GetSuccessResponse();
            var responseContext = new SDK.Api.Results.Response.Context();
            var requestInResponse = new Request();
            var context = It.IsAny<ApiContext>();

            var result = new ApiResult<object[]>(data, response, context, responseContext, requestInResponse);

            Assert.AreEqual(context, result.Context);
            Assert.AreEqual(response, result.RestResponse);
            Assert.AreEqual(responseContext, result.ResponseContext);
            Assert.AreEqual(requestInResponse, result.RequestInResponse);
            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(default, result.ApiException);
            Assert.AreEqual(data, result.DataOrException);
            Assert.AreEqual(data, result.DataOrDefault);
        }

        [Test]
        public void ConstructorWith5Args_IfUnsuccessfulResponse_InitializesCommonProperties()
        {
            var response = RestResponseFactory.GetFailedResponse();
            var responseContext = new SDK.Api.Results.Response.Context();
            var requestInResponse = new Request();
            var context = It.IsAny<ApiContext>();

            var result = new ApiResult<object>(null, response, context, responseContext, requestInResponse);

            Assert.AreEqual(context, result.Context);
            Assert.AreEqual(response, result.RestResponse);
            Assert.AreEqual(responseContext, result.ResponseContext);
            Assert.AreEqual(requestInResponse, result.RequestInResponse);
            Assert.AreEqual(false, result.IsSuccessful);
            Assert.AreEqual(default, result.DataOrDefault);
            var thrownException = Assert.Catch<ApiException>(() =>
            {
                var data = result.DataOrException;
            });
            Assert.AreEqual(thrownException, result.ApiException);
        }

        [Test]
        public void ConstructorWith4Args_IfSuccessfulResponse_InitializesCommonProperties()
        {
            var data = new[] { new object(), new object(), };
            var response = RestResponseFactory.GetSuccessResponse();
            var context = It.IsAny<ApiContext>();
            var error = (string) null;

            var result = new ApiResult<object>(data, response, context, error);

            Assert.AreEqual(context, result.Context);
            Assert.AreEqual(response, result.RestResponse);
            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(default, result.ApiException);
            Assert.AreEqual(data, result.DataOrException);
            Assert.AreEqual(data, result.DataOrDefault);
        }

        [Test]
        public void ConstructorWith4Args_IfUnsuccessfulResponse_InitializesCommonProperties()
        {
            object[] data = null;
            var response = RestResponseFactory.GetFailedResponse();
            var context = It.IsAny<ApiContext>();
            var error = "Error";

            var result = new ApiResult<object>(data, response, context, error);

            Assert.AreEqual(context, result.Context);
            Assert.AreEqual(response, result.RestResponse);
            Assert.AreEqual(false, result.IsSuccessful);
            Assert.AreEqual(default, result.DataOrDefault);
            var thrownException = Assert.Catch<ApiException>(() =>
            {
                var resultData = result.DataOrException;
            });
            Assert.AreEqual(thrownException, result.ApiException);
            Assert.AreEqual(error, result.ApiException.Message);
        }

        [Test]
        public void ConstructorWith3Args_IfSuccessfulResponse_InitializesCommonProperties()
        {
            var data = new[] { new object(), new object(), };
            var response = RestResponseFactory.GetSuccessResponse();
            var context = It.IsAny<ApiContext>();

            var result = new ApiResult<object>(data, response, context);

            Assert.AreEqual(context, result.Context);
            Assert.AreEqual(response, result.RestResponse);
            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(default, result.ApiException);
            Assert.AreEqual(data, result.DataOrException);
            Assert.AreEqual(data, result.DataOrDefault);
        }

        [Test]
        public void ConstructorWith3Args_IfUnsuccessfulResponse_InitializesCommonProperties()
        {
            object[] data = null;
            var response = RestResponseFactory.GetFailedResponse();
            var context = It.IsAny<ApiContext>();

            var result = new ApiResult<object>(data, response, context);

            Assert.AreEqual(context, result.Context);
            Assert.AreEqual(response, result.RestResponse);
            Assert.AreEqual(false, result.IsSuccessful);
            Assert.AreEqual(default, result.DataOrDefault);
            var thrownException = Assert.Catch<ApiException>(() =>
            {
                var resultData = result.DataOrException;
            });
            Assert.AreEqual(thrownException, result.ApiException);
        }

        [TestCaseSource(typeof(ApiResultTestsSource), nameof(ApiResultTestsSource.GetDataOrContextException_IfContextWithoutInfosAsErrors_ReturnsData))]
        public void GetDataOrContextException_IfContextWithoutInfosAsErrors_ReturnsData(string codeOfInfoAsError, SDK.Api.Results.Response.Context responseContext)
        {
            var data = new[] { new object(), new object(), };
            var response = RestResponseFactory.GetSuccessResponse();
            var result = new ApiResult<object[]>(data, response, It.IsAny<ApiContext>(), responseContext, It.IsAny<Request>());

            var actual = result.GetDataOrContextException(codeOfInfoAsError);

            Assert.AreEqual(data, actual);
        }

        [TestCaseSource(typeof(ApiResultTestsSource), nameof(ApiResultTestsSource.GetDataOrContextException_IfContextWithInfosAsErrors_ThrowsException))]
        public void GetDataOrContextException_IfContextWithInfosAsErrors_ThrowsException(string codeOfInfoAsError, SDK.Api.Results.Response.Context responseContext)
        {
            var data = new[] { new object(), new object(), };
            var response = RestResponseFactory.GetSuccessResponse();
            var result = new ApiResult<object[]>(data, response, It.IsAny<ApiContext>(), responseContext, It.IsAny<Request>());
            
            Assert.Catch<ContextApiException>(() => result.GetDataOrContextException(codeOfInfoAsError));
        }
    }

    public static class ApiResultTestsSource
    {
        public static IEnumerable<TestCaseData> GetDataOrContextException_IfContextWithoutInfosAsErrors_ReturnsData = new[]
        {
            new TestCaseData(
                "notValidPromotionCode",
                new SDK.Api.Results.Response.Context()
            ) {TestName = "GetDataOrContextException_IfContextIsNull_ReturnsData"},
            new TestCaseData(
                "notValidPromotionCode",
                new SDK.Api.Results.Response.Context()
            ) {TestName = "GetDataOrContextException_IfInfoInContextIsNull_ReturnsData"},
            new TestCaseData(
                "notValidPromotionCode",
                new SDK.Api.Results.Response.Context {Info = new List<Info>()}
            ) {TestName = "GetDataOrContextException_IfInfoInContextIsEmptyCollection_ReturnsData"},
            new TestCaseData(
                "notValidPromotionCode",
                new SDK.Api.Results.Response.Context {Info = new List<Info> {new Info {Code = "information"}}}
            ) {TestName = "GetDataOrContextException_IfInfoWithCodeDoesNotExistInInfoInContext_ReturnsData"},
        };


        public static IEnumerable<TestCaseData> GetDataOrContextException_IfContextWithInfosAsErrors_ThrowsException = new[]
        {
            new TestCaseData(
                "notValidPromotionCode",
                new SDK.Api.Results.Response.Context {Info = new List<Info> {new Info {Code = "notValidPromotionCode" } }}
            ) {TestName = "GetDataOrContextException_IfInfoWithCodeExistsInInfoInContext_ThrowsException"},
        };
    }
}