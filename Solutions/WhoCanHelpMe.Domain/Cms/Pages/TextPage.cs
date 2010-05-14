namespace WhoCanHelpMe.Domain.Cms.Pages
{
    #region Using Directives

    using N2;
    using N2.Details;
    using N2.Integrity;

    #endregion

    /// <summary>
    /// The text page definition.
    /// </summary>
    [PageDefinition("Text Page",
        Description = "A text page.",
        SortOrder = 700,
        IconUrl = "~/edit/img/ico/png/page.png")]
    [WithEditableTitle("Page Title", 5, Focus = true, ContainerName = Tabs.Content)]
    [WithEditableName(ContainerName = Tabs.Content)]
    [RestrictParents(typeof(HomePage), typeof(TextPage))]
    public class TextPage : AbstractPage
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