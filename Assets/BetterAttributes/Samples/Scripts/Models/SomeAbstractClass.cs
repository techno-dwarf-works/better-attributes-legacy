using System;
using UnityEngine;

namespace BetterAttributes.Samples.Models
{
    [Serializable]
    public abstract class SomeAbstractClass
    {
        [SerializeField] private protected int baseIntField;
    }

    [Serializable]
    public class SomeAbstractClassImplementation1 : SomeAbstractClass
    {
        [SerializeField] private float floatField;
    }
    
    [Serializable]
    public class SomeAbstractClassImplementation2 : SomeAbstractClass
    {
        [SerializeField] private bool boolField;
    }
}