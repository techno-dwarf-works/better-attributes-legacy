using System.Collections.Generic;
using BetterAttributes.Plugin.EditorAddons;
using BetterAttributes.Plugin.EditorAddons.GizmoAttributes;
using BetterAttributes.Plugin.EditorAddons.ReadOnlyAttributes;
using BetterAttributes.Plugin.EditorAddons.SelectAttributes;
using BetterAttributes.Samples.Interfaces;
using BetterAttributes.Samples.Models;
using UnityEngine;

namespace BetterAttributes.Samples
{
    public class Test : MonoBehaviour
    {
        [GizmoLocal]
        [SerializeField] private Bounds bounds;
        
        [GizmoLocal]
        [SerializeField] private Vector3 vector3Local;
        
        [GizmoLocal]
        [SerializeField] private Quaternion quaternion;
        
        [ReadOnlyField] [SerializeField] private SomeClass someClass;

        [ReadOnlyField] [SerializeField] private float someFloat;

        [SelectImplementation] [SerializeReference]
        private ISomeInterface someInterface;

        [SelectImplementation] [SerializeReference]
        private SomeAbstractClass someAbstractClass;

        [SelectImplementation] [SerializeReference]
        private List<SomeAbstractClass> someAbstractClasses;

        [SelectImplementation(typeof(ISomeInterface))] [SerializeReference]
        private List<ISomeInterface> someInterfaces;

        ///Default usage of attribute.
        [EditorButton]
        private void SomeMethod()
        {
            //Some code.
        }

        ///This button will call method with predefined parameters. 
        ///When invokeParams not specified will call with null.
        [EditorButton(invokeParams: 10f)]
        private void SomeMethod(float floatValue)
        {
            Debug.Log($"{nameof(SomeMethod)}({floatValue})");
        }

        ///This button will call method with predefined parameters. 
        ///When invokeParams not specified will call with null.
        [EditorButton(invokeParams: new object[] { 10f, 10 })]
        private void SomeMethod(float floatValue, int intValue)
        {
            Debug.Log($"{nameof(SomeMethod)}({floatValue}, {intValue})");
        }

        /// This button will be in the same row with button for SomeMethod2.
        /// But will be in the second position.
        /// When captureGroup not specified each button placed in separate row.
        /// When priority not specified buttons in one row sorted by order in code.
        [EditorButton(captureGroup: 1, priority: 2)]
        private void SomeMethod1()
        {
            Debug.Log($"{nameof(SomeMethod1)}");
        }

        [EditorButton(captureGroup: 1, priority: 1)]
        private void SomeMethod2()
        {
            Debug.Log($"{nameof(SomeMethod2)}");
        }

        /// This button will have name "Some Cool Button".
        /// When displayName not specified or null/empty/whitespace button 
        /// will have name same as method.
        [EditorButton(displayName: "Some Cool Button")]
        private void SomeMethod3()
        {
            Debug.Log($"{nameof(SomeMethod3)}");
        }
    }
}