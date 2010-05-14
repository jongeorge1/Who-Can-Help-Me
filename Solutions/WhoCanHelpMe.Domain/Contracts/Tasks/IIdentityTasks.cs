namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    public interface IIdentityTasks
    {
        Identity GetCurrentIdentity();

        void SignOut();

        bool IsSignedIn();

        void Authenticate(string userId);
    }
}