namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Collections.Generic;

    using Microsoft.Practices.ServiceLocation;

    using SharpArch.Core.CommonValidator;
    using SharpArch.Core.DomainModel;

    #endregion

    /// <summary>
    /// Value object that is validatable
    /// </summary>
    public class ValidatableValueObject : ValueObject, IValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatableValueObject"/> class. 
        /// </summary>
        protected ValidatableValueObject()
        {
        }

        /// <summary>
        /// Gets Validator.
        /// </summary>
        /// <value>
        /// The validator.
        /// </value>
        private IValidator Validator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IValidator>();
            }
        }

        /// <summary>
        /// Calls IsValid on the validator
        /// </summary>
        /// <returns>
        /// True if the object is valid
        /// </returns>
        public virtual bool IsValid()
        {
            return this.Validator.IsValid(this);
        }

        /// <summary>
        /// Collection of validation results
        /// </summary>
        /// <returns>
        /// The validation results
        /// </returns>
        public virtual ICollection<IValidationResult> ValidationResults()
        {
            return this.Validator.ValidationResultsFor(this);
        }
    }
}