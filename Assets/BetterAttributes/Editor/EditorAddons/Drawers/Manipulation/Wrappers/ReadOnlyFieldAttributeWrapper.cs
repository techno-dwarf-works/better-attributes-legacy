namespace Better.Attributes.EditorAddons.Drawers.Manipulation.Wrappers
{
    public class ReadOnlyFieldAttributeWrapper : ManipulateWrapper
    {
        protected override bool IsConditionSatisfied()
        {
            return true;
        }
    }
}