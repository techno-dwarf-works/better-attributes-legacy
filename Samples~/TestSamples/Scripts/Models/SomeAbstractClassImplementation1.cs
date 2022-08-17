using System;
using BetterAttributes.Runtime.EditorAddons.GizmoAttributes;
using UnityEngine;

namespace BetterAttributes.Samples.Scripts.Models
{
    [Serializable]
    public class SomeAbstractClassImplementation1 : SomeAbstractClass
    {
        [SerializeField] private float floatField;
        [Gizmo][SerializeField] private Vector3 vector3;
    }
}