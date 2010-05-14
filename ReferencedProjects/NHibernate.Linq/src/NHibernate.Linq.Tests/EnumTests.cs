using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class EnumTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void CanQueryOnEnumStoredAsInt32_High_1()
		{
			CanQueryOnEnumStoredAsInt32(EnumStoredAsInt32.High, 1);
		}

		[Test]
		public void CanQueryOnEnumStoredAsInt32_Unspecified_2()
		{
			CanQueryOnEnumStoredAsInt32(EnumStoredAsInt32.Unspecified, 2);
		}


		public void CanQueryOnEnumStoredAsInt32(EnumStoredAsInt32 type, int expectedCount)
		{
			var query = (from user in session.Linq<User>()
						 where user.Enum2 == type
						 select user).ToList();

			Assert.AreEqual(expectedCount, query.Count);
		}

		[Test]
		public void CanQueryOnEnumStoredAsString_Meduim_2()
		{
			CanQueryOnEnumStoredAsString(EnumStoredAsString.Medium, 2);

		}

		[Test]
		public void CanQueryOnEnumStoredAsString_Small_1()
		{
			CanQueryOnEnumStoredAsString(EnumStoredAsString.Small, 1);

		}

		public void CanQueryOnEnumStoredAsString(EnumStoredAsString type, int expectedCount)
		{
			var query = (from user in session.Linq<User>()
						 where user.Enum1 == type
						 select user).ToList();

			Assert.AreEqual(expectedCount, query.Count);
		}
	}
}
