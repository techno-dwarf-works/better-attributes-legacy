using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples.OtherModels
{
    [Serializable]
    public class SomeInterfaceImplementation15 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}