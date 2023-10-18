using Better.Attributes.Runtime.Gizmo;
using UnityEngine;

namespace Samples
{
    public class PreviewTest : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        [Gizmo] [SerializeField] private Vector3 pos;
        [Gizmo] [SerializeField] private Quaternion rot;
        
    }
}