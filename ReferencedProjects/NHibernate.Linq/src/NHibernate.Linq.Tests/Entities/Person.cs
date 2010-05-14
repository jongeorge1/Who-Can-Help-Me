using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.Linq.Tests.Entities
{
	public class Person
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
	}
}
