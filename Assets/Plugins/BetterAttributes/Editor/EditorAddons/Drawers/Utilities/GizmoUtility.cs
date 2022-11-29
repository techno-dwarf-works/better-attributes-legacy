using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.EditorAddons.Drawers.Gizmo;
using Better.Attributes.Runtime.Gizmo;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class GizmoUtility : BaseUtility<GizmoUtility>
    {
        private readonly Dictionary<Type, int> hideTransformRegistered = new Dictionary<Type, int>();

        [DidReloadScripts]
        private static void OnReloadScripts()
        {
            Instance.hideTransformRegistered.Clear();
        }

        public bool IsButtonDrawn(Type type)
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

        public void RemoveButtonDrawn(Type type)
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

        private protected override WrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection()
            {
                {
                    typeof(GizmoLocalAttribute), new Dictionary<Type, Type>()
                    {
                        { typeof(Vector3), typeof(Vector3LocalWrapper) },
                        { typeof(Vector2), typeof(Vector2LocalWrapper) },
                        { typeof(Quaternion), typeof(QuaternionLocalWrapper) },
                        { typeof(Bounds), typeof(BoundsLocalWrapper) }
                    }
                },
                {
                    typeof(GizmoAttribute), new Dictionary<Type, Type>()
                    {
                        { typeof(Vector3), typeof(Vector3Wrapper) },
                        { typeof(Vector2), typeof(Vector2Wrapper) },
                        { typeof(Quaternion), typeof(QuaternionWrapper) },
                        { typeof(Bounds), typeof(BoundsWrapper) }
                    }
                }
            };
        }

        private protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>()
            {
                typeof(Vector3),
                typeof(Vector2),
                typeof(Quaternion),
                typeof(Bounds)
            };
        }
    }
}