namespace WhoCanHelpMe.Infrastructure.NHibernate
{
    /// <summary>
    /// Stores the list of valid session keys
    /// </summary>
    public class SessionKeys
    {
        /// <summary>
        /// Data session key
        /// </summary>
        public const string Data = "nhibernate.current_session";
    }
}