using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;


/*
 * des:摄像机工具类
 * date：2017.11.20
 * function：
 * 1.根据plane的大小设置摄像头的区域
 * 2.截屏、录屏区域和上诉相同
 *
 */
public class Camera_takephoto : MonoBehaviour
{

    //设备名称
    public string deviceName;
    //接受返回的图片数据
    private WebCamTexture tex;
    //预览区域宽高
    private int mHeight, mWeight;
    private int mStarta, mStarty;
    //是否在录屏
    private bool mIsSeriousCapture = false;
    //plane的对象，承载显示区域摄像头图片，注意旋转角度
    public Transform planeT;
    //存档路径-test
    private string filePath;


   void Start()
    {

      

          
        //如果没有sdcard，就建立文件夹
        if (!Directory.Exists(Application.dataPath + "/sdcard"))
        {
            Directory.CreateDirectory(Application.dataPath + "/sdcard");
        }

        filePath = Application.dataPath + "/sdcard/";
       /*
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = "/sdcard/";
        }
        else
        {
            filePath = "C:/Users/Administrator/Desktop";
        }
     */

    }

 public void zhaoXiang()
   {
       capturePicJpg(filePath);
   }

 public void luXiang()
   {
       seriouPicPng(filePath);
   }
    /// <summary>
    /// 开启摄像头
    /// </summary>
    public void startCamera()
    {
        if (null == planeT)
        {
            return;
        }

        if (null != tex)
        {
            if (!tex.isPlaying)
            {
                tex.Play();
            }
            return;
        }
        //开启协程
        StartCoroutine(start());
    }
    /// <summary>
    /// 关闭摄像头
    /// </summary>
    public void stopCamera()
    {
        //如果在录制，停止录屏操作
        mIsSeriousCapture = false;

        if (null == tex)
        {
            //这个是？？？
            StopAllCoroutines();
            Debug.Log("StopAllCoroutines" + 1111111);
        }
        tex.Stop();
        StopAllCoroutines();
        Debug.Log("StopAllCoroutines" + 222222);
        tex = null;
    }
    /// <summary>
    /// 摄像头停下了就相当于拍照结束---此时可以直接获取Textrue
    /// </summary>
    /// <returns></returns>
    public Texture getFinalTexture()
    {
        return planeT.GetComponent<Renderer>().material.mainTexture;
    
    }
    /// <summary>
    /// 拍照----JPG
    /// </summary>
    /// <param name="_filePath"></param>
    public void capturePicJpg(string _filePath)
    {
        //正在录屏直接返回
        if (mIsSeriousCapture || null == tex)
        {
            Debug.Log("正在录屏直接返回");
            return;
        }

        if (null != tex && !tex.isPlaying)
        {
            return;
        }

        tex.Pause();
        Debug.Log("开启协程---拍照jpg");
        StartCoroutine(getTexture(_filePath,0));
    }

    /// <summary>
    /// 拍照-----PNG
    /// </summary>
    /// <param name="_filePath"></param>
    public void capturePicPng(string _filePath)
    {
        //png---!屏直接返回
        if (mIsSeriousCapture || null == tex)
        {
            return;
        }

        if (null != tex && !tex.isPlaying)
        {
            return;
        }
        tex.Pause();
        StartCoroutine(getTexture(_filePath, 1));
    }


    public void seriousPicJpg(string _folder)
    {
        //正在录屏直接返回
        if (null == tex)
        {
            return;
        }

        if (null != tex && !tex.isPlaying)
        {
            return;
        }
        mIsSeriousCapture = true;
        StartCoroutine(getTexture(_folder, 0));
    }
    /// <summary>
    ///连续拍照---PNG
    /// </summary>
    /// <param name="_folder"></param>
    public void seriouPicPng(string _folder)
    {
        if (null == tex)
        {
            return;
        }
        if (null != tex && !tex.isPlaying)
        {
            return;
        }

        mIsSeriousCapture = true;

        Debug.Log("开启协程拍照----PNG");
     //   StartCoroutine(getTexture(_folder,1));
        StartCoroutine(SeriousPhotes(_folder, 1));
    }
    /// <summary>
    /// 启动摄像机
    /// </summary>
    /// <returns></returns>
    private IEnumerator start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {

            WebCamDevice[] device = WebCamTexture.devices;
            Debug.Log("启动摄像机后的 device" + device);
            if (device.Length > 0)
            {
                deviceName = device[0].name;
                ///<根据Panle设置摄像头
                Bounds bds = new Bounds();
                Debug.Log("这是bds" + bds);
                bds.min = Camera.main.WorldToScreenPoint(
                    planeT.gameObject.GetComponent<Renderer>().bounds.min);
                bds.max = Camera.main.WorldToScreenPoint(
                    planeT.gameObject.GetComponent<Renderer>().bounds.max);
                mHeight = (int)Mathf.Abs(bds.max.x - bds.min.x);
                mWeight = (int)Mathf.Abs(bds.max.y - bds.min.y);
                mStarta = (int)bds.min.x;
                mStarty = (int)bds.min.y;

                tex = new WebCamTexture(deviceName, mWeight, mHeight, 12);
                planeT.gameObject
                    .GetComponent<Renderer>().material
                    .mainTexture = tex;
                tex.Play();
            }
        }
    }
    /// <summary>
    /// 获取截图
    /// </summary>
    /// <param name="_filePath"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    private IEnumerator getTexture(string _filePath, byte _type)
    {
        yield return new WaitForEndOfFrame();

        Texture2D t = new Texture2D(mWeight, mHeight);

        t.ReadPixels(new Rect(mStarta, mStarty, mWeight, mHeight), 0, 0, false);

        t.Apply();
       
        if (0 == _type)
        {
            byte[] byt = t.EncodeToJPG();

            
            File.WriteAllBytes(_filePath +Time.time
                + ".jpg", byt);
            
          
           
        }

        if (1 == _type)
        {
            byte[] byt = t.EncodeToPNG();
            File.WriteAllBytes(_filePath, byt);
        }

        tex.Play();
    }

    /// <summary>
    /// 连拍
    /// </summary>
    /// <param name="_folder"></param>
    /// <param name="_type"></param>
    /// <returns></returns>
    private IEnumerator SeriousPhotes(string _folder, byte _type)
    {

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devives = WebCamTexture.devices;
            deviceName = devives[0].name;

            tex = new WebCamTexture(deviceName, 400, 300, 12);
            tex.Play();
        }
        //while (mIsSeriousCapture)
        //{
        //    yield return new WaitForEndOfFrame();

        //    Texture2D t = new Texture2D(mWeight, mHeight, TextureFormat.RGB24, true);
        //    t.ReadPixels(new Rect(mStarta, mStarty, Screen.width - 200, Screen.height ), 0, 0, false);


        //    t.Apply();

        //    if (0 == _type)
        //    {
        //        byte[] byt = t.EncodeToJPG();
        //        File.WriteAllBytes(_folder + Time.time.ToString().Split('.')[0] +"_"+ Time.time
        //            .ToString().Split('.')[1] + ".jpg", byt);
        //    }

        //    if (1 == _type)
        //    {
        //        byte[] byt = t.EncodeToPNG();
        //        File.WriteAllBytes(_folder + Time.time.ToString().Split('.')[0] +"_"+
        //            Time.deltaTime.ToString().Split('.')[1] + ".png", byt);
        //    }


        //    Thread.Sleep(300);
        //}
    }
}
