using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return base.QueryState(context);
        }
    }

}