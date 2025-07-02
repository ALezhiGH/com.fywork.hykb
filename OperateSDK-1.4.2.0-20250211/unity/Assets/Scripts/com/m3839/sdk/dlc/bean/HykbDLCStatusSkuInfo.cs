using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// DLC商品实体类
/// </summary>
namespace com.m3839.sdk.dlc.bean
{
    public class HykbDLCSkuStatusInfo
    {
        private int skuId;
        private int status;


        /// <summary>
        /// 商品信息封装
        /// </summary>
        public HykbDLCSkuStatusInfo(AndroidJavaObject skuInfo)
        {
            this.skuId = skuInfo.Call<int>("getSkuId");
            this.status = skuInfo.Call<int>("getStatus");
        }

        /// <summary>
        /// 获取商品id
        /// </summary>
        /// <returns>商品id</returns>
        public int getSkuId()
        {
            return skuId;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns>类型字符串</returns>
        public int getStatus()
        {
            return status;
        }


        public string toString()
        {
            return "HykbDLCSkuStatusInfo{" +
                      "skuId='" + skuId + '\'' +
                      ", status='" + status + '\'' +
                      '}';
        }

    }

}