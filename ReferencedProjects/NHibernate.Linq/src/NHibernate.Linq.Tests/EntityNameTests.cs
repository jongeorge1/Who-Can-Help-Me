using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	using Entities;
	using Impl;
	using Visitors;

	[TestFixture]
	public class EntityNameTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void CanQueryOnEntityName()
		{
			var query = (from e in session.Linq<Person>("person")
						 where e.Id == 1
						 select e);

			var provider = query.Provider as NHibernateQueryProvider;
			var result=provider.TranslateExpression(query.Expression) as CriteriaImpl;
			Assert.That(result.EntityOrClassName,Is.EqualTo("person"));
			var resultList = query.ToList();
			Assert.That(resultList, Has.All.AssignableTo(typeof (Person)));
		}
	}
}
