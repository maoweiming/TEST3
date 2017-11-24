using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestunityAndroid : MonoBehaviour
{

  
    public Text text;
    public Button btn;
    private AndroidJavaObject jo = null;

    void Start()
    {
        //固定方法
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        jo = jc.GetStatic<AndroidJavaObject>("currenActivity");

      
        
    }

    public void Click()
    {
        text.text = "";
        int res = jo.Call<int>("add", 56, 100);

        text.text = "56+100=" + res.ToString();

    }
    /*

    public GUILayer label; //显示的lableUI


    /// <summary>
    /// 调用安卓的对话框
    /// </summary>
    public void FirstButtononClick()
    {

        //Android de接口
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaClass jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

        //参数
        string[] mObject = new string[2];

        //调用的方法
        string ret = jo.Call<string>("ShowDialog", mObject);

       SetUILabelText(ref ret );
    }

    /// <summary>
    /// 调用安卓Toast
    /// </summary>
    public void ScendButtonClick()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaClass jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("ShowToast", "Showing on Toast");

       
    }

    /// <summary>
    /// 测试 unity->java->unity
    /// </summary>
    public void ThirdButtonClick()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("callUnityFunc", "Scripts", "BeCallFunc", "yangx");
    }

    //设置一个回掉方法
    private void BeCallFunc(string _content)
    {
        SetUILabelText(ref _content);
    }

    private void SetUILabelText(ref string value)
    {
        label.text = value;
    }

     */
   

}