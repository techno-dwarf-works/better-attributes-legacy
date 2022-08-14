using System;
using UnityEngine;

namespace BetterAttributes.Samples.Scripts.Models
{
    [Serializable]
    public abstract class SomeAbstractClass
    {
        [SerializeField] private protected int baseIntField;
    }
}