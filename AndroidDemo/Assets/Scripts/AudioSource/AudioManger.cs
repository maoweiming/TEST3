using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger  {

    List<AudioSource> sourceList;
    GameObject ower;


    public AudioManger(GameObject tmpOwer)
    {
        ower = tmpOwer;
        Intial();

    }

    /// <summary>
    /// 初始操作
    /// </summary>
    public void Intial()
    {
        sourceList = new List<AudioSource>();
        Debug.Log(2);
        for (int i = 0; i < sourceList.Count; i++)
        {
            //添加队列音效组件
         AudioSource tmpSource=   ower.AddComponent<AudioSource>();
          
         sourceList.Add(tmpSource);
        }
    }


    /// <summary>
    /// 看是属于空闲
    /// </summary>
    /// <returns></returns>
    public AudioSource GetFreeAudioSource()
    {
        for (int i = 0; i < sourceList.Count; i++)
        {
            AudioSource tmpSource = sourceList[i];
            if (!tmpSource.isPlaying)
            {
                return tmpSource;
            }
        }

        AudioSource tmpSource1 = ower.AddComponent<AudioSource>();
        sourceList.Add(tmpSource1);
        return tmpSource1;
    }


   /// <summary>
   /// 清除
   /// </summary>
    public void ClearFree()
    {
        int tmpCount = 0;
        for (int i = 0; i < sourceList.Count; i++)
        {
          AudioSource tmpSource=  sourceList[i];
            if (!tmpSource.isPlaying)
            {
                tmpCount++;
                //如果大于2就删除
                if (tmpCount > 2)
                {
                    sourceList.Remove(tmpSource);
                }
            }
        }
    }
}
