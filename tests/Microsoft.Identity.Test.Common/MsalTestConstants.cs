﻿//----------------------------------------------------------------------
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
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Cache;

namespace Microsoft.Identity.Test.Unit
{
    internal static class MsalTestConstants
    {
        public static readonly SortedSet<string> Scope = new SortedSet<string>(new[] { "r1/scope1", "r1/scope2" });
        public const string ScopeStr = "r1/scope1 r1/scope2";
        public static readonly SortedSet<string> ScopeForAnotherResource = new SortedSet<string>(new[] { "r2/scope1", "r2/scope2" });
        public static readonly SortedSet<string> CacheMissScope = new SortedSet<string>(new[] { "r3/scope1", "r3/scope2" });
        public const string ScopeForAnotherResourceStr = "r2/scope1 r2/scope2";
        public const string Uid = "my-uid";
        public const string Utid = "my-utid";

        public const string HomeAccountId = Uid + "." + Utid;

        public const string ProductionPrefNetworkEnvironment = "login.microsoftonline.com";
        public const string ProductionPrefCacheEnvironment = "login.windows.net";
        public const string ProductionNotPrefEnvironmentAlias = "sts.windows.net";

        public const string SovereignEnvironment = "login.microsoftonline.de";
        public const string AuthorityHomeTenant = "https://" + ProductionPrefNetworkEnvironment + "/home/";
        public const string AuthorityUtidTenant = "https://" + ProductionPrefNetworkEnvironment + "/" + Utid + "/";
        public const string AuthorityGuestTenant = "https://" + ProductionPrefNetworkEnvironment + "/guest/";
        public const string AuthorityCommonTenant = "https://" + ProductionPrefNetworkEnvironment + "/common/";
        public const string PrefCacheAuthorityCommonTenant = "https://" + ProductionPrefCacheEnvironment + "/common/";
        public const string AuthorityOrganizationsTenant = "https://" + ProductionPrefNetworkEnvironment + "/organizations/";
        public const string AuthorityGuidTenant = "https://" + ProductionPrefNetworkEnvironment + "/12345679/";
        public const string AuthorityGuidTenant2 = "https://" + ProductionPrefNetworkEnvironment + "/987654321/";
        public const string AuthorityWindowsNet = "https://" + ProductionPrefCacheEnvironment + "/" + Utid + "/";
        public const string B2CAuthority = "https://login.microsoftonline.in/tfp/tenant/policy/";
        public const string B2CLoginAuthority = "https://sometenantid.b2clogin.com/tfp/sometenantid/policy/";
        public const string B2CRandomHost = "https://sometenantid.randomhost.com/tfp/sometenantid/policy/";
        public const string B2CLoginAuthorityUsGov = "https://sometenantid.b2clogin.us/tfp/sometenantid/policy/";
        public const string B2CLoginAuthorityMoonCake = "https://sometenantid.b2clogin.cn/tfp/sometenantid/policy/";
        public const string B2CLoginAuthorityBlackforest = "https://sometenantid.b2clogin.de/tfp/sometenantid/policy/";
        public const string ClientId = "d3adb33f-c0de-ed0c-c0de-deadb33fc0d3";
        public static readonly string ClientId_1 = "d3adb33f-c1de-ed1c-c1de-deadb33fc1d3";
        public const string UniqueId = "unique_id";
        public const string IdentityProvider = "my-idp";
        public const string Name = "First Last";
        public const string Claims = "claim1claim2";
        public const string DisplayableId = "displayable@id.com";
        public const string RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        public const string ClientSecret = "client_secret";
        public const string DefaultPassword = "password";
        public const string AuthorityTestTenant = "https://" + ProductionPrefNetworkEnvironment + "/" + Utid + "/";
        public const string DiscoveryEndPoint = "discovery/instance";
        public const string DefaultAuthorizationCode = "DefaultAuthorizationCode";
        public const string DefaultAccessToken = "DefaultAccessToken";
        public const string DefaultClientAssertion = "DefaultClientAssertion";
        public const string RawClientId = "eyJ1aWQiOiJteS11aWQiLCJ1dGlkIjoibXktdXRpZCJ9";
        public const string XClientSku = "x-client-SKU";
        public const string XClientVer = "x-client-Ver";
        public const TokenSubjectType TokenSubjectTypeUser = 0;

        public const string LocalAccountId = "test_local_account_id";
        public const string GivenName = "Joe";
        public const string FamilyName = "Doe";
        public const string Username = "joe@localhost.com";

        public static readonly IDictionary<string, string> ExtraQueryParams
            = new Dictionary<string, string>()
            {
                {"extra", "qp" },
                {"key1", "value1%20with%20encoded%20space"},
                {"key2", "value2"}
            };

        public enum AuthorityType { B2C };
        public static string[] ProdEnvAliases = new string[] {
                                "login.microsoftonline.com",
                                "login.windows.net",
                                "login.microsoft.com",
                                "sts.windows.net"};

        public static readonly string UserIdentifier = CreateUserIdentifier();

        public static string GetDiscoveryEndpoint(string authority)
        {
            return authority + DiscoveryEndPoint;
        }

        public static string CreateUserIdentifier()
        {
            // return CreateUserIdentifier(Uid, Utid);
            return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", Uid, Utid);
        }

        public static string CreateUserIdentifier(string uid, string utid)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", uid, utid);
        }

        public static readonly Account User = new Account(UserIdentifier, DisplayableId, ProductionPrefNetworkEnvironment);

        public static readonly string OnPremiseAuthority = "https://fs.contoso.com/adfs/";
        public static readonly string OnPremiseClientId = "on_premise_client_id";
        public static readonly string OnPremiseUniqueId = "on_premise_unique_id";
        public static readonly string OnPremiseDisplayableId = "displayable@contoso.com";
        public static readonly string FabrikamDisplayableId = "displayable@fabrikam.com";
        public static readonly string OnPremiseHomeObjectId = OnPremiseUniqueId;
        public static readonly string OnPremisePolicy = "on_premise_policy";
        public static readonly string OnPremiseRedirectUri = "urn:ietf:wg:oauth:2.0:oob";
        public static readonly string OnPremiseClientSecret = "on_premise_client_secret";
        public static readonly string OnPremiseUid = "my-OnPremise-UID";
        public static readonly string OnPremiseUtid = "my-OnPremise-UTID";

        public static readonly Account OnPremiseUser = new Account(
            string.Format(CultureInfo.InvariantCulture, "{0}.{1}", OnPremiseUid, OnPremiseUtid), OnPremiseDisplayableId, null);

        public const string BrokerExtraQueryParameters = "extra=qp&key1=value1%20with%20encoded%20space&key2=value2";
        public const string BrokerClaims = "testClaims";

#if !ANDROID && !iOS && !WINDOWS_APP
        public static readonly ClientCredential OnPremiseCredentialWithSecret = new ClientCredential(ClientSecret);
        public static readonly ClientCredential CredentialWithSecret = new ClientCredential(ClientSecret);
#endif
    }
}