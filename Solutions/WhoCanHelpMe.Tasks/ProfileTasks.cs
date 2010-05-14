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

        public void AddAssertion(AddAssertionDetails addAssertionDetails) 
        {
            addAssertionDetails.Validate();

            // TODO: Ideally we want a transaction here as we are potentially doing two updates.

            var profile = this.GetProfileByUserName(addAssertionDetails.UserName);

            var tag = this.tagRepository.FindOne(new TagByNameSpecification(addAssertionDetails.TagName));

            if (tag == null)
            {
                tag = new Tag 
                    {
                        Name = addAssertionDetails.TagName
                    };

                this.tagRepository.Save(tag);
            }

            // See if there's already an assertion for this tag/category combination
            var existingAssertion = profile.Assertions.FirstOrDefault(
                a => (a.Tag == tag) && (a.Category.Id == addAssertionDetails.CategoryId));

            // If not add it. If there is, do nothing further - there's no point returning an error, since the user has what they wanted
            if (existingAssertion == null)
            {
                var category = this.GetCategory(addAssertionDetails.CategoryId);

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

        public void CreateProfile(CreateProfileDetails createProfileDetails)
        {
            createProfileDetails.Validate();

            var profile = new Profile {
                                          UserName = createProfileDetails.UserName,
                                          FirstName = createProfileDetails.FirstName,
                                          LastName = createProfileDetails.LastName,
                                          CreatedOn = DateTime.Now.Date
                                      };

            try
            {
                this.profileRepository.Save(profile);
            }
            catch (Exception)
            {
                // Catching DB unique constraint violation and converting to validation error
                // Should use nhibernate sql exception convertor to do this properly
                throw new RulesException("UserName", "User name already exists");
            }
        }

        public void DeleteProfile(string userId)
        {
            var profile = this.GetProfileByUserName(userId);

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
            return this.profileRepository.FindOne(new ProfileByUserNameSpecification(userName));
        }

        public void RemoveAssertion(Profile profile, int assertionId)
        {
            var assertionToRemove = profile.Assertions.FirstOrDefault(a => a.Id == assertionId);

            if (assertionToRemove != null)
            {
                profile.Assertions.Remove(assertionToRemove);

                this.profileRepository.Save(profile);
            }
        }

        Category GetCategory(int categoryId)
        {
            var category = this.categoryRepository.FindOne(new CategoryByIdSpecification(categoryId));
            
            if (category == null)
            {
                // If no category found, throw validation exception.
                throw new RulesException("CategoryId", "Invalid Category Id");
            }

            return category;
        }
    }
}
