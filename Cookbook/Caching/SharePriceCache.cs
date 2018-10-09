using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Caching;

namespace Cookbook.Caching
{
    public class SharePriceCache : Sitecore.Caching.CustomCache
    {
        public SharePriceCache(ICache innerCache) : base(innerCache)
        {
        }

        public SharePriceCache(string name, long maxSize) : base(name, maxSize)
        {
        }

        new public void SetString(string key, string value, DateTime expiry)
        {
            base.SetString(key, value, expiry);
        }
        new public string GetString(string key)
        {
            return base.GetString(key);
        }
    }
}