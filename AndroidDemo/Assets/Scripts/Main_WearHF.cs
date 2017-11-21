using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WearHFPlugin;
using System;
/// <summary>
/// WeraHF单列脚本
/// </summary>
public class Main_WearHF : MonoBehaviour {


    WearHF main_WeraHF;

    public static Main_WearHF Instance;
	void Awake () {

        Instance = this;
        main_WeraHF = GameObject.FindGameObjectWithTag("WearHF Manager").GetComponent<WearHF>();

       
	}


    public void m_AddVoiceCommand(string com, Action<string> action)
    {
        main_WeraHF.AddVoiceCommand(com,action);
    }
}
