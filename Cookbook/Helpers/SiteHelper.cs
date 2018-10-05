using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.Helpers
{
    public static class SiteHelper
    {
        public static Item HomeItem()
        {
            string homePath = Sitecore.Context.Site.StartPath;
            return  Sitecore.Context.Database.GetItem(homePath);           
        }

        public static string GetImageUrl(Item item)
        {
            //var currentItem = Sitecore.Context.Item;
            var currentItem = item;
            var imageUrl = string.Empty;

            Sitecore.Data.Fields.ImageField imageField = currentItem.Fields["Image"];
            if (imageField?.MediaItem != null)
            {
                var image = new MediaItem(imageField.MediaItem);
                imageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(image));
            }

            return imageUrl;
        }

        public static string GetImageUrlFromHtmlString(HtmlString htmlString)
        {
            string url = htmlString.ToString();
            var start = url.IndexOf('"', 0);

            if (start == -1)
                return "";

            var end = url.IndexOf('"', start + 1);
            var newurl = url.Substring(start, end - start);
            return url.Substring(start, end - start);
        }
    }
}