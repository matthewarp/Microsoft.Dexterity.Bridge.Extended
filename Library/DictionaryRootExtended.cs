﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dexterity.Bridge.Extended
{
    internal delegate short SetParamHandler(object callbackObject);

    public class DictionaryRootExtended
    {
        public DictionaryRoot Dictionary { get; }

        private readonly IAppDispatch dispatch;

        private readonly SetParamHandler setParamHandler;

        private readonly object controller, appProxy;

        private static readonly PropertyInfo controllerInfo;
        private static readonly FieldInfo appProxyInfo, appDispatchInfo;
        private static readonly MethodInfo setParaHandlerInfo;

        static DictionaryRootExtended()
        {
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            controllerInfo = typeof(DictionaryRoot).GetProperty("Controller", flags);
            appProxyInfo = controllerInfo.PropertyType.GetField("_appProxy", flags);
            appDispatchInfo = appProxyInfo.FieldType.GetField("_appDispatch", flags);
            setParaHandlerInfo = appProxyInfo.FieldType.GetMethod("SetParamHandler");
        }

        internal DictionaryRootExtended(DictionaryRoot dic)
        {
            Dictionary = dic ?? throw new ArgumentNullException(nameof(dic));

            controller = controllerInfo.GetValue(dic);
            appProxy = appProxyInfo.GetValue(controller);
            dispatch = (IAppDispatch)appDispatchInfo.GetValue(appProxy);
            setParamHandler = (SetParamHandler)setParaHandlerInfo.CreateDelegate(typeof(SetParamHandler), appProxy);
        }

        public short LastRegisteredEvent()
        {
            ParameterHandler handler = new ParameterHandler();
            setParamHandler(handler);
            ExecuteSanscript("local integer l_tag; system 5156, l_tag; OLE_SetProperty(\"Result\", str(l_tag));", out _);

            return short.Parse(handler.Result);
        }

        public void UnregisterTagId(short tagId)
        {
            dispatch.UnregisterTrigger(tagId);
        }

        public void SetParamHandler(object handler)
        {
            setParamHandler(handler);
        }

        public unsafe short ExecuteSanscript(string codeString, out string compilerErrorMessage)
        {
            try
            {
                char* pointer = (char*)Marshal.StringToBSTR(codeString).ToPointer();
                char* chPtr;
                short num = dispatch.ExecuteSanscript(pointer, &chPtr);
                IntPtr ptr = (IntPtr)((void*)chPtr);
                compilerErrorMessage = Marshal.PtrToStringBSTR(ptr);
                Marshal.FreeBSTR((IntPtr)((void*)pointer));
                Marshal.FreeBSTR((IntPtr)((void*)chPtr));
                return num;
            }catch(Exception ex)
            {
                compilerErrorMessage = ex.Message;
                return -1;
            }
        }
    }
}
