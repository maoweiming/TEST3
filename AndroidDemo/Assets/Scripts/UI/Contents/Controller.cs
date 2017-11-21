﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controller : MonoBehaviour {

    public InfiniteScroll scroll;

    private int count = 100;
	// Use this for initialization
	void Start () {

        Debug.Log("预留预设体空间=======" + count);
        scroll.FillItem += scroll_FillItem;

        scroll.PullLoad += scroll_PullLoad;
       //初始化无限滚动  100
        scroll.InitData(count);
	}


    void scroll_FillItem(int index, GameObject item)
    {
        //这里我们可以填写和修改item prefab
        //改变文字，图像等
        //通过索引，我们可以从JSON数组获取数据，例如
        item.transform.GetChild(0).GetComponent<Text>().text = "item##" + index;

    }

    //这里我们监听拖拽刷新事件并处理它
    //它可以将数据从服务器加载到JSON对象并附加到列表
    //做到这一点，调用ApplyDataTo函数，其中arg1 =通用项追加后计数，arg2 = count追加，arg3 =追加方向（顶部或底部）
    void scroll_PullLoad(InfiniteScroll.Direction obj)
    {
        count += 20;
        scroll.ApplyDataTo(count, 20, obj);
      
    }

   
	
	
}
