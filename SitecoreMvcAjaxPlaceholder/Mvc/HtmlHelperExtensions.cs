using System.Web.Mvc;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Helpers;
using SitecoreMvcAjaxPlaceholder.Mvc.Helpers;

namespace SitecoreMvcAjaxPlaceholder.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static SitecoreAjaxHelper SitecoreAjax(this HtmlHelper htmlHelper)
        {
            Assert.ArgumentNotNull((object) htmlHelper, "htmlHelper");
            SitecoreAjaxHelper threadData = ThreadHelper.GetThreadData<SitecoreAjaxHelper>();
            if (threadData != null)
                return threadData;
            SitecoreAjaxHelper data = new SitecoreAjaxHelper(htmlHelper);
            ThreadHelper.SetThreadData<SitecoreAjaxHelper>(data);
            return data;
        }
    }
}