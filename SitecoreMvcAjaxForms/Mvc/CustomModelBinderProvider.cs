using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SitecoreMvcAjaxForms.Mvc
{
    /// <summary>
    /// See http://www.matthidinger.com/archive/2011/08/16/An-inheritance-aware-ModelBinderProvider-in-MVC-3.aspx
    /// </summary>
    public class CustomModelBinderProvider : Dictionary<Type, IModelBinder>, IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            var binders = from binder in this
                where binder.Key.IsAssignableFrom(modelType)
                select binder.Value;

            return binders.FirstOrDefault();
        }
    }
}