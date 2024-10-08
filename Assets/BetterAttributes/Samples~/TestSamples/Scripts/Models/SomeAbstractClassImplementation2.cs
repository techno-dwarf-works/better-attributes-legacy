using System;
using UnityEngine;

namespace Samples.Models
{
    [Serializable]
    public class SomeAbstractClassImplementation2 : SomeAbstractClass
    {
        [SerializeField] private bool boolField;
        
        [SerializeField] private protected int baseIntField;

        public int BaseIntField => baseIntField;
    }
    
    [Serializable]
    public class SomeAbstractClassImplementation3 : SomeAbstractClass
    {
    }
}