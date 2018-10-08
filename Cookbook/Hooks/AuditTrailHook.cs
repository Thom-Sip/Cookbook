﻿using Cookbook.Handlers;
using Sitecore.Events;
using Sitecore.Events.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cookbook.Hooks
{
    public class AuditTrailHook : IHook
    {
        public bool WriteLogs { get; set; }

        public AuditTrailHook(string logActivity)
        {
            WriteLogs = (logActivity == "true");
        }

        public void Initialize()
        {
            AuditTrailEventHandler handler = new AuditTrailEventHandler();
            handler.WriteLogs = WriteLogs;
            Event.Subscribe("item:created", new EventHandler(handler.OnItemCreated));
            Event.Subscribe("item:deleted", new EventHandler(handler.OnItemDeleted));
        }
    }
}