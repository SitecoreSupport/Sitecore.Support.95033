using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SecurityModel;
using Sitecore.Shell.Applications.WebEdit.Commands;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;

namespace Sitecore.Support
{
    [Serializable]
    public class NewRendering : WebEditCommand
    {
        // Methods
        public override void Execute(CommandContext context)
        {
        }

        public override string GetClick(CommandContext context, string click) =>
            "javascript:scNewRendering();";

        public override CommandState QueryState(CommandContext context)
        {
            if (!WebEditCommand.CanWebEdit() || !Policy.IsAllowed("Page Editor/Can Design"))
            {
                return CommandState.Hidden;
            }
            if (WebUtil.GetQueryString("mode") != "edit")
            {
                return CommandState.Disabled;
            }
            if ((context.Items.Length > 0) && !WebEditCommand.CanDesignItem(context.Items[0]))
            {
                return CommandState.Disabled;
            }
            string str = "design";
            if (Registry.GetString("/Current_User/Page Editor/Capability/" + str, "on") == "off")
            {
                return CommandState.Disabled;
            }
            // Sets the context item to the variable
            Assert.ArgumentNotNull(context, "context");
            Item item = context.Items[0];
            // Check whether user has permission to Write and Language Write
            if (HasWriteAccess(item))
            {
                if (!item.Access.CanWriteLanguage())
                {
                    return CommandState.Disabled;
                }
            }
            return base.QueryState(context);
        }
        // Check whether user has permission to Write 
        protected static bool HasWriteAccess(Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return item.Access.CanWrite();
        }
    }

}