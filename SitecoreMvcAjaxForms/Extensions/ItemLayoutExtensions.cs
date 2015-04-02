using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Layouts;

namespace SitecoreMvcAjaxForms.Extensions
{
    /// <summary>
    /// borrowed from https://markstiles.net/Blog/2011/02/21/sitecore-layout-comparer-extension-classes.aspx
    /// </summary>
    public static class ItemLayoutExtensions
    {
        public static DeviceDefinition DefaultDevice(this Item item, Database database)
        {
            var devices = item.Devices();
            foreach (var device in devices)
            {
                Item deviceItem = database.GetItem(device.ID);
                if (deviceItem[DeviceFieldIDs.Default] == "1")
                {
                    return device;
                }
            }
            return null;
        }

        /// <summary>
        /// This will return a list of Device Definitions on the provided item
        /// </summary>
        public static List<DeviceDefinition> Devices(this Item item)
        {
            LayoutDefinition layout = LayoutDefinition.Parse(item["__Renderings"]);
            return layout.Devices.Cast<DeviceDefinition>().ToList();
        }
    }
}