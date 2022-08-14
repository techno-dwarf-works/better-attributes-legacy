using System;
using UnityEngine;

namespace BetterAttributes.Samples.Scripts.Models
{
    [Serializable]
    public class SomeAbstractClassImplementation1 : SomeAbstractClass
    {
        [SerializeField] private float floatField;
    }
}