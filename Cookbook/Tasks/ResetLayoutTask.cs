using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Jobs;
using Sitecore.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.Tasks
{
    public class ResetLayoutTask
    {
        private string _jobName = " Reset Layout Details Job";

        public Job Job
        {
            get { return JobManager.GetJob(_jobName); }
        }

        public void Execute(Item[] items, CommandItem command,ScheduleItem schedule)
        {
            Log.Info("Running Reset latyouts task", this);

            foreach (Item rootItem in items)
            {
                ResetLayoutDetails(rootItem);
            }
        }

        private void ResetLayoutDetails(Item rootItem)
        {
            if (rootItem != null)
            {
                List<Item> itemList = rootItem.Axes.GetDescendants().ToList();
                itemList.Add(rootItem);

                foreach (Item item in itemList)
                {
                    using (new EditContext(item))
                    {
                        item.Fields["__renderings"].Reset();
                    }
                }
            }
        }
    }
}