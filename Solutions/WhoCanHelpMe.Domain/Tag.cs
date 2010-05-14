namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    using NHibernate.Validator.Constraints;

    using SharpArch.Core.DomainModel;

    #endregion

    [DebuggerDisplay("{Name}")]
    public class Tag : Entity
    {
        [NotNull]
        [NotEmpty]
        [DomainSignature]
        public virtual string Name { get; set; }

        [Min(0)]
        public virtual int Views { get; set; }
    }
}
