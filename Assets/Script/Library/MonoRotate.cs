using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRotate : MonoBehaviour
{
    /// <summary>
    /// 旋转
    /// </summary>
    public float XRotate;
    public float YRotate;
    public float ZRotate;
    public void Update()
    {
        //Lib.Rotate(gameObject, ZRotate * Time.deltaTime);
        var r = transform.eulerAngles;
        r.z += ZRotate * Time.deltaTime;
        transform.eulerAngles = r;


        transform.Rotate(new Vector3(XRotate * Time.deltaTime, YRotate * Time.deltaTime));
        //g.x += XRotate * Time.deltaTime /360;
        //g.y += YRotate * Time.deltaTime / 360;
        //transform.rotation = g;
    }
}
