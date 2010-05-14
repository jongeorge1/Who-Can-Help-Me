using System.Linq;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class PagingTests : BaseTest
	{
		[Test]
		public void Customers1to5()
		{
			var q = (from c in nwnd.Customers select c.CustomerID).Take(5);
			var query = q.ToList();

			Assert.AreEqual(5, query.Count);
		}

		[Test]
		public void Customers11to20()
		{
			var query = (from c in nwnd.Customers
						 orderby c.CustomerID
						 select c.CustomerID).Skip(10).Take(10).ToList();
			Assert.AreEqual(query[0], "BSBEV");
			Assert.AreEqual(10, query.Count);
		}

		[Test]
		[Ignore("NHibernate does not currently support subqueries in from clause")]
		public void CustomersChainedTake()
		{
			var q = (from c in nwnd.Customers
					 orderby c.CustomerID
					 select c.CustomerID).Take(5).Take(6);
			var query = q.ToList();
			Assert.AreEqual(query[0], "BLAUS");
			Assert.AreEqual(5, query.Count);
		}

		[Test]
		[Ignore("NHibernate does not currently support subqueries in from clause")]
		public void CustomersChainedSkip()
		{
			var q = (from c in nwnd.Customers select c.CustomerID).Skip(10).Skip(5);
			var query = q.ToList();
			Assert.AreEqual(query[0], "CONSH");
			Assert.AreEqual(76, query.Count);
		}



		[Test]
		[Ignore("NHibernate does not currently support subqueries in from clause")]
		public void CountAfterTakeShouldReportTheCorrectNumber()
		{
			var users = nwnd.Customers.Skip(3).Take(10);
			Assert.AreEqual(10, users.Count());
		}
	}
}
