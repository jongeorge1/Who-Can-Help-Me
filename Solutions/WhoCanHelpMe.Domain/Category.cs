namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System.Diagnostics;

    using SharpArch.Core.DomainModel;

    #endregion

    [DebuggerDisplay("{Name}")]
    public class Category : Entity
    {
        public virtual string Description { get; set; }

        [DomainSignature]
        public virtual string Name { get; set; }

        public virtual int SortOrder { get; set; }
    }
}