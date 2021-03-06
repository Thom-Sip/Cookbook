﻿using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Events;
using Sitecore.Publishing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.Handlers
{
    public class ItemPublishEventHandler
    {

        protected void OnItemSaved(object sender, EventArgs args)
        {
            if ((args != null))
            {
                Item item = Event.ExtractParameter<Item>(args, 0);

                //------ Casting to avoid index, Doesn't work, SaveArgs = null after casting
                //ItemSavedEventArgs saveArgs = args as ItemSavedEventArgs;
                //Item item = saveArgs.Item;

                if (item != null && item.Database.Name == "master")
                {
                    if (item["Auto Publish"] == "1")
                    {
                        //using (new EditContext(item)) ---- Why would you disable this upon save?
                        //{
                        //    item.Fields["Auto Publish"].Value = "0";
                        //}
                        Database[] targetDBs = GetTargetDatabases();
                        PublishManager.PublishItem(item, targetDBs, item.Languages, false, false);
                    }
                }
            }
        }

        private static Database[] GetTargetDatabases()
        {
            Item publishTarget = Client.GetItemNotNull("/sitecore/system/publishing targets");
            List<Database> db = new List<Database>();
            foreach (Item item in publishTarget.Children)
                db.Add(Factory.GetDatabase(item["Target Database"]));
            return db.ToArray();
        }
    }
}