using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.Publishing
{
    public class RemoveDuplicateItems : PublishItemProcessor
    {
        public override void Process(PublishItemContext context)
        {
            Item sourceItem = context.PublishHelper.GetSourceItem(context.ItemId);

            if (sourceItem != null)
            {
                Item targetItem = context.PublishOptions.TargetDatabase.GetItem(sourceItem.Paths.Path);
                if (targetItem != null && targetItem.ID != sourceItem.ID)
                {
                    context.PublishHelper.DeleteTargetItem(targetItem.ID);
                    Log.Info("Deleted duplicate item: " + targetItem.Paths.Path, this);
                }
            }
        }
    }
}