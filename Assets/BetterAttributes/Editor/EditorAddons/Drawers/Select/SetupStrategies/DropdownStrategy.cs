using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Better.Attributes.EditorAddons.Drawers.Select.SetupStrategies.DropdownCollection;
using Better.Attributes.EditorAddons.Drawers.Utilities;
using Better.Attributes.EditorAddons.Extensions;
using Better.Attributes.Runtime.Select;
using Better.EditorTools.Comparers;
using Better.Tools.Runtime;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Select.SetupStrategies
{
    public class DropdownStrategy : SetupStrategy
    {
        private readonly DropdownAttribute _dropdownAttribute;
        private IDataCollection _data = new NoneCollection();

        public DropdownStrategy(FieldInfo fieldInfo, object propertyContainer, SelectAttributeBase selectAttributeBase) : base(fieldInfo, propertyContainer,
            selectAttributeBase)
        {
            _dropdownAttribute = (DropdownAttribute)selectAttributeBase;
        }

        private bool TryGetType(IEnumerable<string> path, out Type type)
        {
            var buffer = new Stack<string>(path);
            var typeString = buffer.Aggregate(buffer.Pop(), (input, item) => input + AttributesDefinitions.Dot + item);

            type = Type.GetType(typeString, false, true);
            if (type != null) return true;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var bufferType = assembly.GetTypes().FirstOrDefault(t => t.Name == typeString);
                if (bufferType != null)
                {
                    type = bufferType;
                    return true;
                }
            }

            return type != null;
        }

        public override List<object> Setup()
        {
            var memberName = _dropdownAttribute.SelectorName;
            var type = _propertyContainer.GetType();
            var instance = _propertyContainer;
            if (memberName.StartsWith(AttributesDefinitions.SelectorDefinition))
            {
                memberName = memberName.Replace(AttributesDefinitions.SelectorDefinition, string.Empty);
                instance = GetItemsFrom(ref memberName, out type);
            }

            var response = FetchResponse(type, memberName, instance);

            switch (response)
            {
                case null:
                    return new List<object>();
                case IDictionary dictionary:
                    _data = new DictionaryCollection(dictionary, _dropdownAttribute.ShowDefault, _dropdownAttribute.ShowUniqueKey);
                    break;
                case IList list:
                    _data = new ListCollection(list, _dropdownAttribute.ShowDefault, _dropdownAttribute.ShowUniqueKey);
                    break;
            }

            return _data.GetValues();
        }

        private object FetchResponse(Type type, string memberName, object instance)
        {
            var member = type.GetMember(memberName, BetterEditorDefines.MethodFlags);
            object response = null;
            foreach (var memberInfo in member)
            {
                switch (memberInfo)
                {
                    case PropertyInfo propertyInfo:
                        response = propertyInfo.GetValue(instance);
                        break;
                    case FieldInfo fieldInfo:
                        response = fieldInfo.GetValue(instance);
                        break;
                    case MethodInfo methodInfo:
                        response = methodInfo.Invoke(instance, Array.Empty<object>());
                        break;
                }

                if (response != null)
                {
                    break;
                }
            }

            return response;
        }

        private object GetItemsFrom(ref string memberName, out Type type)
        {
            object instance = null;
            var path = new Stack<string>(memberName.Split(AttributesDefinitions.NameSeparator));
            memberName = path.Pop().Replace(AttributesDefinitions.Brackets, string.Empty);
            if (!TryGetType(path, out type))
            {
                var instanceName = path.Pop().Replace(AttributesDefinitions.Brackets, string.Empty);

                if (TryGetType(path, out type))
                {
                    const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
                    var members = type.GetMember(instanceName, bindingFlags);
                    object bufferInstance = null;
                    foreach (var memberInfo in members)
                    {
                        switch (memberInfo)
                        {
                            case PropertyInfo propertyInfo:
                                if (propertyInfo.GetGetMethod().IsStatic)
                                {
                                    bufferInstance = propertyInfo.GetValue(null);
                                }

                                break;
                            case FieldInfo fieldInfo:
                                if (fieldInfo.IsStatic)
                                {
                                    bufferInstance = fieldInfo.GetValue(null);
                                }

                                break;
                            case MethodInfo methodInfo:
                                if (methodInfo.IsStatic && methodInfo.GetParameters().Length <= 0)
                                {
                                    bufferInstance = methodInfo.Invoke(null, Array.Empty<object>());
                                }

                                break;
                        }

                        if (bufferInstance != null)
                        {
                            instance = bufferInstance;
                            break;
                        }
                    }
                }
            }

            if (type == null)
            {
                throw new TypeAccessException();
            }

            return instance;
        }

        public override GUIContent ResolveName(object value, DisplayName displayName)
        {
            if (value == null)
            {
                return new GUIContent(SelectUtility.Null);
            }

            var type = _data.FindName(value);
            switch (displayName)
            {
                case DisplayName.Short:
                    var split = type.Split(AttributesDefinitions.NameSeparator);
                    return split.Length <= 1 ? new GUIContent(type) : new GUIContent(split.Last());
                case DisplayName.Full:
                    return new GUIContent(type);
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayName), displayName, null);
            }
        }

        public override GUIContent[] ResolveGroupedName(object value, DisplayGrouping grouping)
        {
            if (value == null)
            {
                return new GUIContent[] { new GUIContent(SelectUtility.Null) };
            }

            var type = _data.FindName(value);

            var split = type.Split(AttributesDefinitions.NameSeparator);
            if (split.Length <= 1)
            {
                return new GUIContent[] { new GUIContent(type) };
            }

            switch (grouping)
            {
                case DisplayGrouping.Grouped:
                    return split.Select(x => new GUIContent(x)).ToArray();
                case DisplayGrouping.GroupedFlat:
                    return new GUIContent[] { new GUIContent(split.First()), new GUIContent(split.Last()) };
                default:
                    throw new ArgumentOutOfRangeException(nameof(grouping), grouping, null);
            }
        }

        public override string GetButtonName(object currentValue)
        {
            return _data.FindName(currentValue);
        }

        public override bool ResolveState(object currentValue, object iteratedValue)
        {
            return Equals(currentValue, iteratedValue);
        }

        public override bool Validate(object item)
        {
            var type = GetFieldOrElementType();

            if (item == null)
            {
                return true;
            }

            return TypeComparer.Instance.Equals(type, item.GetType());
        }

        public override bool CheckSupported()
        {
            return true;
        }

        public override GUIContent GenerateHeader()
        {
            return new GUIContent("Select");
        }
    }
}