using System.Linq;

namespace NHibernate.Linq.Tests.Entities
{
	public class TestContext : NHibernateContext
	{
		public TestContext(ISession session)
			: base(session)
		{
		}

		public IOrderedQueryable<User> Users
		{
			get { return Session.Linq<User>(); }
		}

		public IOrderedQueryable<Role> Roles
		{
			get { return Session.Linq<Role>(); }
		}

		public IOrderedQueryable<Timesheet> Timesheets
		{
			get { return Session.Linq<Timesheet>(); }
		}
	}
}