using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;

namespace Cookbook.Commands
{
    public class GetChildCount : Sitecore.Shell.Framework.Commands.Command
    {
        public override void Execute(CommandContext context)
        {
            if(context.Items.Length == 1)
            {
                Item currentItem = context.Items[0];

                var msg = "Count children called";
                Log.Fatal(msg, this);

                SheerResponse.Alert($"Children count: {currentItem.Children.Count}");
            }
        }

        public override CommandState QueryState(CommandContext context)
        {
            if(context.Items.Length == 1)
            {
                Item currentItem = context.Items[0];
                if(currentItem.Children.Count == 0)
                {
                    return CommandState.Hidden;
                }
            }
            return base.QueryState(context);
        }
    }
}