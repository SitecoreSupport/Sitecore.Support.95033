using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Diagnostics;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.EnableDesigning
{
    // Sitecore.ExperienceEditor.Speak.Ribbon.Requests.EnableDesigning.CanDesign
    using Sitecore.Data.Items;
    using Sitecore.ExperienceEditor.Speak.Ribbon.Requests.Common;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.SecurityModel;
    using Sitecore.Web;

    public class CanDesign : ToggleCapabilityRequest
    {
        public override bool GetState()
        {
            if (!(base.RequestContext.WebEditMode != "edit") && WebEditUtility.CanWebEdit() && Policy.IsAllowed("Page Editor/Can Design") && !base.RequestContext.Item.IsFallback)
            {
                Item item = base.RequestContext.Item;
                if (item != null && WebEditUtil.CanDesignItem(item) && HasWriteAccessFinalLayout(item))
                {
                    return !ItemUtility.RequireLockToEdit(item);
                }
                return false;
            }
            return false;
        }

        protected static bool HasWriteAccessFinalLayout(Item item)
        {
            if (WebUtility.IsEditAllVersionsTicked())
                return true;
            Assert.ArgumentNotNull(item, "item");
            return item.Access.CanWrite() && item.Access.CanWriteLanguage();
        }
    }

}