using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.m3839.sdk.billboard.listener
{
    public abstract class BillBoardUnreadListener : AndroidJavaProxy
    {
        public BillBoardUnreadListener() : base("com.m3839.sdk.billboard.listener.BillBoardUnreadListener") { }


        public void onResult(bool unread)
        {
            OnResult(unread);
        }

        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        public abstract void OnResult(bool unread);

        public abstract void OnFailed(int code, string message);
    }
}

