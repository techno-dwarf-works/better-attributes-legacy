using System;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples.Models
{
    [Serializable]
    public class SomeInterfaceImplementation1 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}