using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using NHibernate.Cfg;
using NHibernate.Linq.Tests.Entities;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	public class GlobalSetup
	{
		private static ISessionFactory factory;

		[SetUp]
		public void SetupNHibernate()
		{
			Configuration cfg = new Configuration().Configure();
			new SchemaExport(cfg).Execute(false, true, false);

			factory = cfg.BuildSessionFactory();

			CreateTestData();
		}

		[TearDown]
		public void TearDown()
		{

		}

		#region Test Data

		private static void CreatePatientData(ISession session)
		{
			State newYork = new State
			{
				Abbreviation = "NY",
				FullName = "New York"
			};
			State florida = new State
			{
				Abbreviation = "FL",
				FullName = "Florida"
			};

			Physician drDobbs = new Physician
			{
				Name = "Dr Dobbs"
			};
			Physician drWatson = new Physician
			{
				Name = "Dr Watson"
			};

			PatientRecord bobBarkerRecord = new PatientRecord
			{
				Name = new PatientName
				{
					FirstName = "Bob",
					LastName = "Barker"
				},
				Address = new Address
				{
					AddressLine1 = "123 Main St",
					City = "New York",
					State = newYork,
					ZipCode = "10001"
				},
				BirthDate = new DateTime(1930, 1, 1),
				Gender = Gender.Male
			};

			PatientRecord johnDoeRecord1 = new PatientRecord
			{
				Name = new PatientName
				{
					FirstName = "John",
					LastName = "Doe"
				},
				Address = new Address
				{
					AddressLine1 = "123 Main St",
					City = "Tampa",
					State = florida,
					ZipCode = "33602"
				},
				BirthDate = new DateTime(1969, 1, 1),
				Gender = Gender.Male
			};

			PatientRecord johnDoeRecord2 = new PatientRecord
			{
				Name = new PatientName
				{
					FirstName = "John",
					LastName = "Doe"
				},
				Address = new Address
				{
					AddressLine1 = "123 Main St",
					AddressLine2 = "Apt 2",
					City = "Tampa",
					State = florida,
					ZipCode = "33602"
				},
				BirthDate = new DateTime(1969, 1, 1)
			};

			Patient bobBarker = new Patient(new[] { bobBarkerRecord }, false, drDobbs);
			Patient johnDoe = new Patient(new[] { johnDoeRecord1, johnDoeRecord2 }, true, drWatson);

			session.Save(newYork);
			session.Save(florida);
			session.Save(drDobbs);
			session.Save(drWatson);
			session.Save(bobBarker);
			session.Save(johnDoe);
		}

		private static void CreateTestData()
		{
			var roles = new[]
            {
                new Role()
                {
                    Name = "Admin",
                    IsActive = true,
                    Entity = new AnotherEntity()
                    {
                        Output = "this is output..."
                    }
                },
                new Role()
                {
                    Name = "User",
                    IsActive = false
                }
            };

			var users = new[]
        	{
        		new User("ayende", DateTime.Today)
                {
                    Role = roles[0],
                    InvalidLoginAttempts = 4,
                    Enum1 = EnumStoredAsString.Medium,
                    Enum2 = EnumStoredAsInt32.High,
                    Component = new UserComponent()
                    {
                        Property1 = "test1",
                        Property2 = "test2",
                        OtherComponent = new UserComponent2()
                        {
                            OtherProperty1 = "othertest1"
                        }
                    }
                },
        		new User("rahien", new DateTime(1998, 12, 31))
                {
                    Role = roles[1],
                    InvalidLoginAttempts = 5,
                    Enum1 = EnumStoredAsString.Small,
                    Component = new UserComponent()
                    {
                        Property2 = "test2"
                    }
                },
        		new User("nhibernate", new DateTime(2000, 1, 1))
                {
                    InvalidLoginAttempts = 6,
                    LastLoginDate = DateTime.Now.AddDays(-1),
                    Enum1 = EnumStoredAsString.Medium
                }
        	};

			var timesheets = new[]
            {
                new Timesheet
                {
                    SubmittedDate = DateTime.Today,
                    Submitted = true
                },
                new Timesheet
                {
                    SubmittedDate = DateTime.Today.AddDays(-1),
                    Submitted = false, 
                    Entries = new List<TimesheetEntry>
                    {
                        new TimesheetEntry
                        {
                            EntryDate = DateTime.Today,
                            NumberOfHours = 6,
							Comments = "testing 123"
                        },
                        new TimesheetEntry
                        {
                            EntryDate = DateTime.Today.AddDays(1),
                            NumberOfHours = 14
                        }
                    }
                },
                new Timesheet
                {
                    SubmittedDate = DateTime.Now.AddDays(1),
                    Submitted = true,
                    Entries = new List<TimesheetEntry>
                    {
                        new TimesheetEntry
                        {
                            EntryDate = DateTime.Now.AddMinutes(20),
                            NumberOfHours = 4
                        },
                        new TimesheetEntry
                        {
                            EntryDate = DateTime.Now.AddMinutes(10),
                            NumberOfHours = 8,
							Comments = "testing 456"
                        },
                        new TimesheetEntry
                        {
                            EntryDate = DateTime.Now.AddMinutes(13),
                            NumberOfHours = 7
                        },
                        new TimesheetEntry
                        {
                            EntryDate = DateTime.Now.AddMinutes(45),
                            NumberOfHours = 38
                        }
                    }
                }
            };

			((IList<User>)timesheets[0].Users).Add(users[0]);
			((IList<User>)timesheets[1].Users).Add(users[0]);
			((IList<User>)timesheets[0].Users).Add(users[1]);

			var animals = new Animal[]
			{
				new Animal() { SerialNumber = "123", BodyWeight = 100 },
				new Lizard() { SerialNumber = "789", BodyWeight = 40, BodyTemperature = 14 },
				new Lizard() { SerialNumber = "1234", BodyWeight = 30, BodyTemperature = 18 },
				new Dog() { SerialNumber = "5678", BodyWeight = 156, BirthDate = new DateTime(1980, 07, 11) },
				new Dog() { SerialNumber = "9101", BodyWeight = 205, BirthDate = new DateTime(1980, 12, 13) },
				new Cat() { SerialNumber = "1121", BodyWeight = 115, Pregnant = true }
			};

			animals[0].Children = new[] { animals[3], animals[4] }.ToList();
			animals[5].Father = animals[3];
			animals[5].Mother = animals[4];

			animals[1].Children = new[] { animals[5] }.ToList();

			using (ISession session = CreateSession())
			{
				session.Delete("from Role");
				session.Delete("from User");
				session.Delete("from Timesheet");
				session.Delete("from Animal");
				session.Delete("from Physician");
				session.Delete("from Patient");
				session.Flush();

				foreach (Role role in roles)
					session.Save(role);

				foreach (User user in users)
					session.Save(user);

				foreach (Timesheet timesheet in timesheets)
					session.Save(timesheet);

				foreach (Animal animal in animals)
					session.Save(animal);

				CreatePatientData(session);

				session.Flush();
			}
		}

		#endregion

		public static ISession CreateSession()
		{
			return factory.OpenSession();
		}

		public static ISession CreateSession(IDbConnection con)
		{
			return factory.OpenSession(con);
		}
	}
}
