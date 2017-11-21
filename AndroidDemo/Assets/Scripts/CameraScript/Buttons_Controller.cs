using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WearHFPlugin;

public class Buttons_Controller : MonoBehaviour {

    //暂停按钮
    public Button m_PauseButton;
    //开始按钮
    public Button m_RusumeButton;
    
    //摄像机材质
    private WebCamTexture m_webcam = null;
    //插件属相
    private WearHF m_weraHf;
    //白色   灰色
    private Color m_colorEnable = Color.white;
    private Color m_colorDisable = Color.grey;


    /// <summary>
    /// 按钮状态
    /// </summary>
    private enum ButtonStart
    {
        //启动 /禁止
        Enable,
        Disabled
    }

    private void Start()
    {
        //查找Button
        m_PauseButton = transform.GetChild(0).GetComponent<Button>();
        m_RusumeButton = transform.GetChild(1).GetComponent<Button>();
        //摄像机材质 身上的脚本
        m_webcam = GameObject.FindGameObjectWithTag("RawIamge").GetComponent<CameraController>().webcam;
        //获取插件 脚本
        m_weraHf = GameObject.FindGameObjectWithTag("WearHF Manager").GetComponent<WearHF>();


        ToggleButtonStart(
            m_PauseButton, m_PauseButton.interactable ? ButtonStart.Enable : ButtonStart.Disabled);

        ToggleButtonStart(
            m_RusumeButton, m_RusumeButton.interactable ? ButtonStart.Enable : ButtonStart.Disabled);
    }

    /// <summary>
    /// 恢复相机
    /// </summary>
    public void ResumeCamera()   
    {
        //打开
        m_webcam.Play();
        ToggleButtonStart(m_PauseButton, ButtonStart.Enable);
        ToggleButtonStart(m_RusumeButton, ButtonStart.Disabled);

    }

    /// <summary>
    /// 暂停相机
    /// </summary>
    public void PauseCamera()
    {
        //暂停相机
        m_webcam.Stop();
        ToggleButtonStart(m_PauseButton, ButtonStart.Disabled);
        ToggleButtonStart(m_RusumeButton, ButtonStart.Enable);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="button">按钮</param>
    /// <param name="start">按钮的状态</param>
    private void ToggleButtonStart(Button button, ButtonStart start)
    {
        //获取button 里面的Text
        Text buttonText = button.GetComponentInChildren<Text>();

        switch (start)
        {
            case ButtonStart.Enable:
                //改变字体颜色
                buttonText.color = m_colorEnable;
                button.interactable = true;

                m_weraHf.AddVoiceCommand(buttonText.text, VoiceCommandCallback);

                break;

            case ButtonStart.Disabled:
                buttonText.color = m_colorDisable;
                button.interactable = false;
                m_weraHf.RemoveVoiceCommand(buttonText.text);
                break;
        }

    }
    /// <summary>
    /// 语音触发判断
    /// </summary>
    /// <param name="voiceCommand">text里面的值</param>
    private void VoiceCommandCallback(string voiceCommand)
    {
        if (m_PauseButton.GetComponentInChildren<Text>().text == voiceCommand)
        {
            PauseCamera();
        }else if(
            m_RusumeButton.GetComponentInChildren<Text>().text == voiceCommand)
        {
            ResumeCamera();
        }
    }
}
