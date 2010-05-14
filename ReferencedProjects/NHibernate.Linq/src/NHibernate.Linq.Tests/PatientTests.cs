using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class PatientTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void CanQueryOnPropertyOfComponent()
		{
			var query = (from pr in session.Linq<PatientRecord>()
						 where pr.Name.LastName == "Doe"
						 select pr).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void CanQueryOnManyToOneOfComponent()
		{
			var florida = session.Linq<State>().FirstOrDefault(x => x.Abbreviation == "FL");

			var query = (from pr in session.Linq<PatientRecord>()
						 where pr.Address.State == florida
						 select pr).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void CanQueryOnPropertyOfManyToOneOfComponent()
		{
			var query = (from pr in session.Linq<PatientRecord>()
						 where pr.Address.State.Abbreviation == "FL"
						 select pr).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void CanQueryOnPropertyOfOneToMany()
		{
			var query = (from p in session.Linq<Patient>()
						 where p.PatientRecords.Any(x => x.Gender == Gender.Unknown)
						 select p).ToList();

			Assert.AreEqual(1, query.Count);
		}

		[Test]
		public void CanQueryOnPropertyOfManyToOne()
		{
			var query = (from pr in session.Linq<PatientRecord>()
						 where pr.Patient.Active == true
						 select pr).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void CanQueryOnManyToOneOfManyToOne()
		{
			var drWatson = session.Linq<Physician>().FirstOrDefault(x => x.Name == "Dr Watson");

			var query = (from pr in session.Linq<PatientRecord>()
						 where pr.Patient.Physician == drWatson
						 select pr).ToList();

			Assert.AreEqual(2, query.Count);
		}

		[Test]
		public void CanQueryOnPropertyOfManyToOneOfManyToOne()
		{
			var query = (from pr in session.Linq<PatientRecord>()
						 where pr.Patient.Physician.Name == "Dr Watson"
						 select pr).ToList();

			Assert.AreEqual(2, query.Count);
		}
	}
}
