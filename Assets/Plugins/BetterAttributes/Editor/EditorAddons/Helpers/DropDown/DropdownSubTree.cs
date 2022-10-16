using System;
using System.Collections.Generic;
using BetterAttributes.Runtime.Tree;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropdownSubTree : DropdownBase, INodeValue<DropdownBase>
    {
        private TreeNode<DropdownBase> _node;

        public DropdownSubTree(GUIContent content) : base(content)
        {
        }

        public void SetNode(TreeNode<DropdownBase> node)
        {
            _node = node;
        }

        internal override bool Invoke(DropdownWindow downPopup)
        {
            downPopup.SetCurrentDrawItems(_node);
            return false;
        }
    }
}