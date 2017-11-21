using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class home_Btn : UIBase {


  


 
	void Start () {

        AddComponentToChild();
    //  AddButtonLisenter("Button", Open);
        Open();
	}

   
    public void Open()
    {

        //查看有什么重复的
        GameObject mo = GetGameObjects("Main_interfaceBg", "Main_interfaceBg");
        GameObject mo1 = GetGameObjects("Database_bg", "Database_bg");
  
        if (mo == null)
        {
          
            //生成预设体
         UIManage.Instance.SetParentResourcesLoad("Main_interfaceBg");
   
        }
        else
        {
            //防止注册
      UIManage.Instance.UnRegistGameObject("Main_home", "Main_interfaceBg");
        }

        if (mo1 != null)
        {
            Destroy(mo1);
            UIManage.Instance.UnDesinPanel(mo1.name);
        }
  
       
    }
}
