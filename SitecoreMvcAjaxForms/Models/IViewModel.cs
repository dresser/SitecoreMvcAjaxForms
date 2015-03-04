using Sitecore.Data.Items;

namespace SitecoreMvcAjaxForms.Models
{
    public interface IViewModel
    {
        Item Item { get; set; }
    }
}