namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    using System;
    using System.Linq;

    using Domain;
    using Domain.Contracts.Repositories;
    using Domain.Contracts.Tasks;
    using Domain.Specifications;

    using Framework.Validation;

    using SharpArch.Core;

    using WhoCanHelpMe.Framework.Extensions;

    using xVal.ServerSide;

    #endregion

    public class ProfileTasks : IProfileTasks
    {
        #region Fields

        private readonly ICategoryRepository categoryRepository;
        private readonly IProfileRepository profileRepository;
        private readonly ITagRepository tagRepository;

        #endregion

        public ProfileTasks(
            IProfileRepository profileRepository, 
            ITagRepository tagRepository,
            ICategoryRepository categoryRepository)
        {
            this.profileRepository = profileRepository;
            this.tagRepository = tagRepository;
            this.categoryRepository = categoryRepository;
        }

        public void AddAssertion(string userName, int categoryId, string tagName) 
        {
            Check.Require(!userName.IsNullOrEmpty(), "userName must be provided.");
            Check.Require(categoryId > 0, "categoryId must be greater than 0.");
            Check.Require(!tagName.IsNullOrEmpty(), "tagName must be provided.");

            // TODO: Ideally we want a transaction here as we are potentially doing two updates.
            var profile = this.GetProfileByUserName(userName);

            var tag = this.tagRepository.FindOne(new TagByNameSpecification(tagName));

            if (tag == null)
            {
                tag = new Tag 
                    {
                        Name = tagName
                    };

                this.tagRepository.Save(tag);
            }

            // See if there's already an assertion for this tag/category combination
            var existingAssertion = profile.Assertions.FirstOrDefault(
                a => (a.Tag == tag) && (a.Category.Id == categoryId));

            // If not add it. If there is, do nothing further - there's no point returning an error, since the user has what they wanted
            if (existingAssertion == null)
            {
                var category = this.GetCategory(categoryId);

                var newAssertion = new Assertion
                    {
                        Profile = profile,
                        Category = category,
                        Tag = tag
                    };

                profile.Assertions.Add(newAssertion);

                this.profileRepository.Save(profile);
            }
        }

        public void CreateProfile(string userName, string firstName, string lastName)
        {
            Check.Require(!userName.IsNullOrEmpty(), "userName must be provided.");
            Check.Require(!firstName.IsNullOrEmpty(), "firstName must be provided.");
            Check.Require(!lastName.IsNullOrEmpty(), "lastName must be provided.");

            var profile = new Profile
                {
                    UserName = userName, 
                    FirstName = firstName, 
                    LastName = lastName, 
                    CreatedOn = DateTime.Now.Date 
                };

            try
            {
                this.profileRepository.Save(profile);
            }
            catch (Exception)
            {
                // Catching DB unique constraint violation and converting to DbC error
                // TODO: Should use nhibernate sql exception convertor to do this properly
                Check.Ensure(false, "Unable to create new profile; userName is already in use.");
            }
        }

        public void DeleteProfile(string userName)
        {
            Check.Require(!userName.IsNullOrEmpty(), "userName must be provided.");

            var profile = this.GetProfileByUserName(userName);

            if (profile != null)
            {
                this.profileRepository.Delete(profile);
            }
        }

        public Profile GetProfileById(int profileId)
        {
            return this.profileRepository.FindOne(new ProfileByIdSpecification(profileId));
        }

        public Profile GetProfileByUserName(string userName)
        {
            Check.Require(!userName.IsNullOrEmpty(), "userName must be provided.");

            return this.profileRepository.FindOne(new ProfileByUserNameSpecification(userName));
        }

        public void RemoveAssertion(Profile profile, int assertionId)
        {
            Check.Require(profile != null, "profile must be provided.");

            var assertionToRemove = profile.Assertions.FirstOrDefault(a => a.Id == assertionId);

            if (assertionToRemove != null)
            {
                profile.Assertions.Remove(assertionToRemove);

                this.profileRepository.Save(profile);
            }
        }

        private Category GetCategory(int categoryId)
        {
            var category = this.categoryRepository.FindOne(new CategoryByIdSpecification(categoryId));
            
            Check.Ensure(category != null, "Invalid categoryId");

            return category;
        }
    }
}
