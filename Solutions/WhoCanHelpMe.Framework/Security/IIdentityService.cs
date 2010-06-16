namespace WhoCanHelpMe.Framework.Security
{
    using System.Security.Principal;

    public interface IIdentityService
    {
        IIdentity GetCurrentIdentity();

        void SignOut();

        bool IsSignedIn();

        void Authenticate(string userId);
    }
}