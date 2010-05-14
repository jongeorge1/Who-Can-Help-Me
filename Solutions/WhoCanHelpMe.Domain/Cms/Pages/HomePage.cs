namespace WhoCanHelpMe.Domain.Cms.Pages
{
    #region Using Directives

    using N2;
    using N2.Details;
    using N2.Installation;
    using N2.Integrity;
    using N2.Web.UI;

    #endregion

    /// <summary>
    /// The home page definition.
    /// </summary>
    [PageDefinition("Home Page",
        Description = "A home page template.",
        SortOrder = 440,
        InstallerVisibility = InstallerHint.PreferredRootPage | InstallerHint.PreferredStartPage,
        IconUrl = "~/edit/img/ico/png/page_world.png")]
    [WithEditableTitle("Title", 5, Focus = true, ContainerName = Tabs.Content)]
    [RestrictParents(typeof(SiteRoot))]
    public class HomePage : AbstractPage
    {
        /// <summary>
        /// Gets or sets BodyText.
        /// </summary>
        /// <value>
        /// The body text.
        /// </value>
        [EditableFreeTextAreaAttribute("Body Text", 100, ContainerName = Tabs.Content,
            HelpText = "Set the body text for the page")]
        public string BodyText
        {
            get { return (string)GetDetail("BodyText"); }
            set { SetDetail("BodyText", value); }
        }  
    }
}