using System.Linq;
using System.Reflection;
using NHibernate.Impl;
using Northwind.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class QueryUtilityTests : BaseTest
	{
		private INHibernateQueryable<Customer> query;

		[SetUp]
		public override void Setup()
		{
			base.Setup();
			query = (from c in session.Linq<Customer>()
					 select c) as INHibernateQueryable<Customer>;
		}

		[Test]
		public void MethodsWorkLikeFluentInterfaces()
		{
			var utility = query.QueryOptions;
			Assert.AreEqual(utility, utility.SetCacheMode(CacheMode.Put));
			Assert.AreEqual(utility, utility.SetCachable(true));
			Assert.AreEqual(utility, utility.SetCacheRegion("cacheRegion"));
			Assert.AreEqual(utility, utility.SetComment("comment"));
		}

		[Test]
		public void SetCacheableActsOnInnerCriteria()
		{
			CriteriaImpl criteria = null;
			query.QueryOptions
				.RegisterCustomAction(x => criteria = x as CriteriaImpl)
				.SetCachable(true);
			query.ToList();
			Assert.AreEqual(criteria.Cacheable, true);
		}

		[Test]
		public void SetCacheModeActsOnInnerCriteria()
		{
			CriteriaImpl criteria = null;
			query.QueryOptions
				.RegisterCustomAction(x => criteria = x as CriteriaImpl)
				.SetCacheMode(CacheMode.Ignore);
			query.ToList();
			object value = criteria.GetType().GetField("cacheMode", BindingFlags.Instance | BindingFlags.NonPublic)
				.GetValue(criteria);
			Assert.AreEqual(value, CacheMode.Ignore);
		}

		[Test]
		public void SetCacheRegionOnInnerCriteria()
		{
			CriteriaImpl criteria = null;
			query.QueryOptions
				.RegisterCustomAction(x => criteria = x as CriteriaImpl)
				.SetCacheRegion("someRegion");
			query.ToList();
			Assert.AreEqual("someRegion", criteria.CacheRegion);
		}

		[Test]
		public void SetCommentActsOnInnerCriteria()
		{
			CriteriaImpl criteria = null;
			query.QueryOptions
				.RegisterCustomAction(x => criteria = x as CriteriaImpl)
				.SetComment("something");
			query.ToList();
			Assert.AreEqual("something", criteria.Comment);
		}

		[Test]
		public void RegisterCustomActionActsOnInnerCriteria()
		{
			bool b = false;
			query.QueryOptions.RegisterCustomAction(delegate(ICriteria criteria)
			{
				Assert.IsNotNull(criteria);
				b = true;
			});
			query.ToList();
			Assert.IsTrue(b);
		}

		[Test]
		public void ActionIsInvokedForImmediateResult()
		{
			CriteriaImpl crit = null;
			bool customActionWasCalled = false;
			query.QueryOptions
				.SetCachable(true)
				.RegisterCustomAction(delegate(ICriteria criteria)
										{
											Assert.IsNotNull(criteria);
											crit = criteria as CriteriaImpl;
											customActionWasCalled = true;
										});
			query.FirstOrDefault();
			Assert.IsTrue(crit.Cacheable);
			Assert.IsTrue(customActionWasCalled);
		}
	}
}
