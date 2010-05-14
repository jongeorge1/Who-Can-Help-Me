namespace WhoCanHelpMe.Web.Views
{
    #region Using Directives

    using System.Collections.Generic;
    using MvcContrib.FluentHtml;
    using MvcContrib.FluentHtml.Behaviors;
    using Spark.Web.Mvc;

    #endregion

    public abstract class SparkModelViewPage<T> : SparkView<T>, IViewModelContainer<T> where T : class
    {
        private readonly List<IBehaviorMarker> behaviors = new List<IBehaviorMarker>();

        protected SparkModelViewPage()
        {
            this.behaviors.Add(new ValidationBehavior(() => ViewData.ModelState));
        }

        public IEnumerable<IBehaviorMarker> Behaviors
        {
            get { return this.behaviors; }
        }

        public string HtmlNamePrefix { get; set; }

        public T ViewModel
        {
            get { return Model; }
        }
    }
}