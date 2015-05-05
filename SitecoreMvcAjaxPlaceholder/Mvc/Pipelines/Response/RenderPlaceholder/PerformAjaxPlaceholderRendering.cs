using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Mvc.Pipelines.Response.RenderPlaceholder;
using Sitecore.Mvc.Presentation;

namespace SitecoreMvcAjaxPlaceholder.Mvc.Pipelines.Response.RenderPlaceholder
{
    public class PerformAjaxPlaceholderRendering : PerformRendering
    {
        protected override IEnumerable<Rendering> GetRenderings(string placeholderName, RenderPlaceholderArgs args)
        {
            var renderings = base.GetRenderings(placeholderName, args);
            if (Sitecore.Context.PageMode.IsPageEditorEditing)
            {
                var pageEditorSelectedRendering = GetPageEditorRendering(renderings);
                if (pageEditorSelectedRendering != null)
                {
                    return new Rendering[] {pageEditorSelectedRendering};
                }
            }
            var rendering = renderings.FirstOrDefault();
            if (rendering == null)
            {
                return new Rendering[] {};
            }
            return new Rendering[] {rendering};
        }

        protected Rendering GetPageEditorRendering(IEnumerable<Rendering> renderings)
        {
            if (HttpContext.Current.Session["RenderingReferenceId"] == null)
            {
                return null;
            }
            var renderingId = (Guid)HttpContext.Current.Session["RenderingReferenceId"];
            return renderings.FirstOrDefault(r => r.UniqueId == renderingId);
        }
    }
}