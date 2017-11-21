using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZXing;
using ZXing.QrCode;
using System;
using System.IO;
using UnityEngine.UI;
using ZXing.Common;
using ZXing.Rendering;
public class BarcodeCam : MonoBehaviour
{


    /*
    public Texture2D encoded;//生成的二维码为Texture2D类型  
    public string Lastresult;//二维码中所包含的内容信息，我是使用了GUID进行代替  
    public int count = 5;//生成几个二维码  
     int  idKey;
    void Start()
    {

        encoded = new Texture2D(256, 256);
        for (int i = 0; i < count; i++)
        {
            idKey = 1111;
         //   Guid idKey = Guid.NewGuid();
            Lastresult = idKey.ToString();
            var textForEncoding = Lastresult;
            if (textForEncoding != null)
            {
                var color32 = Encode(textForEncoding, encoded.width, encoded.height);
                encoded.SetPixels32(color32);
                encoded.Apply();
                byte[] bytes = encoded.EncodeToPNG();//把二维码转成byte数组，然后进行输出保存为png图片就可以保存下来生成好的二维码  
                if (!Directory.Exists(Application.dataPath + "/AdamBieber"))//创建生成目录，如果不存在则创建目录  
                {
                    Directory.CreateDirectory(Application.dataPath + "/AdamBieber");
                }
                string fileName = Application.dataPath + "/AdamBieber/" + idKey + ".png";
                System.IO.File.WriteAllBytes(fileName, bytes);
            }
        }
    }


    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
         {
             Format = BarcodeFormat.QR_CODE,
             Options = new QrCodeEncodingOptions
             {
                 Height = height,
                 Width = width
             }
         };
        return writer.Write(textForEncoding);
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(100, 100, 256, 256), encoded);
    }

    */

    /*
    public Texture2D encoded;
    //指定字符串  
    public string QRCodes = "毛伟明";
    public RawImage QRImage;

    String er;
    void Start()
    {

        Dictionary<EncodeHintType, object> hits = new Dictionary<EncodeHintType, object>();
        hits.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
        ShowCode();
    }


    //定义方法生成二维码  
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        QrCodeEncodingOptions qe = new QrCodeEncodingOptions();

        qe.CharacterSet = "UTF-8";
        var writer = new BarcodeWriter
          {
              Format = BarcodeFormat.QR_CODE,
              Options = new QrCodeEncodingOptions
              {
                  Height = height,
                  Width = width
                 
                  
              }
          };
        return writer.Write(textForEncoding);
    }



    public void ShowCode()
    {



        encoded = new Texture2D(256, 256);
        //获取现在时间
        System.DateTime now = System.DateTime.Now;

        Debug.Log(now);
        //  QRCodes = now+"";


        var textForEncoding = QRCodes;
        if (textForEncoding != null)
        {
            //二维码写入图片  
            var color32 = Encode(textForEncoding, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            //重新赋值一张图，计算大小,避免白色边框过大  
            Texture2D encoded1;
            encoded1 = new Texture2D(190, 190);//创建目标图片大小  
            encoded1.SetPixels(encoded.GetPixels(32, 32, 190, 190));
            encoded1.Apply();
            QRImage.texture = encoded1;
        }
    }

     */
     public Texture2D encoded;  
  
   
 
     void Start()  
    {  
         //设置二维码大小  
        encoded = new Texture2D(512, 512);  
         //二维码边框  
        BitMatrix BIT;  
         //设置二维码扫描结果  
         string name = "毛为名—www-B747";  
  
         Dictionary<EncodeHintType, object> hints = new Dictionary<EncodeHintType, object>();  
  
        //设置编码方式  
        hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");  
   
        
        BIT = new MultiFormatWriter().encode(name, BarcodeFormat.QR_CODE, 512, 512, hints);  
         int width = BIT.Width;  
         int height = BIT.Width;

         if (name != null)
         {
         for (int x = 0; x < height; x++)  
         {  
            for (int y = 0; y < width; y++)  
            {  
               if (BIT[x, y])  
                {  
                     encoded.SetPixel(y, x, Color.black);  
                }  
                 else  
               {  
                    encoded.SetPixel(y, x, Color.white);  
               }  
  
             }  
        }

        
             encoded.Apply();
             byte[] bytes = encoded.EncodeToPNG();
             if (!Directory.Exists(Application.dataPath + "/AdamBieber"))//创建生成目录，如果不存在则创建目录  
             {
                 Directory.CreateDirectory(Application.dataPath + "/AdamBieber");
             }
             string fileName = Application.dataPath + "/AdamBieber/" +name+ ".png";
             System.IO.File.WriteAllBytes(fileName, bytes);
         }
        
  
    }  
   
    void OnGUI()  
    {  
        GUI.DrawTexture(new Rect(100, 100, 256, 256), encoded);  
     }  

     
}  
