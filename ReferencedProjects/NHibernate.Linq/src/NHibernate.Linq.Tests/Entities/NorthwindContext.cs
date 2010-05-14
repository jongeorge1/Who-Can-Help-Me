using System.Linq;
using Northwind.Entities;

namespace NHibernate.Linq.Tests.Entities
{
	public class NorthwindContext : NHibernateContext
	{
		public NorthwindContext(ISession session)
			: base(session)
		{
		}

		public IOrderedQueryable<Category> Categories
		{
			get { return Session.Linq<Category>(); }
		}

		public IOrderedQueryable<Customer> Customers
		{
			get { return Session.Linq<Customer>(); }
		}

		public IOrderedQueryable<CustomerCustomerDemo> CustomerCustomerDemos
		{
			get { return Session.Linq<CustomerCustomerDemo>(); }
		}

		public IOrderedQueryable<CustomerDemographic> CustomerDemographics
		{
			get { return Session.Linq<CustomerDemographic>(); }
		}

		public IOrderedQueryable<Employee> Employees
		{
			get { return Session.Linq<Employee>(); }
		}

		public IOrderedQueryable<EmployeeTerritory> EmployeeTerritories
		{
			get { return Session.Linq<EmployeeTerritory>(); }
		}

		public IOrderedQueryable<Order> Orders
		{
			get { return Session.Linq<Order>(); }
		}

		public IOrderedQueryable<OrderDetail> OrderDetails
		{
			get { return Session.Linq<OrderDetail>(); }
		}

		public IOrderedQueryable<Product> Products
		{
			get { return Session.Linq<Product>(); }
		}

		public IOrderedQueryable<Region> Regions
		{
			get { return Session.Linq<Region>(); }
		}

		public IOrderedQueryable<Shipper> Shippers
		{
			get { return Session.Linq<Shipper>(); }
		}

		public IOrderedQueryable<Supplier> Suppliers
		{
			get { return Session.Linq<Supplier>(); }
		}

		public IOrderedQueryable<Territory> Territories
		{
			get { return Session.Linq<Territory>(); }
		}
	}
}