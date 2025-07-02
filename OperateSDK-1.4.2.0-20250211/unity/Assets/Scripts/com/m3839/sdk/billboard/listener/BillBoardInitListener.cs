using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.billboard.listener
{
    public abstract class BillBoardInitListener : AndroidJavaProxy
    {
        public BillBoardInitListener() : base("com.m3839.sdk.billboard.listener.BillBoardInitListener") { }
        

        public void onSucceed()
        {
            OnSuccess();
        }

        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        public abstract void OnSuccess();

        public abstract void OnFailed(int code, string message);
    }
}


