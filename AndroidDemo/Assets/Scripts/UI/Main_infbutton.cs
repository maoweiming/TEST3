using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_infbutton : UIBase {



    public Button btn_Name;

    GameObject prefabs;
    public void Start()
    {
       AddComponentToChild();
       btn_Name = GameObject.FindGameObjectWithTag("Button_name").GetComponent<Button>();

       Text btn_Text = btn_Name.GetComponent<Text>();

       Main_WearHF.Instance.m_AddVoiceCommand(btn_Text.text,name_Btn);

    //   AddButtonLisenter("Button_Name", Firststep);
    }
            

    public void name_Btn(string com)
    {
        if (btn_Name.GetComponentInChildren<Text>().text == com)
        {
            Firststep();
        }
    }

    public void Firststep()
    {

        GameObject first = GetGameObjects("检查步骤", "检查步骤");
        GameObject main_InterfaceBg= GetGameObjects("Main_interfaceBg", "Main_interfaceBg");
        GameObject dataBase_bg = GetGameObjects("Database_bg", "Database_bg");


        if (first == null)
        {
            Destroy(main_InterfaceBg);
            Destroy(dataBase_bg);
            //删除字典里的名字
            UIManage.Instance.UnDesinPanel(main_InterfaceBg.name);
          
            Debug.Log("生成步骤一");
            UIManage.Instance.SetParentResourcesLoad("检查步骤");
        }
        else
        {          
            UIManage.Instance.UnRegistGameObject("Main_home","检查步骤");
        }
       
    }
}
