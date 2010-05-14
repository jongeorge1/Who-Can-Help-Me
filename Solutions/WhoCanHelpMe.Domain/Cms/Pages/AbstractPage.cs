namespace WhoCanHelpMe.Domain.Cms.Pages
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Security.Principal;
    using N2;
    using N2.Collections;
    using N2.Web.UI;

    #endregion

    /// <summary>
    /// A base class for page items. 
    /// </summary>
    [TabContainer(Tabs.Content, "Content", 0)]
    public abstract class AbstractPage : ContentItem, IItemContainer
    {
        #region IItemContainer Members

        /// <summary>
        /// Gets the item associated with the item container.
        /// </summary>
        public ContentItem CurrentItem
        {
            get { return this; }
        }

        #endregion

        /// <summary>
        /// Overridden for performance reasons 
        /// to save N2 having to do role based
        /// authorization look ups for each page
        /// </summary>
        /// <param name="user">
        /// The current user.
        /// </param>
        /// <returns>
        /// Always true
        /// </returns>
        public override bool IsAuthorized(IPrincipal user)
        {
            return true;
        }

        /// <summary>
        /// Gets all children of the specified type
        /// of the current item
        /// </summary>
        /// <typeparam name="T">
        /// The type filter
        /// </typeparam>
        /// <returns>
        /// The list of children
        /// </returns>
        public virtual IList<T> GetChildren<T>() where T : ContentItem
        {
            return new ItemList<T>(
                Children,
                new TypeFilter(typeof(T)));
        }
    }
}