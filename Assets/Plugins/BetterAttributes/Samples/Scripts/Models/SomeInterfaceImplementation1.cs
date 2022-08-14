using System;
using BetterAttributes.Samples.Scripts.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples.Scripts.Models
{
    [Serializable]
    public class SomeInterfaceImplementation1 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}