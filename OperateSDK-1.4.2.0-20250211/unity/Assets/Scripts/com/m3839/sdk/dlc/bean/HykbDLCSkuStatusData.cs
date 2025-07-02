using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 快爆DLC查询购买状态实体类
/// </summary>
namespace com.m3839.sdk.dlc.bean
{
    public class HykbDLCSkuStatusData
    {
        // 商品列表
        private List<HykbDLCSkuStatusInfo> list = new List<HykbDLCSkuStatusInfo>();

        /// <summary>
        /// 商品列表包装
        /// </summary>
        /// <param name="data">DLC商品列表对象</param>
        public HykbDLCSkuStatusData(AndroidJavaObject data) {

            AndroidJavaObject listObject = data.Call<AndroidJavaObject>("getList");
            int size = listObject.Call<int>("size");
            for(int i = 0; i<size; i++) 
            {
                HykbDLCSkuStatusInfo info = new HykbDLCSkuStatusInfo(listObject.Call<AndroidJavaObject>("get", i));
                list.Add(info);
            }
        }

        public List<HykbDLCSkuStatusInfo> getList() 
        {
            return list;
        }







    }
}

