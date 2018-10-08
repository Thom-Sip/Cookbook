using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;

namespace Cookbook.Pipelines
{
    public class PageNotFoundResolver : Sitecore.Pipelines.HttpRequest.HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            string filePath =HttpContext.Current.Server.MapPath(args.Url.FilePath);
            if (IsValidItem()
            || args.LocalPath.StartsWith("/sitecore")
            || File.Exists(filePath))
                return;
            Context.Item = Get404Page();
            if (Context.Item != null)
                Sitecore.Context.Items["Is404Page"] = "true";
        }

        protected virtual bool IsValidItem()
        {
            if (Context.Item == null
            || Context.Item.Versions.Count == 0)
                return false;
            if (Context.Item.Visualization.Layout == null)
                return false;
            return true;
        }

        private Item Get404Page()
        {
            string itemPath = Context.Site.StartPath + "/404-Page";
            return Context.Database.GetItem(itemPath);
        }
    }
}