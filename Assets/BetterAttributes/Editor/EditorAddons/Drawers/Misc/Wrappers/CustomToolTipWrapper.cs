using System.Reflection;
using Better.Attributes.Runtime.Misc;
using Better.Commons.EditorAddons.Drawers.Caching;
using UnityEditor;
using UnityEngine;

namespace Better.Attributes.EditorAddons.Drawers.Misc.Wrappers
{
    public class CustomToolTipWrapper : MiscWrapper
    {
        private CustomTooltipAttribute _tooltipAttribute;
        private const char Asterisk = '*';

        public override void SetProperty(SerializedProperty property, FieldInfo fieldInfo, MiscAttribute attribute)
        {
            base.SetProperty(property, fieldInfo, attribute);
            _tooltipAttribute = (CustomTooltipAttribute)attribute;
        }

        public override void PreDraw(Rect position, GUIContent label)
        {
            if (!string.IsNullOrEmpty(_tooltipAttribute.Tooltip) && !string.IsNullOrEmpty(label.text))
            {
                label.tooltip = _tooltipAttribute.Tooltip;
                var addingSymbol = _tooltipAttribute.TooltipSymbol == default ? Asterisk : _tooltipAttribute.TooltipSymbol;
                label.text += addingSymbol.ToString();
            }
        }

        public override void PostDraw()
        {
        }

        public override HeightCacheValue GetHeight(GUIContent label)
        {
            return HeightCacheValue.GetAdditive(0);
        }
    }
}