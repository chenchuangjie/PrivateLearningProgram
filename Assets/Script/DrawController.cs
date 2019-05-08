using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawController : MonoBehaviour
{

    // Pencil对象
    [SerializeField] GameObject Pencil;

    // 新建描绘标记
    public bool isPress;

    // 新建pencils数组
    List<GameObject> pencils = new List<GameObject>(0);

    void Update()
    {
        //创建鼠标点击相机屏幕的位置，z轴为距离10
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);

        // 点击鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            // 当鼠标左键按下后，允许描绘
            isPress = true;
            // Instantiate 实例化一个对象，就相当于复制/克隆一个对象
            // 克隆GameObject或Component时，所有子对象和组件也将被克隆，其属性设置与原始对象的属性相同。
            // Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            // Instantiate(需要复制对象, 新对象的位置, 新对象的方向, 将分配给新对象的父级);
            // Quaternion.Euler返回围绕z轴旋转z度，围绕x轴旋转x度，围绕y轴旋转y度的旋转
            // Camera.ScreenToWorldPoint :转变position从屏幕空间到世界空间。
            pencils.Add(Instantiate(Pencil, Camera.main.ScreenToWorldPoint(mousePos), Quaternion.Euler(Vector3.zero), this.transform));
        }
        // 描绘标记判断
        if (isPress)
        {
            //获取鼠标点击相机屏幕的坐标，设置当前描绘点的位置
            pencils[pencils.Count - 1].transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        }
        // 松开鼠标左键
        if (Input.GetMouseButtonUp(0))
        {
            // 当鼠标左键按下后，禁止描绘
            isPress = false;
        }

        // 点击键盘ESC键
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //清除所有描绘的点
            clearAllPencils();
        }
    }
    

    void clearAllPencils()
    {

        // 删除所有的点
        for(int i =0; i < pencils.Count; i++)
        {
            Destroy(pencils[i]);
        }
        //清除数组
        pencils.Clear();
    }
}
