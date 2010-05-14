namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    #region Using Directives

    using System.Collections.Generic;

    #endregion

    public interface ICategoryTasks
    {
        IList<Category> GetAll();

        Category Get(int categoryId);
    }
}
