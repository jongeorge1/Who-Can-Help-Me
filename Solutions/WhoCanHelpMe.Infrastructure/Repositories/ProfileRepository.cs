namespace WhoCanHelpMe.Infrastructure.Repositories
{
    #region Using Directives

    using Domain;
    using Domain.Contracts.Repositories;

    using NHibernate;

    using SharpArch.Data.NHibernate;

    #endregion

    [SessionFactory(SessionKeys.Data)]
    public class ProfileRepository : LinqRepository<Profile>, IProfileRepository
    {
    }
}