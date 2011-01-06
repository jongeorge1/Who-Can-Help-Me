namespace WhoCanHelpMe.Tasks.Contracts
{
    #region Using Directives

    using System.Collections.Generic;

    using WhoCanHelpMe.Domain;

    #endregion

    public interface ICategoryQueryTasks
    {
        IList<Category> GetAll();

        Category Get(int categoryId);
    }
}
