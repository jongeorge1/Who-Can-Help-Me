namespace WhoCanHelpMe.Domain
{
    #region Using Directives

    using System;
    using System.Diagnostics;

    using SharpArch.Core.DomainModel;

    #endregion

    [DebuggerDisplay("{Headline}")]
    public class NewsItem : EntityWithTypedId<string>
    {
        public virtual string Author { get; set; }

        public virtual string AuthorPhotoUrl { get; set; }

        public virtual string AuthorUrl { get; set; }

        public virtual string Headline { get; set; }

        public virtual DateTime PublishedTime { get; set; }

        [DomainSignature]
        public virtual string Url { get; set; }
    }
}
