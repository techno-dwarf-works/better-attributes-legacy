using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples
{
    [Serializable]
    public class SomeInterfaceImplementation14 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}