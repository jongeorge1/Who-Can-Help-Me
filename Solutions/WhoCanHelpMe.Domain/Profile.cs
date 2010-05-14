namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using SharpArch.Core.DomainModel;

    #endregion

    [DebuggerDisplay("{FirstName} {LastName} - {UserName}")]
    public class Profile : Entity
    {
        public Profile()
        {
            this.Assertions = new List<Assertion>();
        }

        public virtual IList<Assertion> Assertions { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        [DomainSignature]
        public virtual string UserName { get; set; }
    }
}
