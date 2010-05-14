using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class MethodCallTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void CanExecuteAny()
		{
			bool result = session.Linq<User>().Any();
			Assert.IsTrue(result);
		}

		[Test]
		public void CanExecuteAnyWithArguments()
		{
			bool result = session.Linq<User>().Any(u => u.Name == "user-does-not-exist");
			Assert.IsFalse(result);
		}

		[Test]
		public void CanExecuteCountWithOrderByArguments()
		{
			var query = session.Linq<User>().OrderBy(u => u.Name);
			int count = query.Count();
			Assert.AreEqual(3, count);
		}
	}
}
