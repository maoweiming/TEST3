using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using UnityEngine.UI;

public class QRcode : MonoBehaviour {

    //像素块
    public Color32[] data;
    //是否扫描
    private bool isScan;
    //图片
    public RawImage cameraTextrue;
    //现实内容
    public Text textQRcode;
    //摄像机
    private WebCamTexture webCamTexture;
    // ZXing  一个智能类解码位图对象中的条码
    private BarcodeReader barcodeReader;
    private float timer = 0;



    IEnumerator Start()
    {
       
        barcodeReader = new BarcodeReader();
        //调用外接摄像头
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            string devicename = devices[0].name;
            webCamTexture=new WebCamTexture(devicename,400,300);
           cameraTextrue.texture=webCamTexture;
            webCamTexture.Play();
            isScan=true;
        }
    }


    void Update()
    {
        if (textQRcode.text != null)
        {
            textQRcode.text += "";
            Start();
        }
        if (isScan)
        {
            timer += Time.deltaTime;
            //0.5秒扫描一次
            if (timer > 0.5f)
            {
                //开始协程扫描
                StartCoroutine(ScanQcod());

                //时间归零
                timer = 0;
            }
        }
    }

    /// <summary>
    /// 协程开始扫描
    /// </summary>
    /// <returns></returns>
    IEnumerator ScanQcod()
    {
        //获取Color32格式的像素颜色块。
        data = webCamTexture.GetPixels32();
        //方法接入宽高
        DecodeQR(webCamTexture.width, webCamTexture.height);
        //WaitForEndOfFrame
        //等待直到所有的摄像机和GUI被渲染完成后，在该帧显示在屏幕之前。
        yield return new WaitForEndOfFrame();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    private void DecodeQR(int width, int height)
    {
        var br = barcodeReader.Decode(data, width, height);
        if (br != null)
        {
       //     Debug.Log(br.Text);
            textQRcode.text = br.Text;

           
          //  isScan = false;
         //   webCamTexture.Stop();
        }
     
    }
}
