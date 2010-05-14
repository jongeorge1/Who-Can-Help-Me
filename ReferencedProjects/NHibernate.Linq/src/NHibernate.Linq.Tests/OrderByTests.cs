using System.Linq;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class OrderByTests : BaseTest
	{
		[Test]
		public void AscendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						orderby c.CustomerID
						select c.CustomerID;

			var ids = query.ToList();

			if (ids.Count > 1)
			{
				Assert.Greater(ids[1], ids[0]);
			}
		}

		[Test]
		public void DescendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						orderby c.CustomerID descending
						select c.CustomerID;

			var ids = query.ToList();

			if (ids.Count > 1)
			{
				Assert.Greater(ids[0], ids[1]);
			}
		}

		[Test]
		[Ignore("NHibernate does not currently support subqueries in select clause (no way to specify a projection from a detached criteria).")]
		public void AggregateAscendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						orderby c.Orders.Count
						select c;

			var customers = query.ToList();

			if (customers.Count > 1)
			{
				Assert.Less(customers[0].Orders.Count, customers[1].Orders.Count);
			}
		}

		[Test]
		[Ignore("NHibernate does not currently support subqueries in select clause (no way to specify a projection from a detached criteria).")]
		public void AggregateDescendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						orderby c.Orders.Count descending
						select c;

			var customers = query.ToList();

			if (customers.Count > 1)
			{
				Assert.Greater(customers[0].Orders.Count, customers[1].Orders.Count);
			}
		}

		[Test]
		public void ComplexAscendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						where c.Country == "Belgium"
						orderby c.Country, c.City
						select c.City;

			var ids = query.ToList();

			if (ids.Count > 1)
			{
				Assert.Greater(ids[1], ids[0]);
			}
		}

		[Test]
		public void ComplexDescendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						where c.Country == "Belgium"
						orderby c.Country descending, c.City descending
						select c.City;

			var ids = query.ToList();

			if (ids.Count > 1)
			{
				Assert.Greater(ids[0], ids[1]);
			}
		}

		[Test]
		public void ComplexAscendingDescendingOrderByClause()
		{
			var query = from c in nwnd.Customers
						where c.Country == "Belgium"
						orderby c.Country ascending, c.City descending
						select c.City;

			var ids = query.ToList();

			if (ids.Count > 1)
			{
				Assert.Greater(ids[0], ids[1]);
			}
		}
	}
}
