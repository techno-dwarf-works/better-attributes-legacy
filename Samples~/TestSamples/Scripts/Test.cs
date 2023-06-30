using System;
using System.Collections.Generic;
using Better.Attributes.Runtime;
using Better.Attributes.Runtime.DrawInspector;
using Better.Attributes.Runtime.Gizmo;
using Better.Attributes.Runtime.Headers;
using Better.Attributes.Runtime.Preview;
using Better.Attributes.Runtime.ReadOnly;
using Better.Attributes.Runtime.Rename;
using Better.Attributes.Runtime.Select;
using Better.Extensions.Runtime;
using Samples.Interfaces;
using Samples.Models;
using UnityEngine;

namespace Samples
{
    [Flags]
    public enum MyFlagEnum
    {
        First = 1,
        Second = 2,
        Third = 4
    }

    public class Test : MonoBehaviour
    {
        [IconHeader("78b772f3f305147419de3cc97ca72bd7")]
        [Select(typeof(ISomeInterface))]
        [SerializeField] private SerializedType serializedType;
        [Select] [SerializeField] private KeyCode keyCode;

        [Select] [SerializeField] private MyFlagEnum myFlagEnumTest;

        [Preview] [SerializeField] private PreviewTest component;
        [Preview] [SerializeField] private Texture2D texture;

        [DrawInspector] [SerializeField] private List<TestScriptableObject> scriptableObjectList;
        [DrawInspector] [SerializeField] private TestScriptableObject[] scriptableObjectArray;
        [DrawInspector] [SerializeField] private TestScriptableObject scriptableObject;

        [GizmoLocal] [SerializeField] private Vector3 vector3Local;

        [GizmoLocal] [RenameField("Quaternion Local Rename")] [SerializeField]
        private Quaternion quaternion;

        [GizmoLocal] [SerializeField] private SomeClass some;

        [ReadOnlyField] [SerializeField] private SomeClass someClass;

        [ReadOnlyField] [SerializeField] private float someFloat;

        [Select(DisplayGrouping.GroupedFlat)] [SerializeReference]
        private ISomeInterface someInterface;

        [Select] [SerializeReference]
        private SomeAbstractClass someAbstractClass;
        
        [Select(typeof(SomeAbstractClass), DisplayName.Full)] [SerializeReference]
        private List<SomeAbstractClass> someAbstractClasses;

        [Select(typeof(ISomeInterface), DisplayGrouping.Grouped)] [SerializeReference]
        private List<ISomeInterface> someInterfaces;

        [GizmoLocal] [SerializeField] private Bounds bounds;

        [Gizmo] [SerializeField] private List<Vector3> _vector3s;

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