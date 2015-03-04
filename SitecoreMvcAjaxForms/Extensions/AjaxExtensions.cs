using System.Web.Mvc;
using System.Web.Mvc.Html;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using SitecoreMvcAjaxForms.Models;

namespace SitecoreMvcAjaxForms.Extensions
{
    public static class AjaxExtensions
    {
        public static MvcHtmlString FormData(this HtmlHelper htmlHelper, IViewModel model)
        {
            Assert.ArgumentNotNull(model, "model");
            Assert.IsNotNull(model.Item, "IViewModel.Item cannot be null");
            return htmlHelper.Hidden(GetFormDataHiddenFieldName(), GetItemIdValue(model.Item));
        }

        private static string GetFormDataHiddenFieldName()
        {
            return "__AjaxFormData";
        }

        private static string GetItemIdValue(Item item)
        {
            return item.ID.ToShortID().ToString();
        }
    }
}