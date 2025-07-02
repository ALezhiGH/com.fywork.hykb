using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.billboard
{
    public class HykbBillboard
    {
        // 单例中间层对象，方便获取
        private static AndroidJavaClass sJavaClass;
        public static AndroidJavaClass getBillboardClass()
        {
            if (sJavaClass == null)
            {
                sJavaClass = new AndroidJavaClass("com.m3839.sdk.billboard.HykbBillboard");
            }
            return sJavaClass;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init(string gameId, int orientation, listener.BillBoardInitListener listener)
        {
            getBillboardClass().CallStatic("init", HykbContext.GetInstance().GetActivity(), gameId, orientation, listener);
        }

        /// <summary>
        /// 公告面板打开
        /// </summary>
        public static void OpenBillBoardPanel(listener.BillBoardPanelListener listener)
        {
            getBillboardClass().CallStatic("openBillBoardPanel", HykbContext.GetInstance().GetActivity(), listener);
        }


        /// <summary>
        /// 获取公告的未读情况
        /// </summary>
        public static void LoadBillBoardStatus(listener.BillBoardUnreadListener listener)
        {
            getBillboardClass().CallStatic("loadBillBoardStatus", HykbContext.GetInstance().GetActivity(), listener);
        }
    }
}

