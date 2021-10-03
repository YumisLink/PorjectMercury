using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoScale : MonoBehaviour
{
    public float XScale;
    public float YScale;
    public float ZScale;
    public float XScaleDelta;
    public float YScaleDelta;
    public float ZScaleDelta;
    void Update()
    {
        var a = transform.localScale;
        a.x += XScale * Time.deltaTime;
        a.y += YScale * Time.deltaTime;
        a.z += ZScale * Time.deltaTime;
        transform.localScale = a;
        XScale += XScaleDelta * Time.deltaTime;
        YScale += YScaleDelta * Time.deltaTime;

        ZScale += ZScaleDelta * Time.deltaTime;
    }
}
