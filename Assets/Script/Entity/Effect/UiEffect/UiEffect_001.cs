using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEffect_001 : Effect
{
    Image sr;
    float TimeCount = 0;
    public override void Init()
    {
        base.Init();
        sr = GetComponent<Image>();
    }
    public override void OnUpdate()
    {
        TimeCount += Time.deltaTime;
        var s = sr.color;
        s.a -= Time.deltaTime * 1f;
        sr.color = s;
        if (TimeCount >= 1.5f)
        {
            UiTextController.Init();
            Destroy(gameObject);
        }
        base.OnUpdate();
    }
}
