using System.Collections;
using System.Collections.Generic;
using BetterAttributes.Runtime;
using BetterAttributes.Runtime.Attributes.Select;
using BetterAttributes.Samples.Interfaces;
using UnityEngine;

public class RemovableTest : MonoBehaviour
{
    [SelectImplementation] [SerializeReference]
    private ISomeInterface someInterface;

    [EditorButton]
    public void Test()
    {
        
    }
}
