using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Preview;
using Better.Attributes.Runtime.Preview;
using Better.EditorTools.EditorAddons.Comparers;
using Better.EditorTools.EditorAddons.Utilities;
using Better.EditorTools.EditorAddons.WrappersTypeCollection;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class PreviewUtility : BaseUtility<PreviewUtility>
    {
        protected override BaseWrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection()
            {
                {
                    typeof(PreviewAttribute), new Dictionary<Type, Type>(AssignableFromComparer.Instance)
                    {
                        { typeof(Sprite), typeof(SpriteWrapper) },
                        { typeof(Texture2D), typeof(TextureWrapper) },
                        { typeof(Component), typeof(AssetWrapper) }
                    }
                }
            };
        }

        protected override HashSet<Type> GenerateAvailable()
        {
            return new HashSet<Type>(AssignableFromComparer.Instance)
            {
                typeof(Sprite),
                typeof(Texture),
                typeof(Component)
            };
        }
    }
}