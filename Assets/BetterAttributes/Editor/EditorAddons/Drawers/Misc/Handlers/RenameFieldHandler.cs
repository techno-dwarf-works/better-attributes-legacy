using Better.Attributes.Runtime.Misc;
using Better.Commons.EditorAddons.Drawers;

namespace Better.Attributes.EditorAddons.Drawers.Misc
{
    public class RenameFieldHandler : MiscLabelHandler
    {
        protected override void OnUpdateLabel(LabelContainer labelContainer)
        {
            var name = ((RenameFieldAttribute)_attribute).Name;
            labelContainer.Text = name;
        }
    }
}