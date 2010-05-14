using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class ProxiedEntityTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void QueryForProxiedEntity()
		{
			var query = (from e in session.Linq<ProxiedEntity>()
						 where e.Id == 1
						 select e);

			ObjectDumper.Write(query);
		}
	}
}