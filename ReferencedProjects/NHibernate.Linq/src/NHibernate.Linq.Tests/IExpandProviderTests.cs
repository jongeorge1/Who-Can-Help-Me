using System.Collections;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class IExpandProviderTests : BaseTest
	{
		IExpandProvider expander = null;

		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		// Create the IUpdatable Interface
		public override void Setup()
		{
			base.Setup();

			expander = nhib as IExpandProvider;
		}

		[Test]
		public void TestValidExpandProviderInterface()
		{
			Assert.IsNotNull(expander, "Context Object is not valid IExpandProvider interface");
		}

		[Test]
		public void TestExpand()
		{
			var qry = this.nhib.Timesheets.Where(t => t.Entries.Count > 0);
			ExpandSegmentCollection segments = new ExpandSegmentCollection(1);
			segments.Add(new ExpandSegment("Entries", null));
			ICollection<ExpandSegmentCollection> segmentCollections = new List<ExpandSegmentCollection>(1);
			segmentCollections.Add(segments);
			IEnumerable expandedQuery = expander.ApplyExpansions(qry, segmentCollections);

			Timesheet ts = expandedQuery.OfType<Timesheet>().FirstOrDefault();

			session.Evict(ts);

			Assert.IsNotNull(ts, "Failed to find Timesheet.");
			Assert.IsTrue(ts.Entries.Count > 0, "Failed to expand Entries");
		}
	}
}
