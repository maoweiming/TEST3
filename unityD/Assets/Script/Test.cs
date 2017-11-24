using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour {

  

    void OnGUI()
    {
        //调用显示一个文本为“Hello World!”的Toest
        if (GUI.Button(new Rect(0, 0, 200, 20), "Show Toest - Hello World!"))
        {
            //Unity侧调用Android侧代码
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    //调用成员方法
                    jo.Call("ShowToast", "Hello World!");
                }
            }
        }

      
    }

}
