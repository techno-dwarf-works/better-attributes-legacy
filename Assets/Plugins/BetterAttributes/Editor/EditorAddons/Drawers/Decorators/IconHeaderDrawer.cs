using BetterAttributes.Runtime.Attributes.Headers;
using UnityEditor;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Decorators
{
    [CustomPropertyDrawer(typeof(IconHeaderAttribute))]
    internal sealed class IconHeaderDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;
            position = EditorGUI.IndentedRect(position);
            if (attribute is IconHeaderAttribute iconHeaderAttribute)
            {
                Texture loadedTexture = null;
                if (iconHeaderAttribute.UseResources)
                {
                    loadedTexture = Resources.Load<Texture>(iconHeaderAttribute.Path);
                }
                else
                {
                    loadedTexture = AssetDatabase.LoadAssetAtPath<Texture>(iconHeaderAttribute.Path);
                }

                GUI.Label(position, iconHeaderAttribute, EditorStyles.boldLabel);
            }
        }

        public override float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight * 1.5f;
        }
    }
}