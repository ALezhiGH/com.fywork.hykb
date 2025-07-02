using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.billboard.listener
{
    public abstract class BillBoardPanelListener : AndroidJavaProxy
    {
        public BillBoardPanelListener() : base("com.m3839.sdk.billboard.listener.BillBoardPanelListener") { }


        public void onPanelClose()
        {
            OnPanelClose();
        }

        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        public abstract void OnPanelClose();

        public abstract void OnFailed(int code, string message);
    }
}
