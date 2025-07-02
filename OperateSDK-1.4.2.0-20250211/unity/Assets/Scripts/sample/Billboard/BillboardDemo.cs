using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.m3839.sdk.billboard;
using com.m3839.sdk.billboard.listener;

public class BillboardDemo : MonoBehaviour
{
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitClick()
    {
        string gameId = "5091";
        HykbBillboard.Init(gameId, 1, new BillBoardInitListenerProxy(this));
    }

    private class BillBoardInitListenerProxy : BillBoardInitListener
    {
        private BillboardDemo demo;
        public BillBoardInitListenerProxy(BillboardDemo demo)
        {
            this.demo = demo;
        }
        public override void OnFailed(int code, string message)
        {
            demo.ShowText.text = "Failed: code = " + code + ",message = " + message;
        }

        public override void OnSuccess()
        {
            demo.ShowText.text = "初始化成功";
        }
    }

    public void OpenBillboardClick()
    {
        HykbBillboard.OpenBillBoardPanel(new BillBoardPanelListenerProxy(this));
    }

    private class BillBoardPanelListenerProxy : BillBoardPanelListener
    {
        private BillboardDemo demo;
        public BillBoardPanelListenerProxy(BillboardDemo demo)
        {
            this.demo = demo;
        }

        public override void OnFailed(int code, string message)
        {
            demo.ShowText.text = "Failed: code = " + code + ",message = " + message;
        }

        public override void OnPanelClose()
        {
            demo.ShowText.text = "公告弹出关闭了";
        }
    }

    public void LoadBillboardClick()
    {
        HykbBillboard.LoadBillBoardStatus(new BillBoardUnreadListenerProxy(this));
    }

    private class BillBoardUnreadListenerProxy : BillBoardUnreadListener
    {
        private BillboardDemo demo;
        public BillBoardUnreadListenerProxy(BillboardDemo demo)
        {
            this.demo = demo;
        }

        public override void OnFailed(int code, string message)
        {
            demo.ShowText.text = "Failed: code = " + code + ",message = " + message;
        }

        public override void OnResult(bool unread)
        {
            if(unread)
            {
                demo.ShowText.text = "还有未读消息";
            }
            else
            {
                demo.ShowText.text = "全部已读";
            }
        }
    }
}
