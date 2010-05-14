namespace WhoCanHelpMe.Framework.Caching
{
    public interface ICacheable
    {
        string GenerateCacheKey();
    }
}