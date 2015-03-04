using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitecoreMvcDemo.Web.Models.Carousel;

namespace SitecoreMvcDemo.Web.Controllers
{
    public class CarouselController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var item = Sitecore.Mvc.Presentation.RenderingContext.Current.Rendering.Item;
            var slideIds = Sitecore.Data.ID.ParseArray(item["SelectedItems"]);
            var viewModel = new CarouselViewModel
            {
                CarouselSlideItems = slideIds.Select(i => item.Database.GetItem(i)).ToList()
            };
            return View("~/Views/Renderings/Carousel/Carousel.cshtml", viewModel);
        }
    }
}
