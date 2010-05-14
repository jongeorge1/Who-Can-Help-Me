using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class PropertyMethodMappingTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		[Ignore("NHibernate does not currently support subqueries in select clause (no way to specify a projection from a detached criteria).")]
		public void CanExecuteCountInSelectClause()
		{
			var results = session.Linq<Timesheet>()
				.Select(t => t.Entries.Count).ToList();

			Assert.AreEqual(3, results.Count);
			Assert.AreEqual(0, results[0]);
			Assert.AreEqual(2, results[1]);
			Assert.AreEqual(4, results[2]);
		}

		[Test]
		public void CanExecuteCountInWhereClause()
		{
			var results = session.Linq<Timesheet>()
				.Where(t => t.Entries.Count >= 2).ToList();

			Assert.AreEqual(2, results.Count);
		}
	}
}
