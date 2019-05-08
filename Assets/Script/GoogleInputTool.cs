using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleInputTool : MonoBehaviour
{
    [SerializeField] string url;
    public string text;
    [SerializeField] string itc;
    [SerializeField] string num;
    [SerializeField] string cp;
    [SerializeField] string cs;
    [SerializeField] string ie;
    [SerializeField] string oe;
    [SerializeField] string app;

    public void decodeToZhuyin()
    {
        Debug.Log("111");
        StartCoroutine(IenumDecodeToZhuYin(url + text + itc + num + cp + cs + ie + oe + app));
    }

    IEnumerator IenumDecodeToZhuYin(string apiUrl)
    {
        WWWForm form = new WWWForm();{
            form.AddField("text", text);
            form.AddField("itc", itc);
            form.AddField("num", num);
            form.AddField("cp", cp);
            form.AddField("cs", cs);
            form.AddField("ie", ie);
            form.AddField("oe", oe);
            form.AddField("app", app);
        }
        UnityWebRequest ur = UnityWebRequest.Post(apiUrl, "text=wo&itc=zh-t-i0-pinyin&num=11&cp=0&cs=1&ie=utf-8&oe=utf-8&app=demopage");
        Debug.Log(form.headers);
        yield return ur.SendWebRequest();
        if (ur.isHttpError || ur.isNetworkError)
        {
            Debug.Log("出问题："+ ur.error); //如果网络出现问题就会报错
        }
        else
        {
            Debug.Log("sss");
            Debug.Log(ur.downloadHandler.text);
        }
    }
}
