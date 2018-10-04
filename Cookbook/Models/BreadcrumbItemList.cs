using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace Cookbook.Models
{
    public class BreadcrumbItemList : Sitecore.Mvc.Presentation.RenderingModel
    {
        public List<BreadcrumbItem> Breadcrumbs { get; set; }

        public override void Initialize(Rendering rendering)
        {
            Breadcrumbs = new List<BreadcrumbItem>();
            List<Item> items = GetBreadcrumbItems();

            foreach(var item in items)
            {
                Breadcrumbs.Add(new BreadcrumbItem(item));
            }
            Breadcrumbs.Add(new BreadcrumbItem(Sitecore.Context.Item));

            base.Initialize(rendering);
        }

        private List<Sitecore.Data.Items.Item> GetBreadcrumbItems()
        {
            string homepath = Sitecore.Context.Site.StartPath;
            Item HomeItem = Sitecore.Context.Database.GetItem(homepath);
            List<Item> items = Sitecore.Context.Item.Axes.GetAncestors()
                .SkipWhile(x => x.ID != HomeItem.ID)
                .ToList();

            return items;
        }
    }
}