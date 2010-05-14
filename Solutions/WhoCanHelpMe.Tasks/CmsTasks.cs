using System;
using System.Collections.Generic;
using N2;
using N2.Web;
using WhoCanHelpMe.Domain.Contracts.Tasks;

namespace WhoCanHelpMe.Tasks
{
    #region Using Directives

    #endregion

    public class CmsTasks : ICmsTasks
    {
        private readonly IUrlParser n2UrlParser;

        public CmsTasks(IUrlParser n2UrlParser)
        {
            this.n2UrlParser = n2UrlParser;
        }

        public IList<ContentItem> GetNavigationItems()
        {
            return n2UrlParser.StartPage.Children;
        }
    }
}
