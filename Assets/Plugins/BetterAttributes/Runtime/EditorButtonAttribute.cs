#region license

// Copyright 2021 - 2022 Arcueid Elizabeth D'athemon
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//     http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics;

namespace BetterAttributes.Runtime
{
    /// <summary>
    /// Displays Button in Inspector
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class EditorButtonAttribute : Attribute
    {
        private readonly string _displayName;

        public EditorButtonAttribute()
        {
            _displayName = string.Empty;
        }

        public EditorButtonAttribute(string displayName)
        {
            _displayName = displayName;
        }

        /// <summary>
        /// Provides Editor button
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="invokeParams"></param>
        public EditorButtonAttribute(string displayName, params object[] invokeParams) : this(displayName, -1, -1,
            invokeParams)
        {
        }

        /// <summary>
        /// Provides Editor button
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="captureGroup"></param>
        /// <param name="priority"></param>
        /// <param name="invokeParams"></param>
        public EditorButtonAttribute(string displayName, int captureGroup, int priority, params object[] invokeParams) :
            this(displayName)
        {
            InvokeParams = invokeParams;
            Priority = priority;
            CaptureGroup = captureGroup;
        }

        /// <summary>
        /// Provides Editor button
        /// </summary>
        /// <param name="invokeParams"></param>
        public EditorButtonAttribute(params object[] invokeParams) : this(string.Empty, -1, -1, invokeParams)
        {
        }

        /// <summary>
        /// Provides Editor button
        /// </summary>
        /// <param name="captureGroup"></param>
        /// <param name="invokeParams"></param>
        public EditorButtonAttribute(int captureGroup, params object[] invokeParams) : this(string.Empty, captureGroup,
            -1, invokeParams)
        {
        }

        /// <summary>
        /// Provides Editor button
        /// </summary>
        /// <param name="displayName"></param>
        /// <param name="captureGroup"></param>
        /// <param name="invokeParams"></param>
        public EditorButtonAttribute(string displayName, int captureGroup, params object[] invokeParams) : this(
            displayName, captureGroup, -1,
            invokeParams)
        {
        }

        /// <summary>
        /// Provides Editor button
        /// </summary>
        /// <param name="captureGroup"></param>
        /// <param name="priority"></param>
        /// <param name="invokeParams"></param>
        public EditorButtonAttribute(int captureGroup, int priority, params object[] invokeParams) : this(string.Empty,
            captureGroup, priority,
            invokeParams)
        {
        }
        
        public object[] InvokeParams { get; }

        public int Priority { get; }

        public int CaptureGroup { get; }

        private bool IsValidName()
        {
            return !string.IsNullOrWhiteSpace(_displayName) && !string.IsNullOrWhiteSpace(_displayName);
        }

        public string GetDisplayName(string name)
        {
            return IsValidName() ? _displayName : name;
        }
    }
}