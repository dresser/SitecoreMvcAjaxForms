using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace SitecoreMvcAjaxForms.Extensions
{
    public static class ItemExtensions
    {
        public static bool InheritsTemplate(this Item item, ID templateId)
        {
            if (item.TemplateID == templateId)
            {
                return true;
            }
            return item.Template.BaseTemplates.Any(t => t.ID == templateId);
        }

        public static Item GetFirstChildInheritingFromTemplate(this Item item, ID templateId)
        {
            Assert.ArgumentNotNull(item, "item");
            if (!item.HasChildren)
            {
                return null;
            }
            return item.Children.ToArray().FirstOrDefault(c => c.InheritsTemplate(templateId));
        }
    }
}