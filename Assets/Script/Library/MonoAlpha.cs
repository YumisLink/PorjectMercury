using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoAlpha : MonoBehaviour
{
    public float StartTime;
    public float time;
    private float become;
    private SpriteRenderer rd;
    private float TimeCount;
    private void Start()
    {
        rd = GetComponent<SpriteRenderer>();
        become = 1 / time;
    }
    void Update()
    {
        TimeCount += Time.deltaTime;
        if (StartTime > TimeCount)
            return;
        var c = rd.color;
        c.a -= become * Time.deltaTime ;
        rd.color = c;

    }
}
