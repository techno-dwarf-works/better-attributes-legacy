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
using UnityEngine;

namespace BetterAttributes.Runtime.SelectAttributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class SelectAttributeBase : PropertyAttribute
    {
        private readonly Type _type;
        public bool FindTypesRecursively { get; }

        public SelectAttributeBase(Type type, bool findTypesRecursively = false)
        {
            _type = type;
            FindTypesRecursively = findTypesRecursively;
        }

        public SelectAttributeBase(bool useTypeExplicitly = false)
        {
            FindTypesRecursively = useTypeExplicitly;
        }

        public Type GetFieldType()
        {
            return _type;
        }

        public SelectAttributeBase()
        {
        }
    }
}
