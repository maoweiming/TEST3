using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPalyer : MonoBehaviour {

    ClipManger clipManger;
    AudioManger audioManger;
    //单例脚本
    public static AudioPalyer Instance;

    private void Awake()
    {
        Instance = this;
        clipManger = new ClipManger();
        audioManger = new AudioManger(gameObject);
    }
    
    public void PlayAudio(string Name)
    {
        AudioSource tmpAudio = audioManger.GetFreeAudioSource();
        clipManger.PlayAudio(tmpAudio, Name);
    }

    public void StopAudio(string tmpName)
    {
        clipManger.StopAudio(tmpName);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
         //   PlayAudio("so");
            Debug.Log(" 音乐");
        }
    }
}
