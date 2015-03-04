using System.ComponentModel;

namespace SitecoreMvcAjaxForms.Models
{
    public class ViewModelBase: IViewModel
    {
        [ReadOnly(true)]
        public Sitecore.Data.Items.Item Item { get; set; }
    }
}