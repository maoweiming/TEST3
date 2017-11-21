using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Button_WearHF : UIBase
{


    public Text[] btn;

    public Button button;
    public Button ziliaoku;

    void Start()
    {

        button = transform.GetChild(0).GetComponent<Button>();
        ziliaoku = transform.GetChild(1).GetComponent<Button>();

   
        /*
       //获取所有的子类transform组件
       Transform[] childTrans = transform.GetComponentsInChildren<Transform>();

       for (int i = 0; i < childTrans.Length; i++)
       {
           //如果不是以_n结尾的就添加
           if (!childTrans[i].name.EndsWith("_n"))
               childTrans[i].gameObject.AddComponent<UIBehaviours>();
       }
*/
        btn = gameObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < btn.Length; i++)
        {
            Debug.Log(btn[i].text + "=======所有Button事件的名字");
        //    m_wearHF.AddVoiceCommand(btn[i].text, Open);
            Main_WearHF.Instance.m_AddVoiceCommand(btn[i].text,Open);
        }


    }


    /// <summary>
    /// 语音识别判断方法
    /// </summary>
    /// <param name="com"></param>
    public void Open(string com)
    {

        if (button.GetComponentInChildren<Text>().text == com)
        {
            OpenOne();
        }
        else if (ziliaoku.GetComponentInChildren<Text>().text == com)
        {

            DataOpen1();
        }



    }

    /// <summary>
    /// 排班表的方法
    /// </summary>
    public void OpenOne()
    {


        //查看有什么重复的
        GameObject mo = GetGameObjects("Main_interfaceBg", "Main_interfaceBg");
        GameObject mo1 = GetGameObjects("Database_bg", "Database_bg");
        GameObject first = GetGameObjects("检查步骤", "检查步骤");
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

        if (first != null)
        {
            Destroy(first);
            UIManage.Instance.UnDesinPanel(first.name);
        }
    }

    /// <summary>
    /// 资料库的方法
    /// </summary>
    public void DataOpen1()
    {

        GameObject mo = GetGameObjects("Database_bg", "Database_bg");

        GameObject mo1 = GetGameObjects("Main_interfaceBg", "Main_interfaceBg");
        //      Debug.Log(mo1.name);

        if (mo == null)
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
