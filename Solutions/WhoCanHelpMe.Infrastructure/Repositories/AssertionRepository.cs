namespace WhoCanHelpMe.Infrastructure.Repositories
{
    #region Using Directives

    using Domain;
    using Domain.Contracts.Repositories;

    using NHibernate;

    #endregion

    public class AssertionRepository : LinqRepository<Assertion>, IAssertionRepository
    {
    }
}
