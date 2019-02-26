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

using System;
using System.Globalization;

namespace Microsoft.Identity.Client
{
    internal static class LogMessages
    {
        public const string BeginningAcquireByRefreshToken = "Begin acquire token by refresh token...";
        public const string NoScopesProvidedForRefreshTokenRequest = "No scopes provided for acquire token by refresh token request. Using default scope instead.";

        public static string UsingXScopesForRefreshTokenRequest(int numScopes)
        {
            return string.Format(CultureInfo.InvariantCulture, "Using {0} scopes for acquire token by refresh token request", numScopes);
        }

        public const string CustomWebUiAcquiringAuthorizationCode = "Using CustomWebUi to acquire the authorization code";
        public const string CustomWebUiRedirectUriMatched = "Redirect Uri was matched.  Returning success from CustomWebUiHandler.";
        public const string CustomWebUiOperationCancelled = "CustomWebUi AcquireAuthorizationCode was canceled";

        public static string CustomWebUiCallingAcquireAuthorizationCodePii(Uri authorizationUri, Uri redirectUri)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "calling CustomWebUi.AcquireAuthorizationCode authUri({0}) redirectUri({1})",
                authorizationUri,
                redirectUri);
        }

        public const string CustomWebUiCallingAcquireAuthorizationCodeNoPii = "Calling CustomWebUi.AcquireAuthorizationCode";

        public const string CheckMsalTokenResponseReturnedFromBroker = "Checking MsalTokenResponse returned from broker. ";
        public const string BrokerResponseContainsAccessToken = "Broker response contains access token. Access token count: ";
        public const string UnknownErrorReturnedInBrokerResponse = "Unknown error returned in broker response. ";
        public const string BrokerInvocationRequired = "Broker invocation required. Adding BrokerInstallUrl to broker payload. ";
        public const string CanInvokeBrokerAcquireTokenWithBroker = "Can invoke broker. Will attempt to acquire token with broker. ";
        public const string AuthenticationWithBrokerDidNotSucceed = "Broker authentication did not succeed, or the broker install failed. " +
            "See https://aka.ms/msal-net-brokers for more information. ";


        public static string ErrorReturnedInBrokerResponse(string error)
        {
            return string.Format(CultureInfo.InvariantCulture, "Error {0} returned in broker response. ", error);
        }
    }
}
