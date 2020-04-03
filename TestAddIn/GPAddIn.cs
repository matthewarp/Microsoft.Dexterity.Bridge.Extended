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

        Delegate handler;

        public void Initialize()
        {
            var dictionary = DictionaryRoots.Get(0, false);
            var window = dictionary.Forms["RM_Customer_Maintenance"].Windows["RM_Customer_Maintenance"].Extended();
            var field = window.Window.Fields["Customer Number"].Extended();
            var proc = dictionary.Procedures["Security"].Extended();
            var func = dictionary.Functions["Security"].Extended();

            window.BeforeModalDialog += Window_BeforeModalDialog;

            field.Change += Field_Change1;
            field.Change += Field_Change2;

            proc.AfterInvoke += Proc_AfterInvoke;
            func.AfterInvoke += Func_AfterInvoke;

            WARN("Window Tag: " + string.Join(",",window.EventDescriptions.BeforeModalDialog.TagIds));
            WARN("Field Tag: " + string.Join(",", field.EventDescriptions.Change.TagIds));
            WARN("Proc Tag: " + string.Join(",", proc.EventDescriptions.AfterInvoke.TagIds));
            WARN("Func Tag: " + string.Join(",", func.EventDescriptions.AfterInvoke.TagIds));

            field.Change -= Field_Change2;

            WARN("Field Tag: " + string.Join(",", field.EventDescriptions.Change.TagIds));
        }

        private void Func_AfterInvoke(object sender, ScriptEventArgs e)
        {
            WARN(FormatArguments(e.Arguments));
        }

        private void Proc_AfterInvoke(object sender, ScriptEventArgs e)
        {
            WARN(FormatArguments(e.Arguments));
        }

        private void Window_BeforeModalDialog(object sender, BeforeModalDialogEventArgs e)
        {
            WARN("Modal");
        }

        private void Field_Change1(object sender, EventArgs e)
        {
            WARN("Field Change1");
        }

        private void Field_Change2(object sender, EventArgs e)
        {
            WARN("Field Change1");
        }

        private static string FormatArguments(Argument[] args)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                str.AppendLine($"[({i})]:{args[0].Value}");
            }

            return str.ToString();
        }


        private static void WARN(string msg)
        {
            DynGP.Dynamics.Forms.SyVisualStudioHelper.Functions.DexWarning.Invoke(msg);
        }
    }
}
