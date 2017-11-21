using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBehaviours : MonoBehaviour {

   /// <summary>
   /// 把所有的东西存到UIManger里
   /// </summary>
    void Awake () {
        //找到挂有UIBase 是我父类
        UIBase tmpBase = gameObject.GetComponentInParent<UIBase>();

        //每个都注册  panle 名字   本身的名字   自身
        UIManage .Instance.AddRegistGameObject(tmpBase.name ,transform.name,gameObject);
	}
	
	public void AddButtonListener(UnityAction action)
    {
        Button tmpButton = transform.GetComponent<Button>();

        if (tmpButton != null)
        { 
            tmpButton.onClick.AddListener(action);
        }
    }

    /// <summary>
    /// 按下之后还要回调函数
    /// </summary>
    public void AddInputEditorEndListener(UnityAction<string > action)
    {
        InputField tmpButton = transform.GetComponent<InputField>();
        if (tmpButton != null)
        {
            tmpButton.onEndEdit.AddListener(action);
        }
    }


    public void AddSliderListener(UnityAction<float>  action)
    {
        Slider tmpButton = transform.GetComponent<Slider>();
        if (tmpButton != null)
        {
            tmpButton.onValueChanged.AddListener(action);
        }
    }


    public void AddToggleListener(UnityAction<bool>  action)
    {
        Toggle tmpButton = transform.GetComponent<Toggle>();

        if (tmpButton != null)
        {
            tmpButton.onValueChanged.AddListener(action);
        }
    }

    public void AddDropdownListener(UnityAction<int>  action)
    {
        Dropdown tmpButton = transform.GetComponent<Dropdown>();
        if (tmpButton != null)
        {
            tmpButton.onValueChanged.AddListener(action);
        }
    }
}
