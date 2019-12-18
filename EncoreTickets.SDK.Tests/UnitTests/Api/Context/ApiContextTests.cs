﻿using EncoreTickets.SDK.Api.Context;
using EncoreTickets.SDK.Utilities.Enums;
using NUnit.Framework;

namespace EncoreTickets.SDK.Tests.UnitTests.Api.Context
{
    internal class ApiContextTests
    {
        [TestCase(Environments.Production, "username", "password")]
        [TestCase(Environments.Production, "", "")]
        [TestCase(Environments.Production, null, null)]
        [TestCase(Environments.QA, "username", "password")]
        [TestCase(Environments.Sandbox, "username", "password")]
        [TestCase(Environments.Staging, "username", "password")]
        public void ConstructorWithEnvironmentAndCredentials_InitializesCorrectly(Environments env, string username, string password)
        {
            var context  = new ApiContext(env, username, password);

            Assert.AreEqual(env, context.Environment);
            Assert.AreEqual(username, context.UserName);
            Assert.AreEqual(password, context.Password);
            Assert.AreEqual(AuthenticationMethod.JWT, context.AuthenticationMethod);
            Assert.Null(context.AccessToken);
            Assert.Null(context.Affiliate);
        }

        [TestCase(Environments.Production, "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1Ni")]
        [TestCase(Environments.Production, "")]
        [TestCase(Environments.Production, null)]
        [TestCase(Environments.QA, "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1Ni")]
        [TestCase(Environments.Sandbox, "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1Ni")]
        [TestCase(Environments.Staging, "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1Ni")]
        public void ConstructorWithEnvironmentAndToken_InitializesCorrectly(Environments env, string token)
        {
            var context = new ApiContext(env, token);

            Assert.AreEqual(env, context.Environment);
            Assert.AreEqual(token, context.AccessToken);
            Assert.AreEqual(AuthenticationMethod.JWT, context.AuthenticationMethod);
            Assert.Null(context.UserName);
            Assert.Null(context.Password);
            Assert.Null(context.Affiliate);
        }

        [Test]
        public void ConstructorWithNothing_InitializesForProdEnvironment()
        {
            var context = new ApiContext();

            Assert.AreEqual(Environments.Production, context.Environment);
            Assert.AreEqual(AuthenticationMethod.JWT, context.AuthenticationMethod);
            Assert.Null(context.UserName);
            Assert.Null(context.Password);
            Assert.Null(context.AccessToken);
            Assert.Null(context.Affiliate);
        }

        [TestCase(Environments.Production)]
        [TestCase(Environments.QA)]
        [TestCase(Environments.Sandbox)]
        [TestCase(Environments.Staging)]
        public void ConstructorWithEnvironment_InitializesCorrectly(Environments env)
        {
            var context = new ApiContext(env);

            Assert.AreEqual(env, context.Environment);
            Assert.AreEqual(AuthenticationMethod.JWT, context.AuthenticationMethod);
            Assert.Null(context.UserName);
            Assert.Null(context.Password);
            Assert.Null(context.AccessToken);
            Assert.Null(context.Affiliate);
        }
    }
}
