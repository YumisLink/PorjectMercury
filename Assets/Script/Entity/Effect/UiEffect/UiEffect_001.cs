using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEffect_001 : MonoBehaviour
{
    Image sr;
    float TimeCount = 0;
    public float speed = 1;
    public  void Start()
    {
        sr = GetComponent<Image>();
        var s = sr.color;
        s.a =1;
        sr.color = s;
    }
    public  void Update()
    {
        TimeCount += Time.deltaTime;
        var s = sr.color;
        s.a -= Time.deltaTime * 1f * speed;
        sr.color = s;
        if (TimeCount >= 1.5f / speed)
        {
            UiTextController.Init();
            Destroy(gameObject);
        }
    }
}
