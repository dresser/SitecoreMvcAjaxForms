using System.Web.Mvc;
using Sitecore.Data;
using Sitecore.Diagnostics;
using SitecoreMvcAjaxForms.Models;

namespace SitecoreMvcAjaxForms.Mvc
{
    public class ViewModelModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = base.BindModel(controllerContext, bindingContext);
            if (typeof (IViewModel).IsAssignableFrom(bindingContext.ModelType))
            {
                var viewModel = model as IViewModel;
                Assert.IsNotNull(viewModel, "viewModel must not be null");
                var formDataValue = controllerContext.HttpContext.Request["__AjaxFormData"];
                ShortID dataSourceId;
                if (ShortID.TryParse(formDataValue, out dataSourceId))
                {
                    viewModel.Item = Sitecore.Context.Database.GetItem(dataSourceId.ToID());
                }
            }
            return model;
        }
    }
}