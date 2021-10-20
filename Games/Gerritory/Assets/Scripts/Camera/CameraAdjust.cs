using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
        SetCamera(5,4);
    }
    //width 和 height 是指 tiles 的寬和高，原本是整數，代表有幾個 tiles，
    //但因接下來，都是要做 float 的計算，我索性把他改以 float 傳入
    public void SetCamera( float height,float width)
    {
        //設定攝影機的螢幕高度的一半是有幾個 unit 單位，數值愈大，代表要顯示的範圍愈大，
        //不過物件也相對會變小了
        float alpha = 13.5f;
        float beta = 67.96f;
        float sizeY = width * Mathf.Cos(alpha * Mathf.Deg2Rad)
                      + height * Mathf.Cos(beta * Mathf.Deg2Rad);
        float sizeX = sizeY * 1080 / 1920;
        cam.orthographicSize = sizeX / 2;

        //設定攝影機位置
        float theta = 90 - alpha;
        float v = 180 - alpha - beta;
        //假定通過 board 中心座標的直線， 方程式 z=mx+b
        float m = Mathf.Tan(theta / v * Mathf.PI / 2);  //斜率
        float b = (height - m * width) / 2;
        //攝影機離 board 至少要 1 單位，但為了擔心太靠近 board 時，透視效果會被裁切，
        //所以改為 -1.5，不過 x 座標卻是視 board 的大小，機動移動位置的
        const float z = -3.0f;
        float x = (z - b) / m;
        cam.transform.position = new Vector3(x, height / 2, z);
    }
}
