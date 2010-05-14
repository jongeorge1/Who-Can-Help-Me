namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    public interface IProfileTasks
    {
        Profile GetProfileByUserName(string userName);

        Profile GetProfileById(int profileId);

        void RemoveAssertion(Profile profile, int assertionId);

        void AddAssertion(AddAssertionDetails addAssertionDetails);

        void CreateProfile(CreateProfileDetails createProfileDetails);

        void DeleteProfile(string userId);
    }
}