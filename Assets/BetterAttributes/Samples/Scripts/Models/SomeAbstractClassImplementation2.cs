using System;
using UnityEngine;

namespace BetterAttributes.Samples.Models
{
    [Serializable]
    public class SomeAbstractClassImplementation2 : SomeAbstractClass
    {
        [SerializeField] private bool boolField;
    }
}