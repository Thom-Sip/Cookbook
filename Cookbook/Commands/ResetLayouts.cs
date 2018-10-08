using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cookbook.Tasks;
using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;

namespace Cookbook.Commands
{
    public class ResetLayouts : Sitecore.Shell.Framework.Commands.Command
    {
        
        public override void Execute(CommandContext context)
        {
            ResetLayoutDetailsJob job = new ResetLayoutDetailsJob();
            Item item = Sitecore.Configuration.Factory.GetDatabase("master").GetItem("/sitecore/Content/Home");
            job.Run(item);
        }
    }
}