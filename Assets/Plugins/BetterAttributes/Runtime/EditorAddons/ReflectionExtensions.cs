using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BetterAttributes.Runtime.EditorAddons
{
    public static class ReflectionExtensions
    {
#if UNITY_EDITOR
        public const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic |
                                          BindingFlags.Static | BindingFlags.Instance |
                                          BindingFlags.DeclaredOnly;

        public static string PrettyMemberName(this MemberInfo input)
        {
            return input.Name.PrettyCamelCase();
        }

        public static Dictionary<int, IEnumerable<KeyValuePair<MethodInfo, EditorButtonAttribute>>>
            GetSortedMethodAttributes(this Type type)
        {
            var methodButtonsAttributes =
                new Dictionary<int, IEnumerable<KeyValuePair<MethodInfo, EditorButtonAttribute>>>();

            foreach (var pair in type.GetMethodsAttributes<EditorButtonAttribute>())
            {
                foreach (var attribute in pair.Value)
                {
                    if (methodButtonsAttributes.ContainsKey(attribute.CaptureGroup))
                    {
                        var list = methodButtonsAttributes[attribute.CaptureGroup];
                        list = list.Append(new KeyValuePair<MethodInfo, EditorButtonAttribute>(pair.Key, attribute));
                        methodButtonsAttributes[attribute.CaptureGroup] = list.OrderBy(x => x.Value.Priority);
                    }
                    else
                    {
                        methodButtonsAttributes.Add(attribute.CaptureGroup,
                            new List<KeyValuePair<MethodInfo, EditorButtonAttribute>>
                            {
                                new KeyValuePair<MethodInfo, EditorButtonAttribute>(pair.Key, attribute)
                            });
                    }
                }
            }

            return methodButtonsAttributes.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        }

        public static IEnumerable<KeyValuePair<MethodInfo, IEnumerable<T>>> GetMethodsAttributes<T>(this Type t)
            where T : Attribute
        {
            return t == null
                ? Enumerable.Empty<KeyValuePair<MethodInfo, IEnumerable<T>>>()
                : t.GetMethods(Flags).Where(x => x.GetCustomAttributes<T>().Any())
                    .ToDictionary(key => key, value => value.GetCustomAttributes<T>(true))
                    .Concat(GetMethodsAttributes<T>(t.BaseType));
        }
#endif
    }
}