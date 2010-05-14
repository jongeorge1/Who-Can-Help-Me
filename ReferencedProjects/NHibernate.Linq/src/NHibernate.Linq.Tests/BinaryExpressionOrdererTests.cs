using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class BinaryExpressionOrdererTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void ValuePropertySwapsToPropertyValue()
		{
			var query = (from user in nhib.Users
						 where ("ayende" == user.Name)
						 select user).ToList();
			Assert.AreEqual(1, query.Count);
		}
		[Test]
		public void PropertyValueDoesntSwaps()
		{
			var query = (from user in nhib.Users
						 where (user.Name == "ayende")
						 select user).ToList();
			Assert.AreEqual(1, query.Count);
		}

		[Test]
		public void PropertyPropertyDoesntSwap()
		{
			var query = (from user in nhib.Users
						 where (user.Name == user.Name)
						 select user).ToList();
			Assert.AreEqual(3, query.Count);
		}
		[Test]
		[Ignore]
		public void PropertyCriteriaDoesntSwap()
		{
		}
		[Test]
		[Ignore]
		public void CriteriaPropertySwapsToPropertyCriteria()
		{
		}
		[Test]
		[Ignore]
		public void ValueCriteriaDoesntSwap()
		{
		}
		[Test]
		[Ignore]
		public void CriteriaValueSwapsValueCriteria()
		{
		}
		[Test]
		[Ignore]
		public void CriteriaCriteriaDoesntSwap()
		{
		}

		[Test]
		public void EqualsSwapsToEquals()
		{
			var query = (from user in nhib.Users
						 where ("ayende" == user.Name)
						 select user).ToList();
			Assert.AreEqual(1, query.Count);
		}
		[Test]
		public void NotEqualsSwapsToNotEquals()
		{
			var query = (from user in nhib.Users
						 where ("ayende" != user.Name)
						 select user).ToList();
			Assert.AreEqual(2, query.Count);
		}
		[Test]
		public void GreaterThanSwapsToLessThan()
		{
			var query = (from user in nhib.Users
						 where (3 > user.Id)
						 select user).ToList();
			Assert.AreEqual(2, query.Count);
		}
		[Test]
		public void GreaterThanOrEqualToSwapsToLessThanOrEqualTo()
		{
			var query = (from user in nhib.Users
						 where (2 >= user.Id)
						 select user).ToList();
			Assert.AreEqual(2, query.Count);
		}
		[Test]
		public void LessThanSwapsToGreaterThan()
		{
			var query = (from user in nhib.Users
						 where (1 < user.Id)
						 select user).ToList();
			Assert.AreEqual(2, query.Count);
		}
		[Test]
		public void LessThanOrEqualToSwapsToGreaterThanOrEqualTo()
		{
			var query = (from user in nhib.Users
						 where (2 <= user.Id)
						 select user).ToList();
			Assert.AreEqual(2, query.Count);
		}
	}
}