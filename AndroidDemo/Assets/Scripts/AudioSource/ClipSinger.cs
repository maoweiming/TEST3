using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipSinger  {


    //进行组装
    AudioClip clip;
    AudioSource audioSource;

    float intVoume;


    public ClipSinger(AudioClip tmpClip)
    {
        clip = tmpClip;
    }
    public void PlayAudioSource(AudioSource tmpSource)
    {
        audioSource = tmpSource;
        audioSource.clip = clip;
        //播放
        audioSource.Play();
        Debug.Log(4);

    }


    /// <summary>
    /// z暂停
    /// </summary>
    public void StopAudioSource()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
	
    /// <summary>
    /// 声音控制
    /// </summary>
    /// <param name="volumSize"></param>
    public void AudioSourceVoume(float volumSize)
    {
        if (audioSource != null)
        {
            audioSource.volume = volumSize;
        }
    }
}


