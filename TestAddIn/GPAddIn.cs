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
            var w = DictionaryRoots.Get(0, false).Forms["RM_Customer_Maintenance"].Windows["RM_Customer_Maintenance"];
            var window = w.Extended();
            var field = window.Window.Fields["Customer Number"].Extended();
            window.BeforeModalDialog += Window_BeforeModalDialog;
            field.Change += Field_Change;
            WARN("Tag: " + window.EventDescriptions.BeforeModalDialog.TagId);
            WARN("Tag: " + field.EventDescriptions.Change.TagId);

            handler = new Action<object, Argument[]>(Procedure_Event);

            var proc = DictionaryRoots.Get(0, false).Procedures["Security"];
            var func = DictionaryRoots.Get(0, false).Functions["Security"];

            TriggerManager.ProcedureTriggers.RegisterTrigger(new WrappedProcedure(proc), proc, AttachType.After, handler);
            TriggerManager.FunctionTriggers.RegisterTrigger(new WrappedFunction(func), func, AttachType.After, handler);
        }

        private void Procedure_Event(object sender, Argument[] args)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                str.AppendLine($"[({i})]:{args[0].Value}");
            }
        }

        private void Security_InvokeAfterOriginal(object sender, Argument[] args)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                str.AppendLine($"[({i})]:{args[0].Value}");
            }
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

    public class WrappedProcedure : ProcedureBase
    {
        protected override Script WrappedScript { get; }

        public WrappedProcedure(Script wrappedScript)
        {
            WrappedScript = wrappedScript ?? throw new ArgumentNullException(nameof(wrappedScript));
        }

        protected override void FireInvokeEvent(Argument[] arguments, Delegate handler)
        {
            handler.DynamicInvoke(this, arguments);
        }
    }
}
