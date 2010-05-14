using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class BinaryBooleanExpressionTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void TimesheetsWithEqualsTrue()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where timesheet.Entries.Any() == true
						 select timesheet).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void NegativeTimesheetsWithEqualsTrue()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where !timesheet.Entries.Any() == true
						 select timesheet).ToList();

			Assert.AreEqual(1, query.Count);
		}

		[Test]
		public void TimesheetsWithEqualsFalse()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where timesheet.Entries.Any() == false
						 select timesheet).ToList();

			Assert.AreEqual(1, query.Count);
		}

		[Test]
		public void NegativeTimesheetsWithEqualsFalse()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where !timesheet.Entries.Any() == false
						 select timesheet).ToList();

			Assert.AreEqual(2, query.Count);
		}
		/******************************************************************************/
		[Test]
		public void TimesheetsWithNotEqualsTrue()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where timesheet.Entries.Any() != true
						 select timesheet).ToList();

			Assert.AreEqual(1, query.Count);
		}

		[Test]
		public void NegativeTimesheetsWithNotEqualsTrue()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where !timesheet.Entries.Any() != true
						 select timesheet).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void TimesheetsWithNotEqualsFalse()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where timesheet.Entries.Any() != false
						 select timesheet).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void NegativeTimesheetsWithNotEqualsFalse()
		{
			var query = (from timesheet in session.Linq<Timesheet>()
						 where !timesheet.Entries.Any() != false
						 select timesheet).ToList();

			Assert.AreEqual(1, query.Count);
		}

		[Test]
		public void MammalsViaDynamicInvokedExpression()
		{
			//simulate dynamically created where clause
			Expression<Func<Mammal, bool>> expr1 = mammal => mammal.Pregnant;
			Expression<Func<Mammal, bool>> expr2 = mammal => false;

			var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
			var dynamicWhereClause = Expression.Lambda<Func<Mammal, bool>>
				  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);

			var animals = session.Linq<Mammal>().Where(dynamicWhereClause).ToArray();

			CollectionAssert.AreCountEqual(0, animals);
		}
	}
}
