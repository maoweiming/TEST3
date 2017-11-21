using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WWWFile : wwwItem
{


    //构造函数 下载什么东西
    public WWWFile(string path)
    {
        InitialURL(path);
    }
    public void InitialURL(string tmpPath)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.OSXEditor)
        {
            URL = "file:///" + Application.dataPath + "/" + tmpPath;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            URL = "file://" + Application.dataPath + "/" + tmpPath;

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            URL = "file://" + Application.dataPath + "/" + tmpPath;
        }
    }

    /// <summary>
    /// 继承父类 有重新
    /// 出错 加入队列
    /// </summary>
    /// <param name="tmpItem"></param>
    public override void DownloadError(wwwItem tmpItem)
    {
        // base.DownloadError(tmpItem);
        wwwHelper.Instance.AddTarget(tmpItem);
    }
    /// <summary>
    /// 下载完成
    /// 做什么都可以
    /// </summary>
    /// <param name="tmpWWW"></param>
    public override void DownloadFinish(WWW tmpWWW)
    {
      //  base.DownloadFinish(tmpWWW);
        Debug.Log("完成"+tmpWWW.texture);
        //下载完 保存
        
     
    }
}
