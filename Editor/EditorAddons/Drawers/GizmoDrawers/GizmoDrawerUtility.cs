using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.GizmoDrawers.LocalWrappers;
using BetterAttributes.EditorAddons.Drawers.GizmoDrawers.Wrappers;
using BetterAttributes.Runtime.EditorAddons.GizmoAttributes;
using UnityEditor;
using UnityEditor.Callbacks;

namespace BetterAttributes.EditorAddons.Drawers.GizmoDrawers
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

        private static readonly Dictionary<Type, int> hideTransformRegistered = new Dictionary<Type, int>();


        [DidReloadScripts]
        private static void OnReloadScripts()
        {
            hideTransformRegistered.Clear();
        }
        
        public static bool IsButtonDrawn(Type type)
        {
            if (hideTransformRegistered.TryGetValue(type, out var count))
            {
                count++;
                hideTransformRegistered[type] = count;
                return true;
            }

            hideTransformRegistered.Add(type, 1);
            return false;
        }

        public static void RemoveButtonDrawn(Type type)
        {
            if (hideTransformRegistered.TryGetValue(type, out var count))
            {
                count--;
                if (count <= 1)
                {
                    hideTransformRegistered.Remove(type);
                    return;
                }

                hideTransformRegistered[type] = count;
            }
        }

        private static Dictionary<SerializedPropertyType, Type> GetWrapperDictionary(Type gizmoType)
        {
            Dictionary<SerializedPropertyType, Type> gizmoDictionary;
            if (gizmoType == typeof(GizmoAttribute))
            {
                gizmoDictionary = _gizmoWrappers;
            }
            else if (gizmoType == typeof(GizmoLocalAttribute))
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