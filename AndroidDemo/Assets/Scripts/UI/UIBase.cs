using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIBase : MonoBehaviour {

    /// <summary>
    /// 获取所有子类的transform都挂UIBehaviours组件
    /// </summary>
    public void AddComponentToChild()
    {
        //获取所有子类的transform  都挂UIBehaviours组件
        Transform[] childTranms = transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childTranms.Length; i++)
        {
            //以什么结尾EndsWith
           if(!childTranms[i].name.EndsWith("_n"))
            childTranms[i].gameObject.AddComponent<UIBehaviours>();
        }

    }


    /// <summary>
    /// 查找   panle名字==transfoem.name
    /// </summary>
    /// <param name="childName"></param>
    /// <returns></returns>
    public UIBehaviours GetBehavirous(string childName)
    {
       
        //从这拿走
        GameObject tmpObj = UIManage.Instance.GetGameObject(transform.name, childName);
        UIBehaviours tmpBehavious=  tmpObj.GetComponent<UIBehaviours>();
        return tmpBehavious;
    }

    /// <summary>
    ///动态 添加事件
    ///只要UIBehaviours不为空就可以添加
    /// </summary>
    /// <param name="childName"></param>
    /// <param name="action"></param>
    public void AddButtonLisenter(string childName, UnityAction action)
    {
        //通过UIBase里的GetBehavirous方法获取控件的名字
        UIBehaviours tmpBahavriou = GetBehavirous(childName);

        if (tmpBahavriou != null)
        {
            tmpBahavriou.AddButtonListener(action);
        }
    }


    public void AddSliderLisenter(string childName,UnityAction<float> action)
    {
        UIBehaviours tmpBahavriou = GetBehavirous(childName);

        if (tmpBahavriou != null)
        {
            tmpBahavriou.AddSliderListener(action);
        }
    }

    ///这是 重载 这是查找字典里的东西
    public GameObject GetGameObjects(string panleName, string childName)
    {
        GameObject tmpobj =UIManage.Instance.GetGameObject(panleName, childName);

        return tmpobj;
    }


    //提高代码复用率
    public GameObject GetGameObjects(string childName)
    {
        GameObject tmpObj = GetGameObjects(transform.name, childName);
        return tmpObj;
    }

   
}
