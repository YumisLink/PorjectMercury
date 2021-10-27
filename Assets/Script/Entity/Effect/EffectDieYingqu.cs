using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDieYingqu : Effect
{
    SpriteRenderer sr;
    public override void Init()
    {
        base.Init();
        sr = GetComponent<SpriteRenderer>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        var Color = sr.color;
        Color.a = Mathf.Clamp(Lib.Fitting(0, 0.4f, Duration), 0, 1);
        sr.color = Color;
    }
}
