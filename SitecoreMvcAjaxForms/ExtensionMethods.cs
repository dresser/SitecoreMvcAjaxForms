using Sitecore.Data.Items;

namespace SitecoreMvcAjaxForms
{
    public static class ExtensionMethods
    {
        public static string GetMediaUrl(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldName)
        {
            return GetMediaUrl(sitecoreHelper, fieldName, sitecoreHelper.CurrentItem);
        }

        public static string GetMediaUrl(this Sitecore.Mvc.Helpers.SitecoreHelper sitecoreHelper, string fieldName, Item item)
        {
            MediaItem mediaItem = item.Fields[fieldName].Item;
            return Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);
        }
    }
}