using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Shell.Applications.ContentEditor.Gutters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.Gutters
{
    public class PublishGutter : Sitecore.Shell.Applications.ContentEditor.Gutters.GutterRenderer
    {
        enum PublishStatus
        {
            Published, NeverPublished, Modified
        }

        private PublishStatus CheckPublishStatus(Item currentItem)
        {
            Database webDb = Factory.GetDatabase("web");
            Item webItem = webDb.GetItem(currentItem.ID);
            if (webItem == null)
                return PublishStatus.NeverPublished;
            if (currentItem["__Revision"] != webItem["__Revision"])
                return PublishStatus.Modified;

            return PublishStatus.Published;
        }

        protected override GutterIconDescriptor GetIconDescriptor(Item item)
        {
            var publishStatus = CheckPublishStatus(item);
            if(publishStatus != PublishStatus.Published)
            {
                GutterIconDescriptor desc = new GutterIconDescriptor();
                if(publishStatus == PublishStatus.NeverPublished)
                {
                    desc.Icon = "Core2/32x32/flag_red_h.png";
                    desc.Tooltip = "Item never published!";
                }
                else
                {
                    desc.Icon = "Core2/32x32/flag_yellow.png";
                    desc.Tooltip = "Item published but modified!";
                }

                desc.Click = $"item:load(id={item.ID})";
                return desc;
            }

            return null;
        }
    }
}