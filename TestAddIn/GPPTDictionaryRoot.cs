using Microsoft.Dexterity.Bridge;
using Microsoft.Dexterity.Bridge.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAddIn
{
    public static class GPPTDictionaryRoot
    {
        private static readonly DateTime ZeroDate = new DateTime(1900, 01, 01);

        #region Getters
        public static short GetWindowValueModifiedBoolean(short nProdID, string sFormName, string sWinName, string sFieldName, ref bool fieldValue, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryGetValue(out fieldValue, out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short GetWindowValueModifiedDate(short nProdID, string sFormName, string sWinName, string sFieldName, ref DateTime fieldValue, out string results)
            => GetWindowValueModifiedDateTime(nProdID, sFormName, sWinName, sFieldName, ref fieldValue, out results);

        private static short GetWindowValueModifiedDateTime(short nProdID, string sFormName, string sWinName, string sFieldName, ref DateTime fieldValue, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryGetValue(out fieldValue, out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short GetWindowValueModifiedNumeric(short nProdID, string sFormName, string sWinName, string sFieldName, ref decimal fieldValue, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryGetValue(out object rawValue, out Result res))
                {
                    fieldValue = decimal.Parse(rawValue.ToString());
                    nStatus = 0;
                }
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short GetWindowValueModifiedString(short nProdID, string sFormName, string sWinName, string sFieldName, ref string fieldValue, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryGetValue(out fieldValue, out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }
            
            return nStatus;
        }

        public static short GetWindowValueModifiedTime(short nProdID, string sFormName, string sWinName, string sFieldName, ref DateTime fieldValue, out string results)
            => GetWindowValueModifiedDateTime(nProdID, sFormName, sWinName, sFieldName, ref fieldValue, out results);
        #endregion

        #region Setters
        public static short SetWindowValueModifiedBoolean(short nProdID, string sFormName, string sWinName, string sFieldName, bool fieldValue, bool runValidate, out string results)
            => SetWindowValueModified(nProdID, sFormName, sWinName, sFieldName, fieldValue, runValidate, out results);

        public static short SetWindowValueModifiedDate(short nProdID, string sFormName, string sWinName, string sFieldName, DateTime fieldValue, bool runValidate, out string results)
            => SetWindowValueModifiedDateTime(nProdID, sFormName, sWinName, sFieldName, fieldValue.Date, runValidate, out results);

        private static short SetWindowValueModifiedDateTime(short nProdID, string sFormName, string sWinName, string sFieldName, DateTime fieldValue, bool runValidate, out string results)
            => SetWindowValueModified(nProdID, sFormName, sWinName, sFieldName, fieldValue, runValidate, out results);

        public static short SetWindowValueModifiedNumeric(short nProdID, string sFormName, string sWinName, string sFieldName, decimal fieldValue, bool runValidate, out string results)
        {
            short nStatus = 0;
            results = string.Empty;

            try
            {
                object value = new DictionaryRoot(nProdID, true).Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().Value;

                if (value is short)
                    nStatus = SetWindowValueModified(nProdID, sFormName, sWinName, sFieldName, (short)fieldValue, runValidate, out results);
                else if (value is int)
                    nStatus = SetWindowValueModified(nProdID, sFormName, sWinName, sFieldName, (int)fieldValue, runValidate, out results);
                else
                    nStatus = SetWindowValueModified(nProdID, sFormName, sWinName, sFieldName, fieldValue, runValidate, out results);
            }
            catch(Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short SetWindowValueModifiedString(short nProdID, string sFormName, string sWinName, string sFieldName, string fieldValue, bool runValidate, out string results)
            => SetWindowValueModified(nProdID, sFormName, sWinName, sFieldName, fieldValue, runValidate, out results);

        public static short SetWindowValueModifiedTime(short nProdID, string sFormName, string sWinName, string sFieldName, DateTime fieldValue, bool runValidate, out string results)
            => SetWindowValueModifiedDateTime(nProdID, sFormName, sWinName, sFieldName, ZeroDate + fieldValue.TimeOfDay, runValidate, out results);

        private static short SetWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, object fieldValue, bool runValidate, out string results)
        {
            DictionaryRoot cRoot;
            FieldBase fieldBase;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                fieldBase = cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName];

                // Read Field Data
                if (fieldBase.Extended().TrySetValue(fieldValue, runValidate, out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }
        #endregion

        #region Actions
        public static short RunValidateWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryRunValidate(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short EnableWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryEnable(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short DisableWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryDisable(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short LockWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryLock(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short UnlockWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryUnlock(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short ShowWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryShow(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short HideWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryHide(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }

        public static short FocusWindowValueModified(short nProdID, string sFormName, string sWinName, string sFieldName, out string results)
        {
            DictionaryRoot cRoot;
            short nStatus = 0;
            results = string.Empty;

            try
            {
                // First parameter is product ID, Second parameter says it is Modified Forms Dictionary
                cRoot = new DictionaryRoot(nProdID, true);

                // Define Field of window of form
                if (cRoot.Forms[sFormName].Windows[sWinName].Fields[sFieldName].Extended().TryFocus(out Result res))
                    nStatus = 0;
                else
                {
                    results = res.Message;
                    nStatus = 1;
                }
            }
            catch (Exception ex)
            {
                results = ex.Message;
                nStatus = 1;
            }

            return nStatus;
        }
        #endregion
    }
}
