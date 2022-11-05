using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.OtherModels
{
    [Serializable]
    public class SomeInterfaceImplementation16 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}