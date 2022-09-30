using System;
using System.Collections.Generic;
using System.Linq;
using BetterAttributes.Runtime.Tree;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropDownCollection : TreeNode<DropDownBase>
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
                    item = AddChild(new DropDownSubTree(new GUIContent(firstKey)));
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

        private void Iterate(TreeNode<DropDownBase> item, bool state, Action<object> onSelect, object value,
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

        private static TreeNode<DropDownBase> AddLeaf(TreeNode<DropDownBase> item, string bufferKey, bool state,
            Action<object> onSelect, object type)
        {
            return item.AddChild(new DropDownItem(new GUIContent(bufferKey), state, onSelect, type));
        }

        private static TreeNode<DropDownBase> AddBranch(TreeNode<DropDownBase> item, string bufferKey)
        {
            var treeNode = item.AddChild(new DropDownSubTree(new GUIContent(bufferKey)));
            return treeNode;
        }

        public DropDownCollection(DropDownBase value) : base(value)
        {
        }
    }
}