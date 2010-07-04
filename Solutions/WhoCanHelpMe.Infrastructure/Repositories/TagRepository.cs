namespace WhoCanHelpMe.Infrastructure.Repositories
{
    using Domain;
    using Domain.Contracts.Repositories;

    using NHibernate;

    public class TagRepository : LinqRepository<Tag>, ITagRepository
    {
    }
}
