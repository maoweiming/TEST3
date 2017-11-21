using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UsewwwFile : MonoBehaviour {


    public static UsewwwFile Instance;
 
    /// <summary>
    /// new 一个wwwFile下载东西 
    ///  tmpFlie.StartDownload();把自己加载进去
    /// </summary>
    void Start () {
        WWWFile tmpFlie = new WWWFile("Gizmos/a330.png");
  
        tmpFlie.StartDownload();

       
    }

  
	
}
