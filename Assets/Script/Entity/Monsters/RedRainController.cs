using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainController : Role
{
    public Room rm;
    public Vector2 v2dMove;
    public Skill Attack;
    public Skill Sun;
    public Skill Attack2;
    public Skill Blink;
    public Skill FireBall;
    public Skill PhantomSword;
    public Skill GoBack;
    public bool weak = false;
    public float NealTimeCount;
    public float FarTimeCount;
    public float VeryFarTimeCount;
    public float TimeCount;

    public override void Init()
    {
        base.Init();
        GoBack = gameObject.AddComponent<RedRainGoBack>();
        Attack = gameObject.AddComponent<RedRainChop>();
        Attack2 = gameObject.AddComponent<RedRainStrike>();
        Sun = gameObject.AddComponent<RedRainSunFire>();
        Blink = gameObject.AddComponent<RedRainBlink>();
        FireBall = gameObject.AddComponent<RedRainFireBall>();
        PhantomSword = gameObject.AddComponent<RedRainPhantomSword>();
        Health = 3800;
    }
    public override void OnUpdate()
    {
        
        var dis = Lib.GetPosision(gameObject, PlayerGameObject);
        var x = Mathf.Abs(dis.x);
        if (!weak)
        {
            var ds = dis.x * dis.x + dis.y * dis.y;
            if (ds <= 400)
            {
                weak = true;
                UiManager.Bosses.Add(this);
            }
        }
        if (!weak)
            return;
        if (Health <= 0)
            return;
        base.OnUpdate();
        TimeCount += Time.deltaTime;
        if (!Part0)
        {
            Part_Zero();
            return;
        }
        if (SkillState != "noon")
            return;
        if (x < 3)
            NealTimeCount += Time.deltaTime;
        if (x > 10)
            FarTimeCount += Time.deltaTime;
        if (x > 15)
            VeryFarTimeCount += Time.deltaTime;
        Part_One();
    }
    void Part_Zero()
    {
        Sun.WantSkill();
        if (TimeCount >= 12)
            Part0 = true;
    }
    void Part_One()
    {
        if (VeryFarTimeCount >= 0.8f)
        {
            Blink.WantSkill();
            VeryFarTimeCount = 0;
        }
        if (FarTimeCount >= 1.4f)
        {
            var a = Random.Range(0, 3);
            if (a == 0) { 
                FireBall.WantSkill();
                Blink.WantSkill();
            } 
            if (a == 1) Attack.WantSkill();
            if (a == 2) PhantomSword.WantSkill();
            FarTimeCount = 0;
        }
        if (NealTimeCount >= 1.4f)
        {
            if (Random.Range(0, 2) == 0)
                PhantomSword.WantSkill();
            else
                Attack.WantSkill();
            NealTimeCount = 0;
        }
        if (TimeCount >= 3.5f)
        {
            Attack.WantSkill();
            TimeCount = 0;
        }
    }
    bool Part0 = false;
}
