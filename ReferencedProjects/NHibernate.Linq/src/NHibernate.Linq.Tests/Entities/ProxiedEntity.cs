namespace NHibernate.Linq.Tests.Entities
{
	public interface IProxiedEntity
	{
		int Id { get; }

		string Dummy { get; set; }
	}

	public class ProxiedEntity : IProxiedEntity
	{
		public int Id { get; set; }

		public string Dummy { get; set; }
	}

	public class AnotherProxiedEntity : IProxiedEntity
	{
		public int Id { get; private set; }

		public string Dummy { get; set; }
	}
}
