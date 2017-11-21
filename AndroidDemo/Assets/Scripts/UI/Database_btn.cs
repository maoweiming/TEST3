using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Database_btn : UIBase {



   
	void Start () {
        AddComponentToChild();
    //    AddButtonLisenter("Button1", DataOpen);

		
	}

    public void DataOpen()
    {

        GameObject mo = GetGameObjects("Database_bg", "Database_bg");
     
        GameObject mo1 = GetGameObjects("Main_interfaceBg", "Main_interfaceBg");
  //      Debug.Log(mo1.name);
     
       if (mo==null)
       {
           Destroy(mo1);
           //删除字典里的名字
           UIManage.Instance.UnDesinPanel(mo1.name);
        
         //生产预设体
           UIManage.Instance.SetParentResourcesLoad("Database_bg");

       }
       else
       {
           UIManage.Instance.UnRegistGameObject("Main_home", "Database_bg");
       }
     
          
    }
}
