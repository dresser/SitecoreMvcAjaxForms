using System.IO;
using System.Web;
using System.Web.Mvc;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Common;
using Sitecore.Mvc.Helpers;
using Sitecore.Mvc.Pipelines;
using Sitecore.Mvc.Pipelines.Response.RenderPlaceholder;

namespace SitecoreMvcAjaxPlaceholder.Mvc.Helpers
{
    public class SitecoreAjaxHelper : SitecoreHelper
    {
        public SitecoreAjaxHelper(HtmlHelper htmlHelper) : base(htmlHelper)
        {
        }

        public HtmlString AjaxPlaceholder(string placeholderName)
        {
            Assert.ArgumentNotNull((object)placeholderName, "placeholderName");
            using (ContextService.Get().Push<ViewContext>(base.HtmlHelper.ViewContext))
            {
                StringWriter stringWriter = new StringWriter();
                PipelineService.Get().RunPipeline<RenderPlaceholderArgs>("mvc.renderAjaxPlaceholder", new RenderPlaceholderArgs(placeholderName, (TextWriter)stringWriter, this.CurrentRendering));
                return new HtmlString(stringWriter.ToString());
            }
        }
    }
}