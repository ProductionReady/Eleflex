namespace Eleflex
{
    /// <summary>
    /// Represents an object used to manage patches for the Eleflex system. Will upgrade existing installations based on the currently loaded code base.
    /// </summary>
    public partial interface ISystemPatchManager
    {
        PatchSystemSummary GetSystemSummary();

        bool Update();

    }
}
