namespace WhoCanHelpMe.Tasks.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using WhoCanHelpMe.Domain;

    #endregion

    public interface IAssertionQueryTasks
    {
        IList<Assertion> ByTag(string tagName);
    }
}
