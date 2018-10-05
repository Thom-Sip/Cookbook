using Cookbook.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookbook.Controllers
{
    public class NavigationController : Controller
    {
        public ActionResult Carousel()
        {
            List<CarouselSlide> slides = new List<CarouselSlide>();
            MultilistField multilistField = Sitecore.Context.Item.Fields["Carousel Slides"];

            if(multilistField != null)
            {
                Item[] carouselItems = multilistField.GetItems();
                foreach(var item in carouselItems)
                {
                    slides.Add(new CarouselSlide(item));
                }
            }

            return PartialView(slides);
        }
    }
}