using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Bridge.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynGP = Microsoft.Dexterity.Applications;

namespace TestAddIn
{
    public class GPAddIn : IDexterityAddIn
    {
        public void Initialize()
        {
            var field = DictionaryRoots.Get(0, false).Forms["RM_Customer_Maintenance"].Windows["RM_Customer_Maintenance"].Fields["Customer Number"].Extended();
            field.Change += Field_Change;
            WARN("Registered RM_Customer_Maintenance / Customer Number Change event, Tag: " + field.EventDescriptions.Change.TagId);
        }

        private void Field_Change(object sender, EventArgs e)
        {

            WARN("Hello There - " + sender.GetType());
        }

        private static void WARN(string msg)
        {
            DynGP.Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke(msg);
        }
    }
}
