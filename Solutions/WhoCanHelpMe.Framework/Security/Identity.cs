namespace WhoCanHelpMe.Framework.Security
{
    using System.Diagnostics;
    using System.Security.Principal;

    [DebuggerDisplay("{UserName}")]
    public class Identity : IIdentity
    {
        public Identity(string name, string authenticationType, bool isAuthenticated)
        {
            this.Name = name;
            this.AuthenticationType = authenticationType;
            this.IsAuthenticated = isAuthenticated;
        }

        public string Name
        {
            get;
            private set;
        }

        public string AuthenticationType
        {
            get;
            private set;
        }

        public bool IsAuthenticated
        {
            get;
            private set;
        }
    }
}