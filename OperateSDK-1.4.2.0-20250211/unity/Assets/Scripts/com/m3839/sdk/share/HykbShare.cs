using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 好游快爆SDK
/// </summary>
namespace com.m3839.sdk.share
{
    /// <summary>
    /// 好游快爆SDK的静态方法包装，用于untiy与安卓SDK对接及交互。
    /// </summary>
    public class HykbShare
    {
        // 单例中间层对象，方便获取
        private static AndroidJavaClass sPayJavaClass;
        public static AndroidJavaClass getPayClass()
        {
            if (sPayJavaClass == null)
            {
                sPayJavaClass = new AndroidJavaClass("com.m3839.sdk.share.HykbShare");
            }
            return sPayJavaClass;
        }

        /// <summary>
        /// 调用分享
        /// </summary>
        /// <param name="id">论坛id</param>
        /// <param name="content">帖子内容</param>
        public static void Share(string id,string content, List<string> list)
        {
            getPayClass().CallStatic("share", HykbContext.GetInstance().GetActivity(), id, content, ConvertStringListToJavaList (list));
        }

        private static AndroidJavaObject ConvertStringListToJavaList(List<string> list)
        {
            AndroidJavaObject javaList = new AndroidJavaObject("java.util.ArrayList");
            foreach(string str in list)
            {
                javaList.Call<bool>("add", str);
            }
            return javaList;
        }
    }
}
