using Cookbook.Models;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.Links;
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

        public ActionResult LanguageSwitcher()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            LanguageCollection langColl = LanguageManager.GetLanguages(Context.Database);

            foreach(var language in langColl)
            {
                string url = GetItemUrl(Context.Item, language);
                list.Add(language.Title, url);
            }

            return View(list);
        }

        public string GetItemUrl(Item item, Language language)
        {
            string url = LinkManager.GetItemUrl(item, 
                new UrlOptions
                {
                    LanguageEmbedding = LanguageEmbedding.Always,
                    LanguageLocation = LanguageLocation.FilePath,
                    Language = language
                });

            return url;
        }
    }
}