using System;
using Samples.Interfaces;
using UnityEngine;

namespace Samples.OtherModels
{
    [Serializable]
    public class SomeInterfaceImplementation1 : ISomeInterface
    {
        [SerializeField] private int intField;
    }
}