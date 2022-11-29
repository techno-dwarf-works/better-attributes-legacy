using System;
using System.Collections.Generic;
using Better.Attributes.EditorAddons.Drawers.Base;
using Better.Attributes.EditorAddons.Drawers.Comparers;
using Better.Attributes.EditorAddons.Drawers.Preview;
using Better.Attributes.Runtime.Preview;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Utilities
{
    public class PreviewUtility : BaseUtility<PreviewUtility>
    {
        private protected override WrappersTypeCollection GenerateCollection()
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

        private protected override HashSet<Type> GenerateAvailable()
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