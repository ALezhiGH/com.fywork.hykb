using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆DLC兑换商品回调接口
/// </summary>
namespace com.m3839.sdk.dlc.listener
{
    public abstract class HykbDLCExchangeListener : AndroidJavaProxy
    {

        public HykbDLCExchangeListener() : base("com.m3839.sdk.dlc.listener.HykbDLCExchangeListener") { }

        public void onSucceed(int ok)
        {
            OnSucceed(ok);
        }

        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 兑换商品成功
        /// </summary>
        public abstract void OnSucceed(int ok);

        /// <summary>
        /// 兑换商品失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);
    }
}
