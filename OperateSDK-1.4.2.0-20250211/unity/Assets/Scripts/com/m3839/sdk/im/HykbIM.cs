﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im
{
    public class HykbIM
    {
        static AndroidJavaClass sdkClass = new AndroidJavaClass("com.m3839.sdk.im.HykbIM");
        static AndroidJavaClass unitySdkClass = new AndroidJavaClass("com.m3839.sdk.im.unity.HykbUnityIM");

        /**
         * 内置快爆登录SDK的初始化（IM + 快爆登录的初始化）
         */
        public static void initWithHykb(string appId, int orientation, listener.HykbIMInitListener listener)
        {
            sdkClass.CallStatic("initWithHykb", HykbContext.GetInstance().GetActivity(), appId, orientation, listener);
        }

        /**
         * 内置快爆登录账号体系的登录（IM + 快爆登录的登录）
         */
        public static void loginWithHykb(listener.UnityIMUserListener listener)
        {
            unitySdkClass.CallStatic("loginWithHykb", HykbContext.GetInstance().GetActivity(), listener);
        }


        public static void logoutWithHykb(listener.HykbIMCallback callback)
        {
            sdkClass.CallStatic("logoutWithHykb", HykbContext.GetInstance().GetActivity(), callback);
        }

        /**
         * IM的监听
         */
        public static void addHykbIMSDKListener(listener.HykbIMSDKListener listener)
        {
            sdkClass.CallStatic("addHykbIMSDKListener", listener);
        }

        /**
         * 好友关系链监听
         */ 
        public static void addHykbIMFriendShipListener(listener.HykbIMFriendShipListener friendShipListener)
        {
            sdkClass.CallStatic("addHykbIMFriendShipListener", friendShipListener);
        }

        /**
         * 邀请相关的全局监听
         */
        public static void addHykbIMSignalingListener(listener.HykbIMSignalingListener signalingListener)
        {
            sdkClass.CallStatic("addHykbIMSignalingListener", signalingListener);
        }

        /// <summary>
        /// IM的初始化
        /// </summary>
        /// <param name="appId">游戏ID（快爆的或者游戏盒）</param>
        /// <param name="chanelType">为1表示游戏盒渠道，为0表示快爆</param>
        /// <param name="listener"></param>
        public static void init(string appId, int chanelType, listener.HykbIMInitListener listener)
        {
            sdkClass.CallStatic("init", HykbContext.GetInstance().GetActivity(), appId, chanelType, listener);
        }

        /// <summary>
        /// IM的登录
        /// </summary>
        /// <param name="userId">渠道的userId</param>
        /// <param name="state">游戏盒渠道需要的用户数据，快爆渠道传null或者""</param>
        /// <param name="listener"></param>
        public static void login(string userId, string state, listener.UnityIMUserListener listener)
        {
            sdkClass.CallStatic("login", userId, state, listener);
        }

        public static bool isLogin()
        {
            return sdkClass.CallStatic<bool>("isLogin");
        }

        public static void logout(listener.HykbIMCallback callback)
        {
            sdkClass.CallStatic("logout", callback);
        }


        public static void getUserInfo(string userId, listener.UnityIMUserListener listener)
        {
            unitySdkClass.CallStatic("getUserInfo", userId, listener);
        }

        public static void getUserInfo(List<string> userIdList, listener.UnityIMUserListListener listener)
        {
            AndroidJavaObject listObject = new AndroidJavaObject("java.util.ArrayList");
            for (int i = 0; i < userIdList.Count; i++)
            {
                listObject.Call<bool>("add", userIdList[i]);
            }
            unitySdkClass.CallStatic("getUserInfo", listObject, listener);
        }

        public static void getUserStatus(List<string> userIdList, listener.UnityIMUserStatusListener listener)
        {
            AndroidJavaObject listObject = new AndroidJavaObject("java.util.ArrayList");
            for (int i = 0; i < userIdList.Count; i++)
            {
                listObject.Call<bool>("add", userIdList[i]);
            }
            unitySdkClass.CallStatic("getUserStatus", listObject, listener);
        }

        public static void getFriendApplicationList(listener.UnityIMFriendApplicationResultListener listener)
        {
            unitySdkClass.CallStatic("getFriendApplicationList", listener);
        }

        public static void getFriendList(listener.UnityIMFriendListListener listener)
        {
            unitySdkClass.CallStatic("getFriendList", listener);
        }

        public static void searchFriends(string keyword, listener.UnityIMFriendListListener listener)
        {
            Debug.Log("chenby keyword = "+ keyword);
            unitySdkClass.CallStatic("searchFriends", keyword, listener);
        }

        public static void addFriend(string userId, string addWording, listener.UnityIMFriendOperationResultListener listener)
        {
            unitySdkClass.CallStatic("addFriend", userId, addWording, listener);
        }

        public static void deleteFromFriendList(List<string> userIdList, listener.UnityIMFriendOperationResultListListener listener)
        {
            AndroidJavaObject listObject = new AndroidJavaObject("java.util.ArrayList");
            for(int i = 0; i < userIdList.Count; i++)
            {
                listObject.Call<bool>("add", userIdList[i]);
            }
            unitySdkClass.CallStatic("deleteFromFriendList", listObject, listener);
        }

        public static void acceptFriendApplication(bean.HykbIMFriendApplication application, listener.UnityIMFriendOperationResultListener listener)
        {
            unitySdkClass.CallStatic("acceptFriendApplication", application.GetApplicationObject(), listener);
        }

        public static void refuseFriendApplication(bean.HykbIMFriendApplication application, listener.UnityIMFriendOperationResultListener listener)
        {
            unitySdkClass.CallStatic("refuseFriendApplication", application.GetApplicationObject(), listener);
        }

        public static void deleteFriendApplication(bean.HykbIMFriendApplication application, listener.HykbIMCallback listener)
        {
            sdkClass.CallStatic("deleteFriendApplication", application.GetApplicationObject(), listener);
        }

        public static string invite(string inviteeUserId, string data, listener.HykbIMCallback callback)
        {
            return sdkClass.CallStatic<string>("invite", inviteeUserId, data, callback);
        }

        public static void accept(string inviteId, string data, listener.HykbIMCallback callback)
        {
            sdkClass.CallStatic("accept", inviteId, data, callback);
        }

        public static void reject(string inviteID, string data, listener.HykbIMCallback callback)
        {
            sdkClass.CallStatic("reject", inviteID, data, callback);
        }

        public static void cancel(string inviteID, string data, listener.HykbIMCallback callback)
        {
            sdkClass.CallStatic("cancel", inviteID, data, callback);
        }

        public static void release()
        {
            sdkClass.CallStatic("release");
        }
    }
}

