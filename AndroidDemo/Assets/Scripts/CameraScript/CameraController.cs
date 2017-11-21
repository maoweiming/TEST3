using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// 获取摄像头/管理相机类
/// </summary>
public class CameraController : MonoBehaviour
{

    [NonSerialized]
    public WebCamTexture webcam;

    //AspectRatioFitter 意思根据高宽比例进行
    /*
     public enum AspectMode 
{                     
    //不使用适合的纵横比
    None,

    //让Height随着Width自动调节
    WidthControlsHeight,

    //让Width随着Height自动调节
    HeightControlsWidth,

    //宽度、高度、位置和锚点都会被自动调整，以使得该矩形拟合父物体的矩形内，同时保持宽高比例
    FitInParent,

    //宽度、高度、位置和锚点都会被自动调整，以使得该矩形覆盖父物体的整个区域，同时保持宽高比
    EnvelopeParent
}
     
     
     Rect rect = GetComponent<Image>().sprite.rect;
AspectRatioFitter fitter = GetComponent<AspectRatioFitter>();
fitter.aspectRatio = rect.width / rect.height;
     
     http://blog.csdn.net/qq_26999509/article/details/53518645
     
     */
    private AspectRatioFitter m_camaerAspectRatio = null;
    private RawImage m_rawImage = null;


    void Awake()
    {
        //获取组件
        m_camaerAspectRatio = GetComponent<AspectRatioFitter>();
        m_rawImage = GetComponent<RawImage>();

        webcam = new WebCamTexture();

        //获取rawImage的材质球
        m_rawImage.material.mainTexture=webcam;
        webcam.requestedHeight=400;
        webcam.requestedWidth=224;

        webcam.Play();
    }

    void Update()
    {
        float aspecRatio = (float)webcam.width / (float)webcam.height;
        //组件的属相==现在的
        m_camaerAspectRatio.aspectRatio = aspecRatio;

        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            m_rawImage.transform.localRotation = new Quaternion(0, 0, 180, 0);

        }
        else
        {
            m_rawImage.transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
