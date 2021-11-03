﻿using Microsoft.Crm.Sdk.Messages;
using Xunit;

namespace FakeXrmEasy.Messages.Tests.FakeMessageExecutors.RetrieveVersionRequestTests
{
    public class RetrieveVersionRequestTests : FakeXrmEasyTestsBase
    {
        [Fact]
        public void AddsFakeVersionRequest()
        {
            var fakeVersionRequest = new RetrieveVersionRequest();
            var result = (RetrieveVersionResponse)_service.Execute(fakeVersionRequest);
            var version = result.Version;
            var versionComponents = version.Split('.');

            var majorVersion = versionComponents[0];
            var minorVersion = versionComponents[1];

#if FAKE_XRM_EASY_9

            Assert.True(int.Parse(majorVersion) >= 9);

#elif FAKE_XRM_EASY_365

            Assert.True(int.Parse(majorVersion) >= 8);

            if (majorVersion == "8")
            {
                Assert.True(int.Parse(minorVersion) >= 2);
            }

#elif FAKE_XRM_EASY_2016

            Assert.True(int.Parse(majorVersion) >= 8);            
            Assert.True(int.Parse(minorVersion) >= 0);
            Assert.True(int.Parse(minorVersion) < 2);

#elif FAKE_XRM_EASY_2015

            Assert.True(version.StartsWith("7"));

#elif FAKE_XRM_EASY_2013

            Assert.True(version.StartsWith("6"));

#elif FAKE_XRM_EASY

            Assert.True(version.StartsWith("5"));
#endif
        }
    }
}
