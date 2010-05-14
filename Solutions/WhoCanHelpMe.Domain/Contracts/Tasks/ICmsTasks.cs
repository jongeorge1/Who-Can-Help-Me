using System.Collections.Generic;
using N2;

namespace WhoCanHelpMe.Domain.Contracts.Tasks
{
    public interface ICmsTasks
    {
        IList<ContentItem> GetNavigationItems();
    }
}