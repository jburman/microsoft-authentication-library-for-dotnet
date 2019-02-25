﻿// ------------------------------------------------------------------------------
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

namespace Microsoft.Identity.Client.Platforms.Android
{
    internal static class MsalErrorAndroidEx
    {
        public const string MissingPackagePermission = "missing_package_permission";
        public const string CannotSwitchToBrokerFromThisApp = "cannot_switch_to_broker_from_this_app";
        public const string IncorrectBrokerAccountType = "incorrect_broker_account_type";
        public const string IncorrectBrokerAppSignature = "incorrect_broker_app_signature";
        public const string FailedToGetBrokerAppSignature = "failed_to_get_broker_app_signature";
        public const string MissingBrokerRelatedPackage = "missing_broker_related_package";
        public const string MissingDigestShaAlgorithm = "missing_digest_sha_algorithm";
        public const string SignatureVerificationFailed = "signature_verification_failed";
        public const string NoBrokerAccountFound = "broker_account_not_found";
        public const string BrokerApplicationRequired = "broker_application_required";
        public const string IncorrectBrokerRedirectUri = "incorrect_broker_redirecturi";
        public const string CallingOnMainThread = "calling_getBrokerUsers_on_main_thread";
    }
}