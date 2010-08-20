namespace WhoCanHelpMe.Infrastructure.NHibernate
{
    #region Using Directives

    using System.Linq;
    using Framework.Contracts.Specifications;

    using global::NHibernate.Linq;

    using SharpArch.Data.NHibernate;
    using WhoCanHelpMe.Domain.Contracts.Repositories;

    #endregion

    public class LinqRepository<T> : Repository<T>, ILinqRepository<T>
    {
        public override void Delete(T target)
        {
            this.Session.Delete(target);
        }
        
        public T FindOne(int id)
        {
            return this.Session.Get<T>(id);
        }

        public T FindOne(ILinqSpecification<T> specification)
        {
            return specification.SatisfyingElementsFrom(this.Session.Linq<T>()).SingleOrDefault();
        }

        public IQueryable<T> FindAll()
        {
            return this.Session.Linq<T>().AsQueryable();
        }

        public IQueryable<T> FindAll(ILinqSpecification<T> specification)
        {
            return specification.SatisfyingElementsFrom(this.Session.Linq<T>());
        }

        public void Save(T entity)
        {
            try
            {
                this.Session.Save(entity);
            }
            catch
            {
                if (this.Session.IsOpen)
                {
                    this.Session.Close();
                }

                throw;
            }

            this.Session.Flush();
        }

        public void SaveAndEvict(T entity)
        {
            this.Save(entity);
            this.Session.Evict(entity);
        }

    }
}