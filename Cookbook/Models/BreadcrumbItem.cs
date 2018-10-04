using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Cookbook.Models
{
    public class BreadcrumbItem : CustomItem
    {
        public BreadcrumbItem(Item item) : base(item)
        {
            Assert.IsNotNull(item, "item");
        }

        public string Title
        {
            get { return InnerItem["Title"]; }
        }

        public bool IsActive
        {
            get { return Sitecore.Context.Item.ID == InnerItem.ID; }
        }

        public string Url
        {
            get { return Sitecore.Links.LinkManager.GetItemUrl(InnerItem); }
        }
    }
}