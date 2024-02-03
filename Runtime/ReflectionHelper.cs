using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Better.Attributes.Runtime
{
    public static class ReflectionHelper
    {
        public static IEnumerable<MemberInfo> GetAllMembers(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

            HashSet<MemberInfo> members = new HashSet<MemberInfo>(new MemberInfoComparer());

            do
            {
                // If the type is a constructed generic type, get the members of the generic type definition
                Type typeToReflect = type.IsGenericType && !type.IsGenericTypeDefinition ? type.GetGenericTypeDefinition() : type;

                foreach (var member in typeToReflect.GetMembers(bindingFlags))
                {
                    // For generic classes, convert members back to the constructed type
                    MemberInfo memberToAdd = type.IsGenericType && !type.IsGenericTypeDefinition
                        ? ConvertToConstructedGenericType(member, type)
                        : member;

                    if(memberToAdd != null)
                    {
                        members.Add(memberToAdd);
                    }
                }

                type = type.BaseType;
            } while (type != null); // Continue until you reach the top of the inheritance hierarchy

            return members;
        }

        private static MemberInfo ConvertToConstructedGenericType(MemberInfo memberInfo, Type constructedType)
        {
            // Ensure the member's declaring type is a generic type definition
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsGenericTypeDefinition)
            {
                var members = constructedType.GetMember(memberInfo.Name);
                return members.FirstOrDefault();
            }

            // Return the original memberInfo if it's not a property of a generic type definition or doesn't need to be constructed
            return memberInfo;
        }

        public static MemberInfo GetMemberByName(Type type, string memberName)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (string.IsNullOrEmpty(memberName))
            {
                throw new ArgumentException("Member name cannot be null or empty.", nameof(memberName));
            }

            var allMembers = GetAllMembers(type);

            // Use LINQ to find the member by name. This assumes you want the first match if there are multiple members with the same name (overloads).
            // If you expect overloads and want to handle them differently, you might need a more complex approach.
            return allMembers.FirstOrDefault(m => m.Name == memberName);
        }

        // Custom comparer for MemberInfo to handle equality correctly, especially for members from base classes
        private class MemberInfoComparer : IEqualityComparer<MemberInfo>
        {
            public bool Equals(MemberInfo x, MemberInfo y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;
                return x.Name == y.Name && x.DeclaringType == y.DeclaringType;
            }

            public int GetHashCode(MemberInfo obj)
            {
                unchecked
                {
                    var hashCode = obj.Name.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.DeclaringType.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}