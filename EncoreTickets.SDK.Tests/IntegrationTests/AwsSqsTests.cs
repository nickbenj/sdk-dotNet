﻿using System.Net;
using System.Threading.Tasks;
using EncoreTickets.SDK.Aws;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace EncoreTickets.SDK.Tests.IntegrationTests
{
    [TestFixture]
    class AwsSqsTests
    {
        private IConfiguration configuration;
        private AwsSqs sqs;
        private bool runTests = false;

        [SetUp]
        public void SetupState()
        {
            configuration = ConfigurationHelper.GetConfiguration();
            sqs = CreateSqs();
            if (!runTests)
            {
                Assert.Ignore("The SQS tests are disabled by default because it is a paid service. Set 'runTests' field to true to run the tests.");
            }
        }

        [Test]
        public async Task TestSendMessage()
        {
            var result = await sqs.SendMessageAsync(configuration["AWS_SQS:QueueUrl"], "test message");

            Assert.AreEqual(HttpStatusCode.OK, result.HttpStatusCode);
        }

        private AwsSqs CreateSqs()
        {
            var options = configuration.GetAWSOptions();
            var profile = options.Profile;
            var region = options.Region.SystemName;
            var accessKey = configuration["AWS_SQS:Credentials:AccessKey"];
            var secretKey = configuration["AWS_SQS:Credentials:SecretKey"];
            return new AwsSqs(profile, region, accessKey, secretKey);
        }
    }
}