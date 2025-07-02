using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.m3839.sdk;
using com.m3839.sdk.share;

public class ShareDemo : MonoBehaviour
{
    public Text ShowText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("我是返回键￣ω￣");
            SceneManager.LoadScene("MainScene");
        }
    }

    public void Share()
    {
        ShowText.text = "分享 开始...";
        string id = "136";
        string content = "我是分享测试文案换行再来一句";
        // List<string> imageList = new List<string> { };// 图片资源列表 本地图片和网络图片 
        // List<string> imageList = new List<string> { "/storage/emulated/0/Pictures/QQ/Image_2042683883840617.jpg"};
        List<string> imageList = new List<string> { "https://himg.bdimg.com/sys/portraitn/item/public.1.bcdc2f1a.7UtzUt6dt6hR51duIpgBBA"};
        HykbShare.Share(id, content, imageList);
        ShowText.text = "分享 结束";
    }
}
