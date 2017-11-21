using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wwwItem  {

    //开始抽象这个下载任务  怎么就变了策略类  
    //采用多态 这是父类  就要抽象共有的东西  www协程去下载


    private string url;
    //构造方法  外界调用 get set
     public string URL
    {
        get
        {
            return url;
        }set
        {
            url = value;
        }
    }
    /// <summary>
    /// 启动 时放队列
    /// </summary>
   public void StartDownload()
    {
        wwwHelper.Instance.AddTarget(this);
    }
    public virtual void BeginDownload()
    {
         
    }
   //这也不做处理 交给上层 下载完处理方法
   /// <summary>
   /// 下载完成 处理
   /// </summary>
   /// <param name="tmpWWW"></param>
    public virtual void DownloadFinish(WWW tmpWWW)
    {

    }
    /// <summary>
    /// 下载出错处理
    /// 整个类返回给上层
    /// </summary>
    /// <param name="tmpWWW"></param>
   public virtual void DownloadError(wwwItem tmpItem)
    {

    }
   public IEnumerator Downoload()
    {
        //设置好URL  下载URL
        WWW tmpWWW = new WWW(URL);
        //下载 完
        yield return tmpWWW;

        //进行判断  下载完 和 出错  父不能不处理逻辑  
        //逻辑交给 上层处理 采用虚函数virtual
        if (string.IsNullOrEmpty(tmpWWW.error))
        {
            //交给
            DownloadFinish(tmpWWW);
        }
        else
        {
            //出错处理 返回一个父类
            DownloadError(this);
        }
    }

}
