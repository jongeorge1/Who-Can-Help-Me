namespace CommonServiceLocator.WindsorAdapter.Tests
{
	using System.Collections;
	using System.Collections.Generic;
	using Components;
	using Microsoft.Practices.ServiceLocation;
	using NUnit.Framework;

	public abstract class ServiceLocatorTestCase
	{
		private IServiceLocator locator;

		[SetUp]
		public void SetUp()
		{
			locator = CreateServiceLocator();
		}

		protected abstract IServiceLocator CreateServiceLocator();

		[Test]
		public void GetInstance()
		{
			ILogger instance = locator.GetInstance<ILogger>();
			Assert.IsNotNull(instance, "instance should not be null");
		}

		[Test]
		[ExpectedException(typeof(ActivationException))]
		public void AskingForInvalidComponentShouldRaiseActivationException()
		{
			locator.GetInstance<IDictionary>();
		}

		[Test]
		public void GetNamedInstance()
		{
			ILogger instance = locator.GetInstance<ILogger>(typeof(AdvnacedLogger).FullName);
			Assert.IsInstanceOfType(typeof(AdvnacedLogger), instance, "Should be an advanced logger");
		}

		[Test]
		public void GetNamedInstance2()
		{
			ILogger instance = locator.GetInstance<ILogger>(typeof(SimpleLogger).FullName);
			Assert.IsInstanceOfType(typeof(SimpleLogger), instance, "Should be a simple logger");
		}

		[Test]
		[ExpectedException(typeof(ActivationException))]
		public void GetNamedInstance_WithZeroLenName()
		{
			locator.GetInstance<ILogger>("");
		}

		[Test]
		[ExpectedException(typeof(ActivationException))]
		public void GetUnknownInstance2()
		{
			locator.GetInstance<ILogger>("test");
		}

		[Test]
		public void GetAllInstances()
		{
			IEnumerable<ILogger> instances = locator.GetAllInstances<ILogger>();
			IList<ILogger> list = new List<ILogger>(instances);
			Assert.AreEqual(2, list.Count);
		}

		[Test]
		public void GetlAllInstance_ForUnknownType_ReturnEmptyEnumerable()
		{
			IEnumerable<IDictionary> instances = locator.GetAllInstances<IDictionary>();
			IList<IDictionary> list = new List<IDictionary>(instances);
			Assert.AreEqual(0, list.Count);
		}

		[Test]
		public void GenericOverload_GetInstance()
		{
			Assert.AreEqual(
				locator.GetInstance<ILogger>().GetType(),
				locator.GetInstance(typeof(ILogger), null).GetType(),
				"should get the same type"
				);
		}

		[Test]
		public void GenericOverload_GetInstance_WithName()
		{
			Assert.AreEqual(
				locator.GetInstance<ILogger>(typeof(AdvnacedLogger).FullName).GetType(),
				locator.GetInstance(typeof(ILogger), typeof(AdvnacedLogger).FullName).GetType(),
				"should get the same type"
				);
		}

		[Test]
		public void Overload_GetInstance_NoName_And_NullName()
		{
			Assert.AreEqual(
				locator.GetInstance<ILogger>().GetType(),
				locator.GetInstance<ILogger>(null).GetType(),
				"should get the same type"
				);
		}

		[Test]
		public void GenericOverload_GetAllInstances()
		{
			List<ILogger> genericLoggers = new List<ILogger>(locator.GetAllInstances<ILogger>());
			List<object> plainLoggers = new List<object>(locator.GetAllInstances(typeof(ILogger)));
			Assert.AreEqual(genericLoggers.Count, plainLoggers.Count);
			for (int i = 0; i < genericLoggers.Count; i++)
			{
				Assert.AreEqual(
					genericLoggers[i].GetType(),
					plainLoggers[i].GetType(),
					"instances (" + i + ") should give the same type");
			}
		}

	}
}