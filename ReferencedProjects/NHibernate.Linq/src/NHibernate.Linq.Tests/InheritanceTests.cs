using System;
using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class InheritanceTests : BaseTest
	{
		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		[Test]
		public void SelectAllAnimalsAndChildenFixBugAutoRelationShip()
		{
			var result = (from a in session.Linq<Animal>()
						  from b in a.Children.Cast<Animal>()
						  orderby a.Id, b.Id
						  select new { Parent = a.Id, Childen = b.Id }).ToArray();

			Assert.AreEqual(7, result.Length);

			Assert.AreEqual(1, result[0].Parent);
			Assert.AreEqual(1, result[1].Parent);
			Assert.AreEqual(2, result[2].Parent);
			Assert.AreEqual(3, result[3].Parent);
			Assert.AreEqual(4, result[4].Parent);
			Assert.AreEqual(5, result[5].Parent);
			Assert.AreEqual(6, result[6].Parent);

			Assert.AreEqual(4, result[0].Childen);
			Assert.AreEqual(5, result[1].Childen);
			Assert.AreEqual(6, result[2].Childen);
			Assert.AreEqual(0, result[3].Childen);
			Assert.AreEqual(0, result[4].Childen);
			Assert.AreEqual(0, result[5].Childen);
			Assert.AreEqual(0, result[6].Childen);
		}

        [Test]
		public void CanSelectLizardsUsingOfType()
		{
			var lizards = session.Linq<Animal>().OfType<Lizard>().ToArray();
			Assert.AreEqual(2, lizards.Length);
		}

		[Test]
		public void CanSelectMotherOfType()
		{
			var children = (from animal in session.Linq<Animal>()
							where animal.Mother is Mammal
							select animal).ToArray();

			var child = children.Single();
			Assert.AreEqual("1121", child.SerialNumber);
		}

		[Test]
		public void CanSelectChildrenOfType()
		{
			var animals = (from animal in session.Linq<Animal>()
						   where animal.Children.OfType<Mammal>().Any(m => m.Pregnant)
						   select animal).ToArray();

			Assert.AreEqual("789", animals.Single().SerialNumber);
		}
	}
}
