using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.m3839.sdk.dlc.bean;


/// <summary>
/// 快爆DLC批量查询购买状态回调接口
/// </summary>
namespace com.m3839.sdk.dlc.listener
{
    public abstract class HykbDLCQueryAllListener : AndroidJavaProxy
    {

        public HykbDLCQueryAllListener() : base("com.m3839.sdk.dlc.listener.HykbDLCQueryAllListener") { }



        /// <summary>
        /// 批量查询成功
        /// </summary>
        public void onSucceed(AndroidJavaObject data)
        {
            OnSucceed(new HykbDLCSkuStatusData(data));
        }

        /// <summary>
        /// 批量查询失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public void onFailed(int code, string message)
        {
            OnFailed(code, message);
        }

        /// <summary>
        /// 批量查询成功
        /// </summary>
        public abstract void OnSucceed(HykbDLCSkuStatusData data);

        /// <summary>
        /// 批量查询失败
        /// </summary>
        /// <param name="code">返回码</param>
        /// <param name="message"> 错误信息</param>
        public abstract void OnFailed(int code, string message);


    }


}


