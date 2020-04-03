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
            var w = DictionaryRoots.Get(0, false).Forms["RM_Customer_Maintenance"].Windows["RM_Customer_Maintenance"];
            var window = w.Extended();
            var field = window.Window.Fields["Customer Number"].Extended();
            window.BeforeModalDialog += Window_BeforeModalDialog;
            field.Change += Field_Change;
            WARN("Tag: " + window.EventDescriptions.BeforeModalDialog.TagId);
            WARN("Tag: " + field.EventDescriptions.Change.TagId);
        }

        private void Window_BeforeModalDialog(object sender, BeforeModalDialogEventArgs e)
        {
            WARN("Modal");
        }

        private void Field_Change(object sender, EventArgs e)
        {
            WARN("Field Change");
        }

        private static void WARN(string msg)
        {
            DynGP.Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke(msg);
        }
    }
}
