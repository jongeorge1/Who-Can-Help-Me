namespace WhoCanHelpMe.Domain.Contracts.Configuration
{
    public interface IConfigurationService
    {
        IAnalyticsConfiguration Analytics { get; }
        INewsConfiguration News { get; }
    }
}