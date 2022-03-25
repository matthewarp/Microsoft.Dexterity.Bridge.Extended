using Microsoft.Dexterity.Applications.DexterityDictionary;
using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Bridge.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAddIn.Properties;
using DynGP = Microsoft.Dexterity.Applications;

namespace TestAddIn
{
    public class GPAddIn : IDexterityAddIn
    { 
        public void Initialize()
        {
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.LoadImportFile.InvokeBeforeOriginal += LoadImportFile_InvokeBeforeOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.LoadImportFile.InvokeAfterOriginal += LoadImportFile_InvokeAfterOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.StringToCustomizationType.InvokeBeforeOriginal += StringToCustomizationType_InvokeBeforeOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.CustomizationStringToType.InvokeBeforeOriginal += CustomizationStringToType_InvokeBeforeOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.CustomizationTypeStringToString.InvokeBeforeOriginal += CustomizationTypeStringToString_InvokeBeforeOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.GetCustomizationImage.InvokeBeforeOriginal += GetCustomizationImage_InvokeBeforeOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Functions.i

            //var dictionary = DictionaryRoots.Get(1, false).Extended();
            //string errors;
            //var result = dictionary.ExecuteSanscript(Resources.PACKAGE, out errors);
            //WARN($"Errors  {result} - {errors}");

            //DynGP.Dexterity.Forms.CustomizationMaintainance.Procedures.AddCustomization.InvokeBeforeOriginal += AddCustomization_InvokeBeforeOriginal;
            //DynGP.Dexterity.Forms.CustomizationMaintainance.Procedures.AddCustomization.InvokeAfterOriginal += AddCustomization_InvokeAfterOriginal;

            //var dictionary = DictionaryRoots.Get(0, false);

            //var formId = dictionary.Forms["RM_Customer_Maintenance"].Extended().Id; // formId: 390
            //var windowId = dictionary.Forms["RM_Customer_Maintenance"].Windows["RM_Customer_Maintenance"].Extended().Id; // windowId: 1

            //var window = dictionary.Forms["RM_Customer_Maintenance"].Windows["RM_Customer_Maintenance"].Extended();
            //var field = window.Window.Fields["Customer Number"].Extended();
            //var proc = dictionary.Procedures["Messenger_Status"].Extended();
            //var func = dictionary.Functions["Security"].Extended();

            //window.BeforeModalDialog += Window_BeforeModalDialog;

            //field.Change += Field_Change1;
            //field.Change += Field_Change2;

            //field.ClickAfterOriginal += Field_ClickAfterOriginal; // Adding an event that isn't available does nothing

            //proc.AfterInvoke += Proc_AfterInvoke;
            //func.AfterInvoke += Func_AfterInvoke; //Adding an event to an un-Supported Func/Proc does nothing

            //WARN("Window Tag: " + string.Join(",",window.EventDescriptions.BeforeModalDialog.TagIds));
            //WARN("Field Tag: " + string.Join(",", field.EventDescriptions.Change.TagIds));
            //WARN("Proc Tag: " + string.Join(",", proc.EventDescriptions.AfterInvoke.TagIds));
            //WARN("Func Tag: " + string.Join(",", func.EventDescriptions.AfterInvoke.TagIds));

            //field.Change -= Field_Change2; // Removing a handler will remove the Tag from Dexterity and remove the handler reference

            //WARN("Field Tag: " + string.Join(",", field.EventDescriptions.Change.TagIds));
        }

        private void GetCustomizationImage_InvokeBeforeOriginal(object sender, CustomizationMaintainanceForm.GetCustomizationImageFunction.InvokeEventArgs e)
        {
            WARN($"GetCustomizationImage {e.inParam1} - {e.result}");
        }

        private void CustomizationTypeStringToString_InvokeBeforeOriginal(object sender, CustomizationMaintainanceForm.CustomizationTypeStringToStringFunction.InvokeEventArgs e)
        {
            WARN($"CustomizationTypeStringToString {e.inParam1} - {e.result}");
        }

        private void CustomizationStringToType_InvokeBeforeOriginal(object sender, CustomizationMaintainanceForm.CustomizationStringToTypeFunction.InvokeEventArgs e)
        {
            WARN($"CustomizationStringToType {e.inParam1} - {e.result}");
        }

        private void StringToCustomizationType_InvokeBeforeOriginal(object sender, CustomizationMaintainanceForm.StringToCustomizationTypeFunction.InvokeEventArgs e)
        {
            WARN($"StringToCustomizationType {e.inParam1} = {e.result}");
        }

        private void AddCustomization_InvokeAfterOriginal(object sender, CustomizationMaintainanceForm.AddCustomizationProcedure.InvokeEventArgs e)
        {
            WARN($"AddCustomizationAfter: {e.inParam2} - {e.inParam1}");
        }

        private void AddCustomization_InvokeBeforeOriginal(object sender, CustomizationMaintainanceForm.AddCustomizationProcedure.InvokeEventArgs e)
        {
            WARN($"AddCustomizationBefore: {e.inParam2}");
        }

        private void LoadImportFile_InvokeAfterOriginal(object sender, CustomizationMaintainanceForm.LoadImportFileFunction.InvokeEventArgs e)
        {
            WARN($"After: {e.inParam1} - {e.result}");
        }

        private void LoadImportFile_InvokeBeforeOriginal(object sender, CustomizationMaintainanceForm.LoadImportFileFunction.InvokeEventArgs e)
        {
            WARN($"Before: {e.inParam1}");
        }

        private void Field_ClickAfterOriginal(object sender, EventArgs e) { throw new NotImplementedException(); }

        private void Func_AfterInvoke(object sender, ScriptEventArgs e)
        {
            //WARN(FormatArguments(e.Arguments));
        }

        private void Proc_AfterInvoke(object sender, ScriptEventArgs e)
        {
            //WARN(FormatArguments(e.Arguments));
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
