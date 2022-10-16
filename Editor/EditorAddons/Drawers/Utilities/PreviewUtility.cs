using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.EditorAddons.Drawers.Preview;
using BetterAttributes.Runtime.Attributes.Preview;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.Utilities
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