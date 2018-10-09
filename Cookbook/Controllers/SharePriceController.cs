﻿using Sitecore.Mvc.Presentation;
using Cookbook.Caching;
using System;
using System.Net;
using System.Web.Mvc;

namespace Cookbook.Controllers
{
    public class SharePriceController : Controller
    {
        public ActionResult ShowSharePriceDetails()
        {
            string code = RenderingContext.Current.Rendering.Item["RIC Code"];

            string shareprice = SharePriceCacheManager.GetCache(code);
            if (string.IsNullOrEmpty(shareprice))
            {
                DateTime expiresOn = DateTime.Now.AddMinutes(2);
                shareprice = FetchSharePriceFromExternalSystem(code);
                SharePriceCacheManager.SetCache(code, shareprice, expiresOn);
            }
            
            return Content(shareprice);
        }

        public string FetchSharePriceFromExternalSystem(string code)
        {
            return DateTime.Now.ToString("HH:mm:ss");
			// This is just a dummy URL
            string url = "http://getshareprice.com/code=" + code;
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }
    }
}
