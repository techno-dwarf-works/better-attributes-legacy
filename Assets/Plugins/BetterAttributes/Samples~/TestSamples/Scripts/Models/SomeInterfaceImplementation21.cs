using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples
{
    [Serializable]
    public class SomeInterfaceImplementation21 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}