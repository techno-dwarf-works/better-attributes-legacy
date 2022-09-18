using System;
using BetterAttributes.Runtime.Attributes.Gizmo;
using UnityEngine;

namespace BetterAttributes.Samples.Models
{
    [Serializable]
    public class SomeAbstractClassImplementation1 : SomeAbstractClass
    {
        [SerializeField] private float floatField;
        [Gizmo][SerializeField] private Vector3 vector3;
    }
}