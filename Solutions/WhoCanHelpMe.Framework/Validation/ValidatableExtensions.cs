namespace WhoCanHelpMe.Framework.Validation
{
    #region Using Directives
    
    using System.Collections.Generic;
    using System.Linq;

    using WhoCanHelpMe.Framework.Extensions;

    using SharpArch.Core.CommonValidator;
    using SharpArch.Core.NHibernateValidator.CommonValidatorAdapter;

    using xVal.ServerSide;

    #endregion

    /// <summary>
    /// Extension methods for Entities
    /// </summary>
    public static class ValidatableExtensions
    {
        /// <summary>
        /// Validates an entity and throws a rules exception if there are validation errors
        /// </summary>
        /// <param name="entity">
        /// The entity to validate.
        /// </param>
        /// <exception cref="RulesException">
        /// </exception>
        public static void Validate(this IValidatable entity)
        {
            if (!entity.IsValid())
            {
                var errors = new List<ErrorInfo>();

                entity.ValidationResults().Each(x => errors.Add(x.GetErrorInfo()));

                throw new RulesException(errors);
            }
        }

        /// <summary>
        /// Gets the ErrorInfo from the validation result and the parent entity type
        /// </summary>
        /// <param name="result">
        /// The validation result.
        /// </param>
        /// <returns>
        /// The ErrorInfo
        /// </returns>
        private static ErrorInfo GetErrorInfo(this IValidationResult result)
        {
            if (result.PropertyName.IsNullOrEmpty())
            {
                return GetClassLevelErrorInfo(result);
            }

            return GetPropertyLevelErrorInfo(result);
        }

        /// <summary>
        /// Returns an ErrorInfo for a property level validation result
        /// </summary>
        /// <param name="result">
        /// The validation result.
        /// </param>
        /// <returns>
        /// The ErrorInfo
        /// </returns>
        private static ErrorInfo GetPropertyLevelErrorInfo(IValidationResult result)
        {
            return new ErrorInfo(((ValidationResult) result).InvalidValue.PropertyPath, result.Message);
        }

        /// <summary>
        /// Returns an ErrorInfo for a class level validation result
        /// </summary>
        /// <param name="result">
        /// The validation result.
        /// </param>
        /// <returns>
        /// The ErrorInfo
        /// </returns>
        private static ErrorInfo GetClassLevelErrorInfo(IValidationResult result)
        {
            var errorInfo = new ErrorInfo(string.Empty, result.Message);

            // Get the validation attributes on the entity type
            var validatorProperties = result.ClassContext.GetCustomAttributes(false)
                .Where(x => typeof(IValidateMultipleProperties).IsAssignableFrom(x.GetType()));

            // If the validation message matches one of the attributes messages,
            // then set the correct property path, based on the primary property name
            validatorProperties.Each(x =>
                {
                    if (result.Message == ((IValidateMultipleProperties) x).Message)
                    {
                        errorInfo =
                            new ErrorInfo(
                                ((ValidationResult)result).InvalidValue.PropertyPath + ((IValidateMultipleProperties) x).PrimaryPropertyName,
                                result.Message);
                    }
                });

            return errorInfo;
        }
    }
}