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
        private class AssignableFromComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type x, Type y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                return x.IsAssignableFrom(y) || x == y;
            }

            public int GetHashCode(Type obj)
            {
                return 0;
            }
        }
        
        private protected override WrappersTypeCollection GenerateCollection()
        {
            return new WrappersTypeCollection()
            {
                {
                    typeof(PreviewAttribute), new Dictionary<Type, Type>(new AssignableFromComparer())
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
            return new HashSet<Type>(new AssignableFromComparer())
            {
                typeof(Sprite),
                typeof(Texture),
                typeof(Component)
            };
        }
    }
}