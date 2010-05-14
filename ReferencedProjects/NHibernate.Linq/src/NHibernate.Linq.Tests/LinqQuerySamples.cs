using System;
using System.Collections.Generic;
using System.Linq;
using Northwind.Entities;
using NUnit.Framework;

namespace NHibernate.Linq.Tests
{
	[TestFixture]
	public class LinqQuerySamples : BaseTest
	{
		#region Where Tests

		[Category("WHERE")]
		[Test(Description = "This sample uses WHERE to filter for Customers in London.")]
		public void DLinq1()
		{
			var q =
				from c in db.Customers
				where c.City == "London"
				orderby c.CustomerID
				select c;

			AssertByIds(q, new[] {
				"AROUT",
				"BSBEV",
				"CONSH",
				"EASTC",
				"NORTS",
				"SEVES"
				}, x => x.CustomerID);
		}

		private static void AssertByIds<T, K>(IEnumerable<T> q, K[] ids, Converter<T, K> getId)
		{
			var current = 0;
			foreach (var customer in q)
			{
				Assert.AreEqual(ids[current], getId(customer));
				current += 1;
			}
			Assert.AreEqual(current, ids.Length);
		}

		[Category("WHERE")]
		[Test(Description = "This sample uses WHERE to filter for Employees hired during or after 1994.")]
		public void DLinq2()
		{
			var q =
				from e in db.Employees
				where e.HireDate >= new DateTime(1994, 1, 1)
				select e;
			AssertByIds(q, new[] { 7, 8, 9 }, x => x.EmployeeID);
		}

		[Category("WHERE")]
		[Test(Description = "This sample uses WHERE to filter for Products that have stock below their reorder level and are not discontinued.")]
		public void DLinq3()
		{
			var q =
				from p in db.Products
				where p.UnitsInStock <= p.ReorderLevel && !p.Discontinued
				select p;

			AssertByIds(q, new[] { 2, 3, 11, 21, 30, 31, 32, 37, 43, 45, 48, 49, 56, 64, 66, 68, 70, 74, }, x => x.ProductID);
		}

		[Category("WHERE")]
		[Test(Description = "This sample uses WHERE to filter for Products that have stock below their reorder level and are not discontinued.")]
		public void DLinq3b()
		{
			var q =
				from p in db.Products
				where p.UnitsInStock <= p.ReorderLevel && p.Discontinued == false
				select p;

			AssertByIds(q, new[] { 2, 3, 11, 21, 30, 31, 32, 37, 43, 45, 48, 49, 56, 64, 66, 68, 70, 74, }, x => x.ProductID);
		}

		[Category("WHERE")]
		[Test(Description = "This sample uses WHERE to filter out Products that are either UnitPrice is greater than 10 or is discontinued.")]
		public void DLinq4()
		{
			var q =
				from p in db.Products
				where p.UnitPrice > 10m || p.Discontinued
				select p;

			AssertByIds(q,
				new[]{
					1 ,2 ,4 ,5 ,6 ,7 ,8 ,9 ,10,
					11,12,14,15,16,17,18,20,22,
					24,25,26,27,28,29,30,31,32,
					34,35,36,37,38,39,40,42,43,
					44,46,48,49,50,51,53,55,56,
					57,58,59,60,61,62,63,64,65,
					66,67,68,69,70,71,72,73,76,
					77,
					}, x => x.ProductID);

		}

		[Category("WHERE")]
		[Test(Description = "This sample calls WHERE twice to filter out Products that UnitPrice is greater than 10" +
					 " and is discontinued.")]
		public void DLinq5()
		{
			IEnumerable<Product> q =
				db.Products.Where(p => p.UnitPrice > 10m).Where(p => p.Discontinued);

			AssertByIds(q, new[] { 5, 9, 17, 28, 29, 42, 53, }, x => x.ProductID);

		}

		[Category("WHERE")]
		[Test(Description = "This sample uses First to select the first Shipper in the table.")]
		public void DLinq6()
		{
			Shipper shipper = db.Shippers.First();
			Assert.AreEqual(1, shipper.ShipperID);
		}

		[Category("WHERE")]
		[Test(Description = "This sample uses First to select the single Customer with CustomerID 'BONAP'.")]
		public void DLinq7()
		{
			Customer cust = db.Customers.First(c => c.CustomerID == "BONAP");
			Assert.AreEqual("BONAP", cust.CustomerID);
		}

		[Category("WHERE")]
		[Test(Description = "This sample uses First to select an Order with freight greater than 10.00.")]
		public void DLinq8()
		{
			Order ord = db.Orders.First(o => o.Freight > 10.00M);
			Assert.AreEqual(10248, ord.OrderID);
		}

		#endregion Where Tests

		#region Select/Distinct Tests

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT to return a sequence of just the Customers' contact names.")]
		public void DLinq9()
		{
			var q =
				from c in db.Customers
				select c.ContactName;
			IList<string> items = q.ToList();
			Assert.AreEqual(91, items.Count);
			items.Each(x => Assert.IsNotNull(x));
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and anonymous types to return a sequence of just the Customers' contact names and phone numbers.")]
		public void DLinq10()
		{
			var q =
				from c in db.Customers
				select new { c.ContactName, c.Phone };
			var items = q.ToList();

			Assert.AreEqual(91, items.Count);

			items.Each(x =>
						{
							Assert.IsNotNull(x.ContactName);
							Assert.IsNotNull(x.Phone);
						});
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and anonymous types to return " +
					 "a sequence of just the Employees' names and phone numbers, " +
					 "with the FirstName and LastName fields combined into a single field, 'Name', " +
					 "and the HomePhone field renamed to Phone in the resulting sequence.")]
		public void DLinq11()
		{
			var q =
				from e in db.Employees
				select new { Name = e.FirstName + " " + e.LastName, Phone = e.HomePhone };
			var items = q.ToList();
			Assert.AreEqual(9, items.Count);

			items.Each(x =>
			{
				Assert.IsNotNull(x.Name);
				Assert.IsNotNull(x.Phone);
			});
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and anonymous types to return " +
					 "a sequence of all Products' IDs and a calculated value " +
					 "called HalfPrice which is set to the Product's UnitPrice " +
					 "divided by 2.")]
		public void DLinq12()
		{
			var q =
				from p in db.Products
				select new { p.ProductID, p.UnitPrice, HalfPrice = p.UnitPrice / 2 };
			foreach (var item in q)
			{
				Assert.IsTrue((item.UnitPrice / 2) == item.HalfPrice);
			}
		}

		[Category("SELECT/DISTINCT")]
		[Test]
		public void DLinq12b()
		{
			var q =
				from p in db.Products
				select new { p.ProductID, p.UnitPrice, HalfPrice = p.UnitPrice * 2 };
			foreach (var item in q)
			{
				Assert.IsTrue(item.UnitPrice * 2 == item.HalfPrice);
			}
		}

		[Category("SELECT/DISTINCT")]
		[Test]
		public void DLinq12c()
		{
			var q =
				from p in db.Products
				select new { p.ProductID, p.UnitPrice, HalfPrice = p.UnitPrice + 2 };
			foreach (var item in q)
			{
				Assert.IsTrue(item.UnitPrice + 2 == item.HalfPrice);
			}
		}

		[Category("SELECT/DISTINCT")]
		[Test]
		public void DLinq12d()
		{
			var q =
				from p in db.Products
				select new { p.ProductID, p.UnitPrice, HalfPrice = p.UnitPrice - 2 };
			foreach (var item in q)
			{
				Assert.IsTrue(item.UnitPrice - 2 == item.HalfPrice);
			}
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and a conditional statment to return a sequence of product " +
					 " name and product availability.")]
		public void DLinq13()
		{
			var q =
				from p in db.Products
				select new { p.ProductName, Availability = p.UnitsInStock - p.UnitsOnOrder < 0 ? "Out Of Stock" : "In Stock" };

			ObjectDumper.Write(q, 1);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and a known type to return a sequence of employees' names.")]
		public void DLinq14()
		{
			var q =
				from e in db.Employees
				select new Name { FirstName = e.FirstName, LastName = e.LastName };

			ObjectDumper.Write(q, 1);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and WHERE to return a sequence of " +
					 "just the London Customers' contact names.")]
		public void DLinq15()
		{
			var q =
				from c in db.Customers
				where c.City == "London"
				select c.ContactName;

			ObjectDumper.Write(q);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses SELECT and anonymous types to return " +
					 "a shaped subset of the data about Customers.")]
		public void DLinq16()
		{
			var q =
				from c in db.Customers
				select new
						{
							c.CustomerID,
							CompanyInfo = new { c.CompanyName, c.City, c.Country },
							ContactInfo = new { c.ContactName, c.ContactTitle }
						};

			ObjectDumper.Write(q, 1);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses nested queries to return a sequence of " +
					 "all orders containing their OrderID, a subsequence of the " +
					 "items in the order where there is a discount, and the money " +
					 "saved if shipping is not included.")]
		[Ignore("TODO")]
		public void DLinq17()
		{
			var q =
				from o in db.Orders
				select new
						{
							o.OrderID,
							DiscountedProducts =
					from od in o.OrderDetails.Cast<OrderDetail>()
					where od.Discount > 0.0f
					select od,
							FreeShippingDiscount = o.Freight
						};

			ObjectDumper.Write(q, 1);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses nested queries to return a sequence of " +
					 "all orders containing their OrderID, a subsequence of the " +
					 "items in the order where there is a discount, and the money " +
					 "saved if shipping is not included.")]
		[Ignore("TODO")]
		public void DLinq17b()
		{
			var q =
				from o in db.Orders
				select new
						{
							o.OrderID,
							DiscountedProducts =
					from od in o.OrderDetails.Cast<OrderDetail>()
					where od.Discount > 0.0f
					select new { od.Quantity, od.UnitPrice },
							FreeShippingDiscount = o.Freight
						};

			ObjectDumper.Write(q, 1);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses nested queries to return a sequence of " +
					 "all orders containing their OrderID, a subsequence of the " +
					 "items in the order where there is a discount, and the money " +
					 "saved if shipping is not included.")]
		[Ignore("TODO")]
		public void DLinq17c()
		{
			var q =
				from o in db.Orders
				select new
						{
							o.OrderID,
							DiscountedProducts =
					from od in o.OrderDetails.Cast<OrderDetail>()
					where od.Discount > 0.0f
					orderby od.Discount descending
					select od,
							FreeShippingDiscount = o.Freight
						};

			ObjectDumper.Write(q, 1);
		}

		[Category("SELECT/DISTINCT")]
		[Test(Description = "This sample uses Distinct to select a sequence of the unique cities " +
					 "that have Customers.")]
		public void DLinq18()
		{
			var q = (
						from c in db.Customers
						select c.City)
				.Distinct();

			ObjectDumper.Write(q);
		}

		#endregion Select/Distinct Tests

		#region Count/Sum/Min/Max/Avg Tests

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Count to find the number of Customers in the database.")]
		public void DLinq19()
		{
			int q = db.Customers.Count();
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Count to find the number of Products in the database " +
					 "that are not discontinued.")]
		public void DLinq20()
		{
			int q = db.Products.Count(p => !p.Discontinued);
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Sum to find the total freight over all Orders.")]
		public void DLinq21()
		{
			decimal? q = db.Orders.Select(o => o.Freight).Sum();
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Sum to find the total number of units on order over all Products.")]
		public void DLinq22()
		{
			int? q = db.Products.Sum(p => p.UnitsOnOrder);
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Min to find the lowest unit price of any Product.")]
		public void DLinq23()
		{
			decimal? q = db.Products.Select(p => p.UnitPrice).Min();
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Min to find the lowest freight of any Order.")]
		public void DLinq24()
		{
			decimal? q = db.Orders.Min(o => o.Freight);
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Min to find the Products that have the lowest unit price " +
					 "in each category.")]
		[Ignore("TODO")]
		public void DLinq25()
		{
			var categories =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								CategoryID = g.Key,
								CheapestProducts =
					from p2 in g
					where p2.UnitPrice == g.Min(p3 => p3.UnitPrice)
					select p2
							};

			ObjectDumper.Write(categories, 1);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Max to find the latest hire date of any Employee.")]
		public void DLinq26()
		{
			DateTime? q = db.Employees.Select(e => e.HireDate).Max();
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Max to find the most units in stock of any Product.")]
		public void DLinq27()
		{
			short? q = db.Products.Max(p => p.UnitsInStock);
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Max to find the Products that have the highest unit price " +
					 "in each category.")]
		[Ignore("TODO")]
		public void DLinq28()
		{
			var categories =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								MostExpensiveProducts =
					from p2 in g
					where p2.UnitPrice == g.Max(p3 => p3.UnitPrice)
					select p2
							};

			ObjectDumper.Write(categories, 1);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Average to find the average freight of all Orders.")]
		public void DLinq29()
		{
			decimal? q = db.Orders.Select(o => o.Freight).Average();
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Average to find the average unit price of all Products.")]
		public void DLinq30()
		{
			decimal? q = db.Products.Average(p => p.UnitPrice);
			Console.WriteLine(q);
		}

		[Category("COUNT/SUM/MIN/MAX/AVG")]
		[Test(Description = "This sample uses Average to find the Products that have unit price higher than " +
					 "the average unit price of the category for each category.")]
		[Ignore("TODO")]
		public void DLinq31()
		{
			var categories =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								ExpensiveProducts =
					from p2 in g
					where p2.UnitPrice > g.Average(p3 => p3.UnitPrice)
					select p2
							};

			ObjectDumper.Write(categories, 1);
		}

		#endregion Count/Sum/Min/Max/Avg Test

		#region Join Tests

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to select all orders for customers in London.")]
		public void DLinqJoin1()
		{
			var q =
				from c in db.Customers
				from o in c.Orders.Cast<Order>()
				where c.City == "London"
				select o;

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to select all orders for customers in London.")]
		public void DLinqJoin1a()
		{
			var q =
				from c in db.Customers
				from o in c.Orders.Cast<Order>()
				where c.City == "London"
				select new { o.OrderDate, o.ShipRegion };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to select all orders for customers in London.")]
		public void DLinqJoin1b()
		{
			var q =
				from c in db.Customers
				from o in c.Orders.Cast<Order>()
				where c.City == "London"
				select new { c.City, o.OrderDate, o.ShipRegion };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to select all orders for customers.")]
		public void DLinqJoin1c()
		{
			var q =
				from c in db.Customers
				from o in c.Orders.Cast<Order>()
				select o;

			var list = q.ToList();

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to select all orders for customers.")]
		public void DLinqJoin1d()
		{
			var q =
				from c in db.Customers
				from o in c.Orders.Cast<Order>()
				select o.OrderDate;

			var list = q.ToList();

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to select all orders for customers.")]
		public void DLinqJoin1e()
		{
			var q =
				from c in db.Customers
				from o in c.Orders.Cast<Order>()
				select c;

			var list = q.ToList();

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "where clause to filter for Products whose Supplier is in the USA " +
					 "that are out of stock.")]
		public void DLinqJoin2()
		{
			var q =
				from p in db.Products
				where p.Supplier.Country == "USA" && p.UnitsInStock == 0
				select p;

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "from clause to filter for employees in Seattle, " +
					 "and also list their territories.")]
		[Ignore("TODO")]
		public void DLinqJoin3()
		{
			var q =
				from e in db.Employees
				from et in e.EmployeeTerritories.Cast<EmployeeTerritory>()
				where e.City == "Seattle"
				select new { e.FirstName, e.LastName, et.PK_EmployeeTerritories.Territory.TerritoryDescription };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample uses foreign key navigation in the " +
					 "select clause to filter for pairs of employees where " +
					 "one employee reports to the other and where " +
					 "both employees are from the same City.")]
		public void DLinqJoin4()
		{
			var q =
				from e1 in db.Employees
				from e2 in e1.Employees.Cast<Employee>()
				where e1.City == e2.City
				select new
						{
							FirstName1 = e1.FirstName,
							LastName1 = e1.LastName,
							FirstName2 = e2.FirstName,
							LastName2 = e2.LastName,
							e1.City
						};

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample explictly joins two tables and projects results from both tables.")]
		[Ignore("TODO")]
		public void DLinqJoin5()
		{
			var q =
				from c in db.Customers
				join o in db.Orders on c.CustomerID equals o.Customer.CustomerID into orders
				select new { c.ContactName, OrderCount = orders.Count() };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample explictly joins three tables and projects results from each of them.")]
		[Ignore("TODO")]
		public void DLinqJoin6()
		{
			var q =
				from c in db.Customers
				join o in db.Orders on c.CustomerID equals o.Customer.CustomerID into ords
				join e in db.Employees on c.City equals e.City into emps
				select new { c.ContactName, ords = ords.Count(), emps = emps.Count() };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample shows how to get LEFT OUTER JOIN by using DefaultIfEmpty(). The DefaultIfEmpty() method returns null when there is no Order for the Employee.")]
		[Ignore("TODO")]
		public void DLinqJoin7()
		{
			var q =
				from e in db.Employees
				join o in db.Orders on e equals o.Employee into ords
				from o in ords.DefaultIfEmpty()
				select new { e.FirstName, e.LastName, Order = o };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample projects a 'let' expression resulting from a join.")]
		[Ignore("TODO")]
		public void DLinqJoin8()
		{
			var q =
				from c in db.Customers
				join o in db.Orders on c.CustomerID equals o.Customer.CustomerID into ords
				let z = c.City + c.Country
				from o in ords
				select new { c.ContactName, o.OrderID, z };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample shows a join with a composite key.")]
		[Ignore("TODO")]
		public void DLinqJoin9()
		{
			var q =
				from o in db.Orders
				from p in db.Products
				join d in db.OrderDetails
					on new { o.OrderID, p.ProductID } equals new { d.PK_Order_Details.Order.OrderID, d.PK_Order_Details.Product.ProductID }
					into details
				from d in details
				select new { o.OrderID, p.ProductID, d.UnitPrice };

			ObjectDumper.Write(q);
		}

		[Category("JOIN")]
		[Test(Description = "This sample shows how to construct a join where one side is nullable and the other isn't.")]
		[Ignore("TODO")]
		public void DLinqJoin10()
		{
			var q =
				from o in db.Orders
				join e in db.Employees
					on o.Employee.EmployeeID equals (int?)e.EmployeeID into emps
				from e in emps
				select new { o.OrderID, e.FirstName };

			ObjectDumper.Write(q);
		}

		#endregion Join Tests

		#region Order By Tests

		[Category("ORDER BY")]
		[Test(Description = "This sample uses orderby to sort Employees by hire date.")]
		public void DLinq36()
		{
			var q =
				from e in db.Employees
				orderby e.HireDate
				select e;

			ObjectDumper.Write(q);
		}

		[Category("ORDER BY")]
		[Test(Description = "This sample uses where and orderby to sort Orders " +
					 "shipped to London by freight.")]
		public void DLinq37()
		{
			var q =
				from o in db.Orders
				where o.ShipCity == "London"
				orderby o.Freight
				select o;

			ObjectDumper.Write(q);
		}

		[Category("ORDER BY")]
		[Test(Description = "This sample uses orderby to sort Products " +
					 "by unit price from highest to lowest.")]
		public void DLinq38()
		{
			var q =
				from p in db.Products
				orderby p.UnitPrice descending
				select p;

			ObjectDumper.Write(q);
		}

		[Category("ORDER BY")]
		[Test(Description = "This sample uses a compound orderby to sort Customers " +
					 "by city and then contact name.")]
		public void DLinq39()
		{
			var q =
				from c in db.Customers
				orderby c.City, c.ContactName
				select c;

			ObjectDumper.Write(q);
		}

		[Category("ORDER BY")]
		[Test(Description = "This sample uses orderby to sort Orders from EmployeeID 1 " +
					 "by ship-to country, and then by freight from highest to lowest.")]
		public void DLinq40()
		{
			var q =
				from o in db.Orders
				where o.Employee.EmployeeID == 1
				orderby o.ShipCountry, o.Freight descending
				select o;

			ObjectDumper.Write(q);
		}


		[Category("ORDER BY")]
		[Test(Description = "This sample uses Orderby, Max and Group By to find the Products that have " +
					 "the highest unit price in each category, and sorts the group by category id.")]
		[Ignore("TODO")]
		public void DLinq41()
		{
			var categories =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					orderby g.Key
					select new
							{
								g.Key,
								MostExpensiveProducts =
					from p2 in g
					where p2.UnitPrice == g.Max(p3 => p3.UnitPrice)
					select p2
							};

			ObjectDumper.Write(categories, 1);
		}

		#endregion Order By Tests

		#region Group By Methods

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by to partition Products by " +
					 "CategoryID.")]
		[Ignore("TODO")]
		public void DLinq42()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select g;

			ObjectDumper.Write(q, 1);

			foreach (var o in q)
			{
				Console.WriteLine("\n{0}\n", o);

				foreach (var p in o)
				{
					ObjectDumper.Write(p);
				}
			}
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Max " +
					 "to find the maximum unit price for each CategoryID.")]
		[Ignore("TODO")]
		public void DLinq43()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								MaxPrice = g.Max(p => p.UnitPrice)
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Min " +
					 "to find the minimum unit price for each CategoryID.")]
		public void DLinq44()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								MinPrice = g.Min(p => p.UnitPrice)
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Average " +
					 "to find the average UnitPrice for each CategoryID.")]
		public void DLinq45()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								AveragePrice = g.Average(p => p.UnitPrice)
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Sum " +
					 "to find the total UnitPrice for each CategoryID.")]
		public void DLinq46()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								TotalPrice = g.Sum(p => p.UnitPrice)
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Count " +
					 "to find the number of Products in each CategoryID.")]
		public void DLinq47()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								NumProducts = g.Count()
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Count " +
					 "to find the number of Products in each CategoryID " +
					 "that are discontinued.")]
		public void DLinq48()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								NumProducts = g.Count(p => p.Discontinued)
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses group by and Count " +
					 "to find the number of Products in each CategoryID " +
					 "that are not discontinued.")]
		public void DLinq48b()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					select new
							{
								g.Key,
								NumProducts = g.Count(p => !p.Discontinued)
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses a where clause after a group by clause " +
					 "to find all categories that have at least 10 products.")]
		public void DLinq49()
		{
			var q =
				from p in db.Products
				group p by p.Category.CategoryID
					into g
					where g.Count() >= 10
					select new
							{
								g.Key,
								ProductCount = g.Count()
							};

			ObjectDumper.Write(q, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses Group By to group products by CategoryID and SupplierID.")]
		[Ignore("TODO")]
		public void DLinq50()
		{
			var categories =
				from p in db.Products
				group p by new { p.Category.CategoryID, p.Supplier.SupplierID }
					into g
					select new { g.Key, g };

			ObjectDumper.Write(categories, 1);
		}

		[Category("GROUP BY/HAVING")]
		[Test(Description = "This sample uses Group By to return two sequences of products. " +
					 "The first sequence contains products with unit price " +
					 "greater than 10. The second sequence contains products " +
					 "with unit price less than or equal to 10.")]
		public void DLinq51()
		{
			var categories =
				from p in db.Products
				group p by new { Criterion = p.UnitPrice > 10 }
					into g
					select g;

			ObjectDumper.Write(categories, 1);
		}

		#endregion Group By Methods

		#region Exists/In/Any/All Methods

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses Any to return only Customers that have no Orders.")]
		public void DLinq52()
		{
			var q =
				from c in db.Customers
				where !c.Orders.Cast<Order>().Any()
				select c;

			ObjectDumper.Write(q);

			foreach (Customer c in q)
				Assert.IsTrue(!c.Orders.Cast<Order>().Any());
		}

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses Any to return only Customers that have Orders.")]
		public void DLinq52b()
		{
			var q =
				from c in db.Customers
				where c.Orders.Cast<Order>().Any()
				select c;

			ObjectDumper.Write(q);

			foreach (Customer c in q)
				Assert.IsTrue(c.Orders.Cast<Order>().Any());
		}

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses Any to return only Categories that have " +
					 "at least one Discontinued product.")]
		public void DLinq53()
		{
			var q =
				from c in db.Categories
				where c.Products.Cast<Product>().Any(p => p.Discontinued)
				select c;

			ObjectDumper.Write(q);

			foreach (Category c in q)
				Assert.IsTrue(c.Products.Cast<Product>().Any(p => p.Discontinued));
		}

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses Any to return only Categories that have " +
					 "zero Discontinued products.")]
		public void DLinq53b()
		{
			var q =
				from c in db.Categories
				where c.Products.Cast<Product>().Any(p => !p.Discontinued)
				select c;

			ObjectDumper.Write(q);

			foreach (Category c in q)
				Assert.IsTrue(c.Products.Cast<Product>().Any(p => !p.Discontinued));
		}

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses Any to return only Categories that does not have " +
					 "at least one Discontinued product.")]
		public void DLinq53c()
		{
			var q =
				from c in db.Categories
				where !c.Products.Cast<Product>().Any(p => p.Discontinued)
				select c;

			ObjectDumper.Write(q);

			foreach (Category c in q)
				Assert.IsTrue(!c.Products.Cast<Product>().Any(p => p.Discontinued));
		}

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses Any to return only Categories that does not have " +
					 "any Discontinued products.")]
		public void DLinq53d()
		{
			var q =
				from c in db.Categories
				where !c.Products.Cast<Product>().Any(p => !p.Discontinued)
				select c;

			ObjectDumper.Write(q);

			foreach (Category c in q)
				Assert.IsTrue(!c.Products.Cast<Product>().Any(p => !p.Discontinued));
		}

		[Category("EXISTS/IN/ANY/ALL")]
		[Test(Description = "This sample uses All to return Customers whom all of their orders " +
					 "have been shipped to their own city or whom have no orders.")]
		[Ignore("TODO")]
		public void DLinq54()
		{
			var q =
				from c in db.Customers
				where c.Orders.Cast<Order>().All(o => o.ShipCity == c.City)
				select c;

			ObjectDumper.Write(q);

			foreach (Customer c in q)
				Assert.IsTrue(c.Orders.Cast<Order>().All(o => o.ShipCity == c.City));
		}

		#endregion Exists/In/Any/All Methods

		#region Union Methods

		[Category("UNION ALL/UNION/INTERSECT")]
		[Test(Description = "This sample uses Concat to return a sequence of all Customer and Employee " +
					 "phone/fax numbers.")]
		[Ignore("TODO")]
		public void DLinq55()
		{
			var q = (
						from c in db.Customers
						select c.Phone
					).Concat(
				from c in db.Customers
				select c.Fax
				).Concat(
				from e in db.Employees
				select e.HomePhone
				);

			ObjectDumper.Write(q);
		}

		[Category("UNION ALL/UNION/INTERSECT")]
		[Test(Description = "This sample uses Concat to return a sequence of all Customer and Employee " +
					 "name and phone number mappings.")]
		[Ignore("TODO")]
		public void DLinq56()
		{
			var q = (
						from c in db.Customers
						select new { Name = c.CompanyName, c.Phone }
					).Concat(
				from e in db.Employees
				select new { Name = e.FirstName + " " + e.LastName, Phone = e.HomePhone }
				);

			ObjectDumper.Write(q);
		}

		[Category("UNION ALL/UNION/INTERSECT")]
		[Test(Description = "This sample uses Union to return a sequence of all countries that either " +
					 "Customers or Employees are in.")]
		[Ignore("TODO")]
		public void DLinq57()
		{
			var q = (
						from c in db.Customers
						select c.Country
					).Union(
				from e in db.Employees
				select e.Country
				);

			ObjectDumper.Write(q);
		}

		[Category("UNION ALL/UNION/INTERSECT")]
		[Test(Description = "This sample uses Intersect to return a sequence of all countries that both " +
					 "Customers and Employees live in.")]
		[Ignore("TODO")]
		public void DLinq58()
		{
			var q = (
						from c in db.Customers
						select c.Country
					).Intersect(
				from e in db.Employees
				select e.Country
				);

			ObjectDumper.Write(q);
		}

		[Category("UNION ALL/UNION/INTERSECT")]
		[Test(Description = "This sample uses Except to return a sequence of all countries that " +
					 "Customers live in but no Employees live in.")]
		[Ignore("TODO")]
		public void DLinq59()
		{
			var q = (
						from c in db.Customers
						select c.Country
					).Except(
				from e in db.Employees
				select e.Country
				);

			ObjectDumper.Write(q);
		}

		#endregion Union Methods

		#region Top/Bottom/Paging Methods

		[Category("TOP/BOTTOM")]
		[Test(Description = "This sample uses Take to select the first 5 Employees hired.")]
		public void DLinq60()
		{
			var q = (
						from e in db.Employees
						orderby e.HireDate
						select e)
				.Take(5);

			ObjectDumper.Write(q);
		}

		[Category("TOP/BOTTOM")]
		[Test(Description = "This sample uses Skip to select all but the 10 most expensive Products.")]
		public void DLinq61()
		{
			var q = (
						from p in db.Products
						orderby p.UnitPrice descending
						select p)
				.Skip(10);

			ObjectDumper.Write(q);
		}

		[Category("Paging")]
		[Test(Description = "This sample uses the Skip and Take operators to do paging by " +
					 "skipping the first 50 records and then returning the next 10, thereby " +
					 "providing the data for page 6 of the Products table.")]
		public void DLinq62()
		{
			var q = (
						from c in db.Customers
						orderby c.ContactName
						select c)
				.Skip(50)
				.Take(10);

			ObjectDumper.Write(q);
		}

		[Category("Paging")]
		[Test(Description = "This sample uses a where clause and the Take operator to do paging by, " +
					 "first filtering to get only the ProductIDs above 50 (the last ProductID " +
					 "from page 5), then ordering by ProductID, and finally taking the first 10 results, " +
					 "thereby providing the data for page 6 of the Products table.  " +
					 "Note that this method only works when ordering by a unique key.")]
		public void DLinq63()
		{
			var q = (
						from p in db.Products
						where p.ProductID > 50
						orderby p.ProductID
						select p)
				.Take(10);

			ObjectDumper.Write(q);
		}

		#endregion Top/Bottom/Paging Methods
	}
}
