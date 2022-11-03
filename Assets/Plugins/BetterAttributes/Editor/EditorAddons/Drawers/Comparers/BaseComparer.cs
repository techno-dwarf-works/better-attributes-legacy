using System.Collections.Generic;

namespace BetterAttributes.EditorAddons.Drawers.Comparers
{
    public abstract class BaseComparer<T, U> where T : IEqualityComparer<U>, new()
    {
        public static T Instance { get; } = new T();
    }
}