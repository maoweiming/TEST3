using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour {

    /*
 1:UIBehaviours做监听事件首先在UIBase中把所有的  子物体找到并且加上UIBehaviours
 2：从UIBase中找UIBehaviours对他进行逻辑控制（这里我封装，针对不同的控件，调用不同的方法，所有UIBehaviours是万能的）
 3：子类里的东西直接调用父类里的，因为在父类，都封装好了，子类直接用减少了代码量
      //专门解决事件绑定
       // 获取所有子类的transform都挂UIBehaviours组件(调用UIBase里的方法）
        AddComponentToChild();
           AddButtonLisenter("Start",LoadCtr. OnClik);*/

    public  Dictionary<string, Dictionary<string, GameObject>> sonMebers;



    public static UIManage Instance;
    private void Awake()
    {
        Instance = this;
        sonMebers = new Dictionary<string, Dictionary<string, GameObject>>();
    }


    /*
     public void bina()
    {
        foreach (KeyValuePair<string, Dictionary<string, GameObject>> kvp in sonMebers)
        {
            Debug.Log(kvp.Key);

           
        }
    }
     * */
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="panleName">panle名字</param>
    /// <param name="ObjName">自己的名字</param>
    /// <param name="tmpObj">本身</param>
    public void AddRegistGameObject(string panleName, string ObjName, GameObject tmpObj)
    {
        if (sonMebers.ContainsKey(panleName))
        {
            sonMebers[panleName].Add(ObjName, tmpObj);
        }
        else
        {
            Dictionary<string, GameObject> tmpDict = new Dictionary<string, GameObject>();
            tmpDict.Add(ObjName, tmpObj);
            sonMebers.Add(panleName, tmpDict);
        }
    }

    /// <summary>
    /// 反注册
    /// </summary>
    /// <param name="panleName"></param>
    /// <param name="objName"></param>
    public void UnRegistGameObject(string panleName, string objName)
    {
        if (sonMebers.ContainsKey(panleName))
        {
            //如果有就删除
            sonMebers[panleName].Remove(objName);
        }
    }

    //删除
    public void UnDesinPanel(string panelName)
    {
        if (sonMebers.ContainsKey(panelName))
        {
            sonMebers[panelName].Clear();
        }
    }

    /// <summary>
    /// 查找子控件
    /// </summary>
    /// <param name="panleName">panle名字</param>
    /// <param name="objName">本身名字</param>
    /// <returns></returns>
    public GameObject GetGameObject(string panleName, string objName)
    {
        if (sonMebers.ContainsKey(panleName))
        {
            if (sonMebers[panleName].ContainsKey(objName))
            {
                return sonMebers[panleName][objName];
            }
        }
        return null;
    }

   

    /// <summary>
    /// 父物体加载 设置
    /// </summary>
    /// <param name="prefabName"></param>
    public GameObject SetParentResourcesLoad(string prefabName)
    {
        //加载到内存
        Object prefabsObj = Resources.Load("Prefabs/" + prefabName);
        //进行实例
        GameObject tmpprefabs = GameObject.Instantiate(prefabsObj) as GameObject;
        //去掉名字clone
        tmpprefabs.name = tmpprefabs.name.Replace("(Clone)", "");
        //找Canvas
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        //设置父物体
        tmpprefabs.transform.SetParent(canvas.transform.GetChild(0).transform, false);


        return tmpprefabs;
    }



    public GameObject OnDestroys(string Name)
    {
        if (sonMebers.ContainsKey(Name))
        {
            //  UnDesinPanel(Name); 

            return gameObject;
        }

        // foreach (KeyValuePair<string, Dictionary<string, GameObject>> item in sonMebers)

        return null;
    }
}
