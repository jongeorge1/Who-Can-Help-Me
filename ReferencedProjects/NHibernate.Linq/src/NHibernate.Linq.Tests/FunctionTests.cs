using System.Linq;
using NHibernate.Linq.SqlClient;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	[Ignore("Calling functions doesn't work currently")]
	public class FunctionTests : BaseTest
	{
		[Test]
		public void SubstringFunction()
		{
			var query = from e in db.Employees
						where db.Methods.Substring(e.FirstName, 1, 2) == "An"
						select e;

			ObjectDumper.Write(query);
		}

		[Test]
		public void LeftFunction()
		{
			var query = from e in db.Employees
						where db.Methods.Substring(e.FirstName, 1, 2) == "An"
						select db.Methods.Left(e.FirstName, 3);

			ObjectDumper.Write(query);
		}

		[Test]
		public void ReplaceFunction()
		{
			var query = from e in db.Employees
						where e.FirstName.StartsWith("An")
						select new
								{
									Before = e.FirstName,
									AfterMethod = e.FirstName.Replace("An", "Zan"),
									AfterExtension = db.Methods.Replace(e.FirstName, "An", "Zan")
								};

			ObjectDumper.Write(query);
		}

		[Test]
		public void CharIndexFunction()
		{
			var query = from e in db.Employees
						where db.Methods.CharIndex(e.FirstName, 'A') == 1
						select e.FirstName;

			ObjectDumper.Write(query);
		}

		[Test]
		public void IndexOfFunctionExpression()
		{
			var query = from e in db.Employees
						where e.FirstName.IndexOf("An") == 1
						select e.FirstName;

			ObjectDumper.Write(query);
		}

		[Test]
		public void IndexOfFunctionProjection()
		{
			var query = from e in db.Employees
						where e.FirstName.Contains("a")
						select e.FirstName.IndexOf('A', 3);

			ObjectDumper.Write(query);
		}

		[Test]
		public void TwoFunctionExpression()
		{
			var query = from e in db.Employees
						where e.FirstName.IndexOf("A") == db.Methods.Month(e.BirthDate)
						select e.FirstName;

			ObjectDumper.Write(query);
		}
	}
}