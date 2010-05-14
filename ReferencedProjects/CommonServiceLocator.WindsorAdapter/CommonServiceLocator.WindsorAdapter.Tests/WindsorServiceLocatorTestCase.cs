namespace CommonServiceLocator.WindsorAdapter.Tests
{
	using Castle.MicroKernel.Registration;
	using Castle.Windsor;
	using Components;
	using Microsoft.Practices.ServiceLocation;
	using NUnit.Framework;

	[TestFixture]
	public class WindsorServiceLocatorTestCase : ServiceLocatorTestCase
	{
		protected override IServiceLocator CreateServiceLocator()
		{
			IWindsorContainer container = new WindsorContainer()
				.Register(
				AllTypes.Of<ILogger>()
					.FromAssembly(typeof(ILogger).Assembly)
					.WithService.FirstInterface()
				);
			return new WindsorServiceLocator(container);
		}

	}
}