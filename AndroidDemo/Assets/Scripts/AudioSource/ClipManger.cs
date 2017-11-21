using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipManger  {

    //音效名字数组
    string[] clipName;

    ClipSinger[] clips;

    //实例化进行
    public ClipManger()
    {
        Initial();
       
    }


    public void Initial()
    {
        //实例 存入音乐名字
        clipName = new string[]
        {
            "so",
            "U.I. Sound 1",
            "U.I. Sound 2"
        };

        //加载声音片段
        clips = new ClipSinger[clipName.Length];
        Debug.Log(clips);
        for (int i = 0; i < clipName.Length; i++)
        {
           
            AudioClip clipMusic = Resources.Load("Audio/" + clipName[i]) as AudioClip;
           
            clips[i] = new ClipSinger(clipMusic);
        }

    }

/// <summary>
/// 查找声音片段
/// </summary>
/// <param name="tmpName"></param>
/// <returns></returns>
    public int FindClips(string tmpName)
    {
     
        for (int i = 0; i < clipName.Length; i++)
        {
            if (tmpName == clipName[i])
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="tmpSource"></param>
    /// <param name="tmpName"></param>
    public void PlayAudio(AudioSource tmpSource,string tmpName)
    {
        int tmpIndex = FindClips(tmpName);
        if (tmpIndex != -1)
        {
            clips[tmpIndex].PlayAudioSource(tmpSource);
        }
    }

    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="audioName"></param>
    public void StopAudio(string audioName)
    {
        int tmpIndex = FindClips(audioName);
        if (tmpIndex != -1)
        {
            clips[tmpIndex].StopAudioSource();
        }
    }
}
