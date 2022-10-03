using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.Runtime.Tree;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropdownCollection : TreeNode<DropdownBase>
    {
        public void AddItem(string[] keys, bool state, Action<object> onSelect, object value)
        {
            var queue = new Queue<string>(keys);
            var firstKey = queue.Dequeue();
            var item = Children.FirstOrDefault(x => x.Value.Equals(firstKey));
            if (queue.Count > 0)
            {
                if (item == null)
                {
                    item = AddChild(new DropdownSubTree(new GUIContent(firstKey)));
                    Iterate(item, state, onSelect, value, queue);
                }
                else
                {
                    Iterate(item, state, onSelect, value, queue);
                }
            }
            else
            {
                AddLeaf(this, firstKey, state, onSelect, value);
            }
        }

        private void Iterate(TreeNode<DropdownBase> item, bool state, Action<object> onSelect, object value,
            Queue<string> queue)
        {
            var bufferItem = item;
            while (queue.Count > 0)
            {
                var bufferKey = queue.Dequeue();
                var first = bufferItem.Children.FirstOrDefault(x => x.Value.Equals(bufferKey));
                if (first == null)
                {
                    var node = queue.Count <= 0
                        ? AddLeaf(bufferItem, bufferKey, state, onSelect, value)
                        : AddBranch(bufferItem, bufferKey);

                    if (queue.Count > 0)
                    {
                        bufferItem = node;
                    }
                }
                else
                {
                    bufferItem = first;
                }
            }
        }

        private static TreeNode<DropdownBase> AddLeaf(TreeNode<DropdownBase> item, string bufferKey, bool state,
            Action<object> onSelect, object type)
        {
            return item.AddChild(new DropdownItem(new GUIContent(bufferKey), state, onSelect, type));
        }

        private static TreeNode<DropdownBase> AddBranch(TreeNode<DropdownBase> item, string bufferKey)
        {
            var treeNode = item.AddChild(new DropdownSubTree(new GUIContent(bufferKey)));
            return treeNode;
        }

        public DropdownCollection(DropdownBase value) : base(value)
        {
        }
    }
}