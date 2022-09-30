using System;
using System.Collections.Generic;
using BetterAttributes.Runtime.Tree;
using UnityEngine;

namespace BetterAttributes.EditorAddons.Helpers
{
    public class DropDownSubTree : DropDownBase, INodeValue<DropDownBase>
    {
        private TreeNode<DropDownBase> _node;

        public DropDownSubTree(GUIContent content) : base(content)
        {
        }

        public void SetNode(TreeNode<DropDownBase> node)
        {
            _node = node;
        }

        internal override bool Invoke(DropDownPopup downPopup)
        {
            downPopup.SetCurrentDrawItems(_node);
            return false;
        }

        public override bool Contains(string searchText, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return Content.text.Contains(searchText, comparison);
        }
    }
}