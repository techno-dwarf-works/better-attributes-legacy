using System;
using System.Collections.Generic;
using BetterAttributes.EditorAddons.Drawers.Base;
using BetterAttributes.Runtime.PreviewAttributes;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Drawers.PreviewDrawers
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
                    typeof(PopupPreviewAttribute), new Dictionary<Type, Type>(new AssignableFromComparer())
                    {
                        { typeof(Sprite), typeof(SpritePopupWrapper) },
                        { typeof(Texture), typeof(TexturePopupWrapper) },
                        { typeof(Component), typeof(AssetPopupWrapper) }
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