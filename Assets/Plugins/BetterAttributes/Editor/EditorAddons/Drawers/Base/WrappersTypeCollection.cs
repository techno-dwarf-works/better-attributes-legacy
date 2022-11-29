using System;
using System.Collections.Generic;

namespace Better.Attributes.EditorAddons.Drawers.Base
{
    public class WrappersTypeCollection : Dictionary<Type, Dictionary<Type, Type>>
    {
        public WrappersTypeCollection() : base()
        {
        }

        public WrappersTypeCollection(IEqualityComparer<Type> equalityComparer) : base(equalityComparer)
        {
        }
    }
}