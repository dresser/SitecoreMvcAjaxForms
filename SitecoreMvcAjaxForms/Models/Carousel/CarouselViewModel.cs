using System.Collections.Generic;
using Sitecore.Data.Items;

namespace SitecoreMvcDemo.Web.Models.Carousel
{
    public class CarouselViewModel
    {
        public IList<Item> CarouselSlideItems { get; set; }
    }
}