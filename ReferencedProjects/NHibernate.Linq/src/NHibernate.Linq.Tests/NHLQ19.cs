using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Linq.Expressions;
using NHibernate.Linq.Tests.Entities;
namespace NHibernate.Linq.Tests
{
	

	public interface IQuery<T>
	{
		Expression<Func<T, bool>> MatchingCriteria { get; }

	}
	public class NameQuery<T>:IQuery<T> where T:IHaveName
	{
		private readonly string name;

		public NameQuery(string name)
		{
			this.name = name;
		}

		public Expression<Func<T, bool>> MatchingCriteria
		{
			get{ return (x) => x.Name == name;}
		}
	}
	[TestFixture]
	public class NHLQ19:BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void CanUseExternalLambda()
		{
			var predicate = new NameQuery<Physician>("Dr Dobbs");
			var result = session.Linq<Physician>().Where(predicate.MatchingCriteria).ToList();
			Assert.That(result.Count,Is.GreaterThan(0));
		}
	}
}
