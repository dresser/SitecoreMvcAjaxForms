using System.Collections.Generic;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;
using SitecoreMvcAjaxForms.Extensions;

namespace SitecoreMvcAjaxForms.Models
{
    public class AjaxContainer : RenderingModel
    {
        public ID ComponentsFolderTemplateId = new ID("{74F46967-4043-42BD-A193-369075A1B5BD}");

        public bool PageEditorMode
        {
            get { return Sitecore.Context.PageMode.IsPageEditorEditing; }
        }

        public IList<Item> FormItems
        {
            get
            {
                var componentsFolder =
                    this.Rendering.Item.GetFirstChildInheritingFromTemplate(ComponentsFolderTemplateId);
                if (componentsFolder == null)
                {
                    return new Item[] {};
                }
                return componentsFolder.Children.ToArray();
            }
        }
    }
}