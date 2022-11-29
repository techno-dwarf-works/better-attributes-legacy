using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Better.Attributes.Runtime.Tree
{
    public interface INodeValue<T>
    {
        public void SetNode(TreeNode<T> node);
    }
    
    public class TreeNode<T>
    {
        private protected readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            _value = value;
        }

        public TreeNode<T> this[int i] => _children[i];

        public TreeNode<T> Parent { get; private set; }

        public T Value => _value;

        public ReadOnlyCollection<TreeNode<T>> Children => _children.AsReadOnly();

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this };
            if (value is INodeValue<T> nodeValue)
            {
                nodeValue.SetNode(node);
            }

            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}