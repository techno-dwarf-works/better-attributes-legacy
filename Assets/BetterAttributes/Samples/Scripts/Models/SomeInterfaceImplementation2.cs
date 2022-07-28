using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples.Models
{
    [Serializable]
    public class SomeInterfaceImplementation2 : ISomeInterface
    {
        [SerializeField] private bool boolField;
    }
}