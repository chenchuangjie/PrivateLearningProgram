using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Loader : MonoBehaviour
{
    [SerializeField] string imgPath, settingPath;
    [SerializeField] RawImage rawimage;
    [SerializeField] RectTransform rawImageRect;

    public bool changeImage, resizeImg;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadSetting());
    }

    IEnumerator loadSetting()//读取文档中的设定
    {
        UnityWebRequest ur = UnityWebRequest.Get(settingPath);
        yield return ur.SendWebRequest(); //等待加载完成
        if(ur.isHttpError || ur.isNetworkError)
        {
            Debug.Log(ur.error); //如果网络出现问题就会报错
        }
        else
        {
            //downloadHandler 管理和处理从远程服务器接收的HTTP响应正文数据。
            string[] lines = ur.downloadHandler.text.Split('\n'); //用换行分割string
            changeImage = bool.Parse(lines[0].Replace("changeImg:", "")); //去掉前面无用的文字
            resizeImg  = bool.Parse(lines[1].Replace("resizeImg:", ""));

        }
        yield return loadImg(); //设定载入完成呼叫载入图片
    }
    IEnumerator loadImg()
    {
        //UnityWebRequest 一个UnityWebRequest正确配置下载图像并将其转换为纹理。 注意：仅支持JPG和PNG格式。
        //UnityWebRequestTexture.GetTexture 创建一个UnityWebRequest，用于通过HTTP GET下载图像，并根据检索到的数据创建一个Texture。
        UnityWebRequest ur = UnityWebRequestTexture.GetTexture(imgPath); //取texture要用UnityWebRequestTexture
        yield return ur.SendWebRequest();  //等待加载完成
        if (ur.isHttpError || ur.isNetworkError)
        {
            Debug.Log(ur.error); //如果网络出现问题就会报错
        }
        else
        {
            // 一个DownloadHandler子类，专门用于下载用作Texture对象的图像。
            //DownloadHandlerTexture的texture属性：返回下载的纹理，或null。（只读）
            // 管理和处理从远程服务器接收的HTTP响应正文数据。
            Texture loadTexture = ((DownloadHandlerTexture)ur.downloadHandler).texture;

            // 设置贴图
            if (changeImage)
                rawimage.texture = loadTexture;
            //设置大小
            if (resizeImg)
                rawImageRect.sizeDelta = new Vector2(loadTexture.width, loadTexture.height);

        }
    }
}
