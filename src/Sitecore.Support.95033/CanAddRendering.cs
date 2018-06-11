using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.AddRendering
{
    // Sitecore.ExperienceEditor.Speak.Ribbon.Requests.AddRendering.CanAddRendering
    using Sitecore.ExperienceEditor;
    using Sitecore.ExperienceEditor.Speak.Server.Contexts;
    using Sitecore.ExperienceEditor.Speak.Server.Requests;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.SecurityModel;
    using Sitecore.Web;
    using Sitecore.Web.UI.HtmlControls;

    public class CanAddRendering : PipelineProcessorControlStateRequest<ItemContext>
    {
        public override bool GetControlState()
        {
            if (!base.RequestContext.Site.EnableWebEdit | !Policy.IsAllowed("Page Editor/Can Design"))
            {
                return false;
            }
            base.RequestContext.ValidateContextItem();
            if (!(base.RequestContext.WebEditMode != "edit") && HasWriteAccessFinalLayout(base.RequestContext.Item) && WebEditUtil.CanDesignItem(base.RequestContext.Item) && !base.RequestContext.Item.IsFallback && !ItemUtility.RequireLockToEdit(base.RequestContext.Item))
            {
                string str = "design";
                string @string = Registry.GetString("/Current_User/Page Editor/Capability/" + str, Constants.Registry.CheckboxTickedRegistryValue);
                if (@string == Constants.Registry.CheckboxUnTickedRegistryValue)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        protected static bool HasWriteAccessFinalLayout(Item item)
        {
            if (WebUtility.IsEditAllVersionsTicked())
                return true;
            Assert.ArgumentNotNull(item, "item");
            return item.Access.CanWrite()&&item.Access.CanWriteLanguage();
        }

    }

}