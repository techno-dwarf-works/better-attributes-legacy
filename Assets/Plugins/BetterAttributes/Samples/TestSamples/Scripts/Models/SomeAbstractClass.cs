using System;
using UnityEngine;

namespace Samples.Models
{
    [Serializable]
    public abstract class SomeAbstractClass
    {
        [SerializeField] private protected int baseIntField;
    }
}