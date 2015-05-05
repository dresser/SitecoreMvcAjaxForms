using System;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace SitecoreMvcAjaxPlaceholder.Controllers
{
    public class AjaxPlaceholderController : Controller
    {
        [HttpGet]
        public ActionResult GetPlaceholderRenderings(Guid pageItemId, Guid deviceId, Guid referenceId, Guid renderingId)
        {
            Assert.ArgumentNotNull(pageItemId, "pageItemId");
            Assert.ArgumentNotNull(deviceId, "deviceId");
            var db = Sitecore.Configuration.Factory.GetDatabase("master");
            var item = db.GetItem(new ID(pageItemId));
            Assert.IsNotNull(item, "Item with ID {0} not found.", pageItemId);

            DeviceItem deviceItem = db.GetItem(new ID(deviceId));
            var allRenderings = item.Visualization.GetRenderings(deviceItem, checkLogin: false);

            var idReferenceId = new ID(referenceId);

            string placeholder =
                allRenderings.Where(r => ID.Parse(r.UniqueId).Equals(idReferenceId))
                    .Select(r => r.Placeholder)
                    .FirstOrDefault();

            Assert.IsNotNull(placeholder, "Placeholder '{0}' could not be found.", placeholder);

            var placeholderRenderings =
                allRenderings.Where(r => r.Placeholder.Equals(placeholder, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

            var selectedReferenceId = GetRenderingReferenceId();
            if (selectedReferenceId == ID.Null)
            {
                selectedReferenceId = ID.Parse(placeholderRenderings.First().UniqueId);
            }

            var renderings = placeholderRenderings.Select(r => new
                {
                    itemId = r.UniqueId,
                    id = r.UniqueId,
                    renderingId = r.RenderingItem.ID.ToString(),
                    renderingIcon = r.RenderingItem.Icon,
                    renderingName = r.RenderingItem.DisplayName,
                    placeholder = r.Placeholder,
                    selected = ID.Parse(r.UniqueId).Equals(selectedReferenceId)
                });
            return Json(renderings, JsonRequestBehavior.AllowGet);
        }

        private ID GetRenderingReferenceId()
        {
            var renderingReferenceId = ControllerContext.HttpContext.Session["RenderingReferenceId"];
            if (renderingReferenceId != null)
            {
                return new ID((Guid) renderingReferenceId);
            }
            return ID.Null;
        }

        [HttpGet]
        public ActionResult GetSelectedRendering()
        {
            return Content(GetRenderingReferenceId().ToString());
        }

        [HttpPost]
        public ActionResult SetSelectedRendering(Guid renderingReferenceId)
        {
            ControllerContext.HttpContext.Session["RenderingReferenceId"] = renderingReferenceId;
            return null;
        }
    }
}