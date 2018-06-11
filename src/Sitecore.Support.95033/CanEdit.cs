using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Support.ExperienceEditor.Speak.Ribbon.Requests.EnableEditing
{
    // Sitecore.ExperienceEditor.Speak.Ribbon.Requests.EnableEditing.CanEdit
    using Sitecore.Data.Items;
    using Sitecore.ExperienceEditor.Speak.Ribbon.Requests.Common;
    using Sitecore.ExperienceEditor.Utils;
    using Sitecore.SecurityModel;

    public class CanEdit : ToggleCapabilityRequest
    {
        public override bool GetState()
        {
            if (base.RequestContext.WebEditMode != "edit")
            {
                return false;
            }
            Item item = base.RequestContext.Item;
            bool flag = item != null && item.Access.CanWrite() && item.Access.CanWriteLanguage();
            bool flag2 = WebEditUtility.CanWebEdit() && Policy.IsAllowed("Page Editor/Can Edit") && !ItemUtility.RequireLockToEdit(item);
            if (flag && flag2)
            {
                return !item.IsFallback;
            }
            return false;
        }
    }

}