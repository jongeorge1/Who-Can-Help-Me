namespace WhoCanHelpMe.Framework.Validation
{
    #region Using Directives

    using NHibernate.Validator.Engine;

    #endregion

    /// <summary>
    /// Interface for a class level custom validator attribute 
    /// that validates based on more than one property. The
    /// primary property name is used to determine which of 
    /// the properties the validation error should refer to.
    /// </summary>
    public interface IValidateMultipleProperties : IRuleArgs
    {
        /// <summary>
        /// Gets PrimaryPropertyName.
        /// </summary>
        /// <value>
        /// The primary property name.
        /// </value>
        string PrimaryPropertyName
        {
            get;
        }
    }
}