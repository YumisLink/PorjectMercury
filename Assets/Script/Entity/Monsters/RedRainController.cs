using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainController : Role
{
    public Skill Attack;
    public Skill Attack2;
    public Vector2 v2dMove;
    bool a = true;
    public override void Init()
    {
        base.Init();
        UiManager.Bosses.Add(this);
        Attack = gameObject.AddComponent<RedRainChop>();
        Attack2 = gameObject.AddComponent<RedRainStrike>();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (a)
        {
            Attack.WantSkill();
            a = false;
        }
    }
}
