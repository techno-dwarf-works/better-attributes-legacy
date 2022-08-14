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
using BetterAttributes.Runtime.EditorAddons.SelectAttributes;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.SelectDrawers
{
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : SelectDrawerBase<SelectImplementationAttribute>
    {
        private protected override void ValidateType(SerializedProperty property, int selectedTypeIndex, Type currentObjectType)
        {
            if (selectedTypeIndex < 0 ||
                selectedTypeIndex >= _reflectionType.Count ||
                currentObjectType == _reflectionType[selectedTypeIndex])
                return;
            currentObjectType = _reflectionType[selectedTypeIndex];

            property.managedReferenceValue = currentObjectType == null
                                                 ? null
                                                 : Activator.CreateInstance(currentObjectType);
        }
    }
}
