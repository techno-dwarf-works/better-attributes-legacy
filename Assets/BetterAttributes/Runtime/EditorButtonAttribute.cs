using System;
using System.Diagnostics;
using Better.Tools.Runtime;

namespace Better.Attributes.Runtime
{
    /// <summary>
    /// Displays Button in Inspector
    /// </summary>
    [Conditional(BetterEditorDefines.Editor)]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
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