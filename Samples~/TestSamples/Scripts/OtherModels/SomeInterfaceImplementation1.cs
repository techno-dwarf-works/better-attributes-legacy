using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.OtherModels
{
    [Serializable]
    public class SomeInterfaceImplementation1 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}