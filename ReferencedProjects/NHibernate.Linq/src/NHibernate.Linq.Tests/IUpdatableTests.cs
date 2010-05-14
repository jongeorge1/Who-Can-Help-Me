using System;
using System.Linq;
using NHibernate.Linq.Tests.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	using System.Data.Services;

	[TestFixture]
	public class IUpdatableTests : BaseTest
	{
		IUpdatable update = null;

		protected override ISession CreateSession()
		{
			return GlobalSetup.CreateSession();
		}

		public override void Setup()
		{
			base.Setup();
			this.update = this.nwnd as IUpdatable;
			AddUser();
		}

		public override void TearDown()
		{
			new GlobalSetup().SetupNHibernate();
		}

		private void AddUser()
		{
			User usr = (User)update.CreateResource("Users", typeof(User).FullName);
			usr.Name = "bgates";
			usr.RegisteredAt = DateTime.Now.AddDays(-1); ;
			usr.InvalidLoginAttempts = 6;
			usr.LastLoginDate = DateTime.Now.AddMinutes(-1);
			usr.Enum1 = EnumStoredAsString.Large;
			this.session.Save(usr);
			this.session.Flush();
		}

		[Test]
		public void TestValidUpdateInterface()
		{
			Assert.IsNotNull(update, "Context Object is not valid IUpdatable interface");
		}

		[Test]
		public void TestClearChanges()
		{
			update.ClearChanges();

			// No externally testability so no test except for if it throws exceptions
		}

		[Test]
		public void TestGetResource()
		{
			// Get a known customer
			var qry = this.nhib.Users.Where(u => u.Id == 1);
			object result = update.GetResource(qry, typeof(User).FullName);
			Assert.IsNotNull(result, "GetResource failed to return a value");

			User user = result as User;

			Assert.IsInstanceOfType(typeof(User), user, "GetResource returned wrong type");
			Assert.IsTrue(user.Id == 1, "GetResource returned wrong instance");

			// Clear it so we're not in an unsafe state
			update.ClearChanges();
		}

		[Test]
		public void TestCreateResource()
		{
			// Create the new object
			object newUser = update.CreateResource("Users", typeof(User).FullName);
			Assert.IsInstanceOfType(typeof(User), newUser);

			// Clear it so we're not in an unsafe state
			update.ClearChanges();
		}

		[Test]
		public void TestSetValue()
		{
			// Create a new customer
			const string userName = "swildermuth";
			object newUser = update.CreateResource("Users", typeof(User).FullName);

			// Update a key
			update.SetValue(newUser, "Id", 1000);

			// Update a non-key
			update.SetValue(newUser, "Name", userName);

			// Test the values
			User user = (User)newUser;
			Assert.AreEqual(user.Id, 1000);
			Assert.AreEqual(user.Name, userName);

			// Clear it so we're not in an unsafe state
			update.ClearChanges();
		}

		[Test]
		public void TestGetValue()
		{
			// Get a known customer
			var qry = this.nhib.Users.Where(u => u.Id == 1);
			object result = update.GetResource(qry, typeof(User).FullName);

			// Get a key
			object key = update.GetValue(result, "Id");
			Assert.AreEqual(key, 1);
			object name = update.GetValue(result, "Name");
			Assert.AreEqual(name, "ayende");
		}

		[Test]
		public void TestSaveChanges()
		{
			User usr = (User)update.CreateResource("Users", typeof(User).FullName);
			usr.Name = "bgates2";
			usr.RegisteredAt = DateTime.Now.AddDays(-1); ;
			usr.InvalidLoginAttempts = 6;
			usr.LastLoginDate = DateTime.Now.AddMinutes(-1);
			usr.Enum1 = EnumStoredAsString.Large;
			update.SaveChanges();

			var qry = from u in nhib.Users
					  where u.Name == "bgates2"
					  select u;

			Assert.IsTrue(qry.Count() == 1, "Failed to save new user");
			update.ClearChanges();
			session.Delete(usr);
		}

		[Test]
		public void TestDeleteResource()
		{
			var qry = from u in nhib.Users
					  where u.Name == "bgates"
					  select u;

			User usr = qry.First();
			update.DeleteResource(usr);
			update.SaveChanges();
			session.Evict(usr);
			var qry2 = from u in nhib.Users
					   where u.Name == "bgates"
					   select u;

			Assert.IsTrue(qry2.Count() == 0, "Failed to delete object");
		}

		[Test]
		public void TestSetReference()
		{
			var qry = this.nhib.Roles.Where(r => r.Name == "User");
			object role = update.GetResource(qry, typeof(Role).FullName);
			Assert.IsInstanceOfType(typeof(Role), role);

			object user = update.CreateResource("Roles", typeof(User).FullName);
			update.SetValue(user, "Name", "tlinus");
			update.SetValue(user, "RegisteredAt", DateTime.Now.AddDays(-1));
			update.SetReference(user, "Role", role);
			Assert.IsInstanceOfType(typeof(User), user);
			User actualUser = (User)user;

			Assert.IsInstanceOfType(typeof(Role), actualUser.Role);

		}

		[Test]
		public void TestAddReferenceToCollection()
		{
			object ts = update.GetResource(nhib.Timesheets.Where(t => !t.Submitted), typeof(Timesheet).FullName);
			Assert.IsInstanceOfType(typeof(Timesheet), ts);
			Timesheet actualTimesheet = (Timesheet)ts;

			TimesheetEntry entry = new TimesheetEntry()
			{
				EntryDate = DateTime.Now,
				NumberOfHours = 5
			};

			update.AddReferenceToCollection(ts, "Entries", entry);

			Assert.IsTrue(actualTimesheet.Entries.Contains(entry));

		}

		[Test]
		public void TestRemoveReferenceFromCollection()
		{
			object ts = update.GetResource(nhib.Timesheets.Where(t => !t.Submitted), typeof(Timesheet).FullName);
			Assert.IsInstanceOfType(typeof(Timesheet), ts);
			Timesheet actualTimesheet = (Timesheet)ts;

			TimesheetEntry entry = actualTimesheet.Entries[0];

			update.RemoveReferenceFromCollection(ts, "Entries", entry);

			Assert.IsFalse(actualTimesheet.Entries.Contains(entry));
		}

		[Test]
		public void TestResolveResource()
		{
			object usr = update.CreateResource("Users", typeof(User).FullName);
			object usr2 = update.ResolveResource(usr);
			Assert.AreSame(usr, usr2);
		}

	}
}
