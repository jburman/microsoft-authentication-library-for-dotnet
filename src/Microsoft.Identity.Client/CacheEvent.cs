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


namespace Microsoft.Identity.Client
{
    internal class CacheEvent : Event
    {
        public const string TokenCacheLookup = "token_cache_lookup";
        public const string TokenCacheWrite = "token_cache_write";
        public const string TokenCacheBeforeAccess = "token_cache_before_access";
        public const string TokenCacheAfterAccess = "token_cache_after_access";
        public const string TokenCacheBeforeWrite = "token_cache_before_write";
        public const string TokenCacheDelete = "token_cache_delete";

        public const string TokenType = "token_type";
        public const string TokenTypeAT = "AT";
        public const string TokenTypeRT = "RT";

        public CacheEvent(string eventName) : base(eventName)  // Ideally we would like to check the eventName is one of the predefined const strings
        {
        }

        public void Update(string tokenType)  // Ideally we would like to check the eventName is one of the predefined const strings
        {
            this[TokenType] = tokenType;
        }
    }
}
