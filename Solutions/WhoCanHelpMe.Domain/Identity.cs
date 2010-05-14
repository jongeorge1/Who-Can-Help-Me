namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    #endregion

    [DebuggerDisplay("{UserName}")]
    public class Identity
    {
        public string UserName { get; set; }
    }
}