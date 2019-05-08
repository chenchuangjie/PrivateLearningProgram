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
            string[] lines = ur.downloadHandler.text.Split('\n'); //用换行分割string
            changeImage = bool.Parse(lines[0].Replace("changeImg:", "")); //去掉前面无用的文字
            resizeImg  = bool.Parse(lines[1].Replace("resizeImg:", ""));

        }
        yield return loadImg(); //设定载入完成呼叫载入图片
    }
    IEnumerator loadImg()
    {
        UnityWebRequest ur = UnityWebRequestTexture.GetTexture(imgPath); //取texture要用UnityWebRequestTexture
        yield return ur.SendWebRequest();  //等待加载完成
        if (ur.isHttpError || ur.isNetworkError)
        {
            Debug.Log(ur.error); //如果网络出现问题就会报错
        }
        else
        {
            Texture loadTexture = ((DownloadHandlerTexture)ur.downloadHandler).texture;
            if (changeImage)
                rawimage.texture = loadTexture;

            if (resizeImg)
                rawImageRect.sizeDelta = new Vector2(loadTexture.width, loadTexture.height);

        }
    }
}
