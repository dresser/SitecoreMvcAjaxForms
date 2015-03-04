using System.Web;
using Sitecore.Mvc.Presentation;

namespace SitecoreMvcAjaxForms.Extensions
{
    /// <summary>
    /// See http://stackoverflow.com/questions/15134720/sitecore-dynamic-placeholders-with-mvc
    /// </summary>
    public static class PlaceholderExtensions
    {
        public static HtmlString DynamicPlaceholder(this Sitecore.Mvc.Helpers.SitecoreHelper helper, string dynamicKey)
        {
            var currentRenderingId = RenderingContext.Current.Rendering.UniqueId;
            return helper.Placeholder(string.Format("{0}_{1}", dynamicKey, currentRenderingId));
        }
    }
}