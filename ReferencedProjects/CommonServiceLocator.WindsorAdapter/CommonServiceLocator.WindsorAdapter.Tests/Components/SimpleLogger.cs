namespace CommonServiceLocator.WindsorAdapter.Tests.Components
{
	using System;

	public class SimpleLogger : ILogger
	{
		public void Log(string msg)
		{
			Console.WriteLine(msg);
		}
	}
}