using System;
using System.Collections.Generic;
using BetterAttributes.Drawers.GizmoDrawers.LocalWrappers;
using BetterAttributes.Drawers.GizmoDrawers.Wrappers;
using BetterAttributes.EditorAddons.GizmoAttributes;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.Drawers.GizmoDrawers
{
    public static class GizmoDrawerUtility
    {
        private static Dictionary<SerializedPropertyType, Type> _gizmoWrappers =
            new Dictionary<SerializedPropertyType, Type>()
            {
                { SerializedPropertyType.Vector3, typeof(Vector3Wrapper) },
                { SerializedPropertyType.Vector2, typeof(Vector2Wrapper) },
                { SerializedPropertyType.Quaternion, typeof(QuaternionWrapper) },
                { SerializedPropertyType.Bounds, typeof(BoundsWrapper) }
            };
        
        private static Dictionary<SerializedPropertyType, Type> _localGizmoWrappers =
            new Dictionary<SerializedPropertyType, Type>()
            {
                { SerializedPropertyType.Vector3, typeof(Vector3LocalWrapper) },
                { SerializedPropertyType.Vector2, typeof(Vector2LocalWrapper) },
                { SerializedPropertyType.Quaternion, typeof(QuaternionLocalWrapper) },
                { SerializedPropertyType.Bounds, typeof(BoundsLocalWrapper) }
            };

        private static Dictionary<SerializedPropertyType, Type> GetWrapperDictionary(Type gizmoType)
        {
            Dictionary<SerializedPropertyType, Type> gizmoDictionary = null;
            if (gizmoType == typeof(GizmoAttribute))
            {
                gizmoDictionary = _gizmoWrappers;
            }else if (gizmoType == typeof(GizmoLocalAttribute))
            {
                gizmoDictionary = _localGizmoWrappers;
            }
            else
            {
                throw new KeyNotFoundException($"Gizmo supported types not found for {gizmoType}");
            }

            return gizmoDictionary;
        }
        
        public static GizmoWrapper GetWrapper(SerializedPropertyType type, Type gizmoType)
        {
            Type wrapperType;
            if (ValidType(type))
            {
                var gizmoWrappers = GetWrapperDictionary(gizmoType);
                wrapperType = gizmoWrappers[type];
            }
            else
            {
                return null;
            }

            return (GizmoWrapper)Activator.CreateInstance(wrapperType);
        }

        public static bool ValidType(SerializedPropertyType fieldType)
        {
            return _gizmoWrappers.ContainsKey(fieldType);
        }
    }
}