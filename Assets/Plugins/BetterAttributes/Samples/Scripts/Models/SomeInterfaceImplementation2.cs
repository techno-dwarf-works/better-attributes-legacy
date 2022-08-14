using System;
using BetterAttributes.Samples.Scripts.Interfaces;
using UnityEngine;

namespace BetterAttributes.Samples.Scripts.Models
{
    [Serializable]
    public class SomeInterfaceImplementation2 : ISomeInterface
    {
        [SerializeField] private bool boolField;
    }
}