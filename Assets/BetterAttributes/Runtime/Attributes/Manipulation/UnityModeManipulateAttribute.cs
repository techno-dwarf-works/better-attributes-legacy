namespace Better.Attributes.Runtime.Manipulation
{
    public enum UnityMode
    {
        PlayMode,
        EditorMode
    }

    public abstract class UnityModeManipulateAttribute : ManipulateAttribute
    {
        public UnityMode Mode { get; }

        public UnityModeManipulateAttribute(UnityMode mode, ManipulationMode modeType) : base(modeType)
        {
            Mode = mode;
        }
    }
}