using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Comparers;
using BetterAttributes.EditorAddons.Drawers.Utilities;
using UnityEditor;

namespace BetterAttributes.EditorAddons.Drawers.Base
{
    public class WrapperCollection<T> : Dictionary<SerializedProperty, WrapperCollectionValue<T>>
        where T : UtilityWrapper
    {
        public WrapperCollection() : base(SerializedPropertyComparer.Instance)
        {
        }

        /// <summary>
        /// Deconstruct method for stored wrappers
        /// </summary>
        public void Deconstruct()
        {
            foreach (var gizmo in Values)
            {
                gizmo.Wrapper.Deconstruct();
            }
        }
    }
}