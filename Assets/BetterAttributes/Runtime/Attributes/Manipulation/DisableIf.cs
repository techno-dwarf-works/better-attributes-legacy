namespace Better.Attributes.Runtime.Manipulation
{
    public class DisableIf : ManipulateConditionAttribute
    {
        public DisableIf(string memberName, object memberValue) : base(memberName, memberValue, ManipulationMode.Disable)
        {
        }
    }
}