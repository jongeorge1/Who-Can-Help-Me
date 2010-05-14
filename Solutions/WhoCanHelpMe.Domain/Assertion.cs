namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    using SharpArch.Core.DomainModel;

    #endregion

    [DebuggerDisplay("{Profile} {Category} {Tag}")]
    public class Assertion : Entity
    {
        [DomainSignature]
        public virtual Category Category { get; set; }

        [DomainSignature]
        public virtual Profile Profile { get; set; }

        [DomainSignature]
        public virtual Tag Tag { get; set; }
    }
}