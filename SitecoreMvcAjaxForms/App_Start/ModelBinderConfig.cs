using System.Web.Mvc;
using SitecoreMvcAjaxForms.Models;
using SitecoreMvcAjaxForms.Mvc;

namespace SitecoreMvcAjaxForms
{
    public class ModelBinderConfig
    {
        public static void RegisterModelBinderProviders(ModelBinderProviderCollection binderProviders)
        {
            binderProviders.Add(new CustomModelBinderProvider
            {
                {typeof (IViewModel), new ViewModelModelBinder()}
            });
        }
    }
}