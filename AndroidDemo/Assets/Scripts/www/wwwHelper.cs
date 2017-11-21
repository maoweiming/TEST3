using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当成入口 做成单例
/// </summary>
public class wwwHelper : MonoBehaviour {

    Queue<wwwItem> dowmLoad;

    public static wwwHelper Instance;
   
    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="tmpItem">下载任务</param>
    public void AddTarget(wwwItem tmpItem)
    {
        //进队列 就下载
        dowmLoad.Enqueue(tmpItem);


        ///如果完成 在启动协程
        if (isDownloadFinish)
        {
            StartCoroutine(DownloadQueue());
        }
        //进队列 ==false  you东西
        isDownloadFinish = false;
    }

    /// <summary>
    /// 开始=true
    /// 进队列=false
    /// 下载完= true
    /// </summary>
    bool isDownloadFinish;
    public IEnumerator DownloadQueue()
    {
        while (dowmLoad.Count > 0)
        {
            //出队列
            wwwItem tmpItem = dowmLoad.Dequeue();
            //开始下载  启动父类一个协程
            yield return tmpItem.Downoload();
        }
        //下载完  ture
        isDownloadFinish = true;
    }
    private void Awake()
    {

        Instance = this;
        dowmLoad = new Queue<wwwItem>();
        isDownloadFinish = true;
    }
}
