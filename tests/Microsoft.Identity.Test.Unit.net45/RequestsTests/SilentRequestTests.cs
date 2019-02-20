// ------------------------------------------------------------------------------
// 
// Copyright (c) Microsoft Corporation.
// All rights reserved.
// 
// This code is licensed under the MIT License.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.ApiConfig.Parameters;
using Microsoft.Identity.Client.Cache;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Internal.Requests;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Test.Common;
using Microsoft.Identity.Test.Common.Core.Helpers;
using Microsoft.Identity.Test.Common.Core.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Identity.Client.Internal.Broker;
using Microsoft.Identity.Client.OAuth2;

namespace Microsoft.Identity.Test.Unit.RequestsTests
{
    [TestClass]
    public class SilentRequestTests
    {
        private TokenCacheHelper _tokenCacheHelper;

        [TestInitialize]
        public void TestInitialize()
        {
            TestCommon.ResetStateAndInitMsal();
            _tokenCacheHelper = new TokenCacheHelper();
        }

        [TestMethod]
        [TestCategory("SilentRequestTests")]
        public void ConstructorTests()
        {
            using (var harness = new MockHttpTestHarness(MsalTestConstants.AuthorityHomeTenant))
            {
                var parameters = harness.CreateRequestParams(harness.Cache, null);
                var silentParameters = new AcquireTokenSilentParameters();
                var request = new SilentRequest(harness.ServiceBundle, parameters, silentParameters);
                Assert.IsNotNull(request);

                parameters.Account = new Account(MsalTestConstants.UserIdentifier, MsalTestConstants.DisplayableId, null);
                request = new SilentRequest(harness.ServiceBundle, parameters, silentParameters);
                Assert.IsNotNull(request);

                request = new SilentRequest(harness.ServiceBundle, parameters, silentParameters);
                Assert.IsNotNull(request);
            }
        }

        [TestMethod]
        [TestCategory("SilentRequestTests")]
        public void ExpiredTokenRefreshFlowTest()
        {
            IDictionary<string, string> extraQueryParamsAndClaims =
               MsalTestConstants.ExtraQueryParams.ToDictionary(e => e.Key, e => e.Value);
            extraQueryParamsAndClaims.Add(OAuth2Parameter.Claims, MsalTestConstants.Claims);

            using (var harness = new MockHttpTestHarness(MsalTestConstants.AuthorityHomeTenant))
            {
                _tokenCacheHelper.PopulateCache(harness.Cache.Accessor);
                var parameters = harness.CreateRequestParams(
                    harness.Cache,
                    null,
                    MsalTestConstants.ExtraQueryParams,
                    MsalTestConstants.Claims);
                var silentParameters = new AcquireTokenSilentParameters();

                // set access tokens as expired
                foreach (var accessItem in harness.Cache.GetAllAccessTokens(true))
                {
                    accessItem.ExpiresOnUnixTimestamp =
                        ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds)
                        .ToString(CultureInfo.InvariantCulture);

                    harness.Cache.AddAccessTokenCacheItem(accessItem);
                }

                TestCommon.MockInstanceDiscoveryAndOpenIdRequest(harness.HttpManager);

                harness.HttpManager.AddMockHandler(
                    new MockHttpMessageHandler()
                    {
                        ExpectedMethod = HttpMethod.Post,
                        ResponseMessage = MockHelpers.CreateSuccessTokenResponseMessage(),
                        ExpectedQueryParams = extraQueryParamsAndClaims
                    });

                var request = new SilentRequest(harness.ServiceBundle, parameters, silentParameters);

                Task<AuthenticationResult> task = request.RunAsync(CancellationToken.None);
                var result = task.Result;
                Assert.IsNotNull(result);
                Assert.AreEqual("some-access-token", result.AccessToken);
                Assert.AreEqual(MsalTestConstants.Scope.AsSingleString(), result.Scopes.AsSingleString());
            }
        }

        [TestMethod]
        [TestCategory("SilentRequestTests")]
        public void SilentRefreshFailedNullCacheTest()
        {
            using (var harness = new MockHttpTestHarness(MsalTestConstants.AuthorityHomeTenant))
            {
                var parameters = harness.CreateRequestParams(
                    null,
                    ScopeHelper.CreateSortedSetFromEnumerable(
                        new[]
                        {
                            "some-scope1",
                            "some-scope2"
                        }));

                var silentParameters = new AcquireTokenSilentParameters();

                try
                {
                    var request = new SilentRequest(harness.ServiceBundle, parameters, silentParameters);
                    Task<AuthenticationResult> task = request.RunAsync(CancellationToken.None);
                    var authenticationResult = task.Result;
                    Assert.Fail("MsalUiRequiredException should be thrown here");
                }
                catch (AggregateException ae)
                {
                    var exc = ae.InnerException as MsalUiRequiredException;
                    Assert.IsNotNull(exc);
                    Assert.AreEqual(MsalUiRequiredException.TokenCacheNullError, exc.ErrorCode);
                }
            }
        }

        [TestMethod]
        [TestCategory("SilentRequestTests")]
        public void SilentRefreshFailedNoCacheItemFoundTest()
        {
            using (var harness = new MockHttpTestHarness(MsalTestConstants.AuthorityHomeTenant))
            {
                harness.HttpManager.AddInstanceDiscoveryMockHandler();

                var parameters = harness.CreateRequestParams(
                    harness.Cache,
                    ScopeHelper.CreateSortedSetFromEnumerable(
                        new[]
                        {
                            "some-scope1",
                            "some-scope2"
                        }));
                var silentParameters = new AcquireTokenSilentParameters();

                try
                {
                    var request = new SilentRequest(harness.ServiceBundle, parameters, silentParameters);
                    Task<AuthenticationResult> task = request.RunAsync(CancellationToken.None);
                    var authenticationResult = task.Result;
                    Assert.Fail("MsalUiRequiredException should be thrown here");
                }
                catch (AggregateException ae)
                {
                    var exc = ae.InnerException as MsalUiRequiredException;
                    Assert.IsNotNull(exc, "Actual exception type is " + ae.InnerException.GetType());
                    Assert.AreEqual(MsalUiRequiredException.NoTokensFoundError, exc.ErrorCode);
                }
            }
        }

        [TestMethod]
        [Description("Test setting of the broker parameters in the BrokerSilentRequest constructor.")]
        public void BrokerSilentRequest_CreateBrokerParametersTest()
        {
            using (var harness = new MockHttpTestHarness(MsalTestConstants.AuthorityHomeTenant))
            {
                // Arrange
                var parameters = harness.CreateRequestParams(
                    harness.Cache,
                    MsalTestConstants.Scope,
                    MsalTestConstants.ExtraQueryParams);

                // Act
                var brokerParameters = parameters.CreateSilentRequestParametersForBroker();

                // Assert
                Assert.AreEqual(11, brokerParameters.Count);

                Assert.AreEqual(harness.Authority.AuthorityInfo.CanonicalAuthority, brokerParameters[BrokerParameter.Authority]);
                Assert.AreEqual(MsalTestConstants.ScopeStr, brokerParameters[BrokerParameter.RequestScopes]);
                Assert.AreEqual(MsalTestConstants.ClientId, brokerParameters[BrokerParameter.ClientId]);

                Assert.AreEqual(harness.ServiceBundle.DefaultLogger.CorrelationId.ToString(), brokerParameters[BrokerParameter.CorrelationId]);
                Assert.AreEqual(MsalIdHelper.GetMsalVersion(), brokerParameters[BrokerParameter.ClientVersion]);
                Assert.AreEqual(MsalTestConstants.DisplayableId, brokerParameters[BrokerParameter.Username]);

                Assert.AreEqual(MsalTestConstants.BrokerExtraQueryParameters, brokerParameters[BrokerParameter.ExtraQp]);

                // Assert.AreEqual(MsalTestConstants.BrokerClaims, brokerParameters[BrokerParameter.Claims]);
                Assert.AreEqual(BrokerParameter.OidcScopesValue, brokerParameters[BrokerParameter.ExtraOidcScopes]);
                Assert.IsTrue(brokerParameters.ContainsKey(BrokerParameter.SilentBrokerFlow));
            }
        }

        private class MockHttpTestHarness : IDisposable
        {
            private readonly MockHttpAndServiceBundle _mockHttpAndServiceBundle;

            public MockHttpTestHarness(string authorityUri)
            {
                _mockHttpAndServiceBundle = new MockHttpAndServiceBundle();
                Authority = Authority.CreateAuthority(ServiceBundle, authorityUri);
                Cache = new TokenCache(ServiceBundle);
            }

            public IServiceBundle ServiceBundle => _mockHttpAndServiceBundle.ServiceBundle;
            public MockHttpManager HttpManager => _mockHttpAndServiceBundle.HttpManager;
            public Authority Authority { get; }
            public ITokenCacheInternal Cache { get; }

            /// <inheritdoc />
            public void Dispose()
            {
                _mockHttpAndServiceBundle.Dispose();
            }

            public AuthenticationRequestParameters CreateRequestParams(
                ITokenCacheInternal cache,
                SortedSet<string> scopes,
                IDictionary<string, string> extraQueryParams = null,
                string claims = null)
            {
                var commonParameters = new AcquireTokenCommonParameters
                {
                    Scopes = scopes ?? MsalTestConstants.Scope,
                    ExtraQueryParameters = extraQueryParams,
                    Claims = claims
                };

                var parameters = new AuthenticationRequestParameters(
                    ServiceBundle,
                    Authority,
                    cache,
                    commonParameters,
                    RequestContext.CreateForTest(ServiceBundle))
                {
                    Account = new Account(MsalTestConstants.UserIdentifier, MsalTestConstants.DisplayableId, null),

                };
                return parameters;
            }
        }
    }
}