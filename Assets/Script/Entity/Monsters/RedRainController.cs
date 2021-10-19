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
    public float MilTimeCount;
    public float FarTimeCount;
    public float VeryFarTimeCount;
    public float HigherTimeCount;
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
    bool p2 = false;
    public override void OnUpdate()
    {
        if (Health<= 0.5f && !p2)
        {
            p2 = true;
            var eff = Effect.Create(GameManager.Effect[17],gameObject,transform.position);
            eff.SetDamage(new Damage(0, DamageType.True));
            eff.damage.fromSkill = "Sun";
        }
        var dis = Lib.GetPosision(gameObject, PlayerGameObject);
        var x = Mathf.Abs(dis.x);
        var y = dis.y;
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
        if (FaceTo == 1)
            Move.Go(-1);
        if (FaceTo == -1)
            Move.Go(1);
        if (y > 3)
            HigherTimeCount += Time.deltaTime;
        if (x < 6)
            NealTimeCount += Time.deltaTime;
        if (x > 6 && x < 10)
            MilTimeCount += Time.deltaTime;
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
        if (VeryFarTimeCount >= 1.5f)
        {
            Blink.WantSkill();
            VeryFarTimeCount = 0;
        }
        if (FarTimeCount >= 1.4f)
        {
            var a = Random.Range(0, 4);
            if (a == 0) { 
                FireBall.WantSkill();
                Blink.WantSkill();
            } 
            if (a == 1) Attack.WantSkill();
            if (a == 2) PhantomSword.WantSkill();
            FarTimeCount = 0;
        }
        if (MilTimeCount >= 1.4f)
        {
            var a = Random.Range(0, 3);
            if (a == 0)
            {
                FireBall.WantSkill();
                Blink.WantSkill();
            }
            if (a == 2) PhantomSword.WantSkill();
            MilTimeCount = 0;
        }
        if (HigherTimeCount >= 1.4f)
        {
            var a = Random.Range(0, 2);
            if (a == 0)
            {
                FireBall.WantSkill();
                Blink.WantSkill();
            }
            if (a == 1) PhantomSword.WantSkill();
            HigherTimeCount = 0;
        }
        if (NealTimeCount >= 1.4f)
        {
            int a = Random.Range(0, 3);
            if (a == 0)
                PhantomSword.WantSkill();
            if (a == 1)
                Attack.WantSkill();
            NealTimeCount = 0;
        }
        if (TimeCount >= 2.8f)
        {
            Attack.WantSkill();
            TimeCount = 0;
        }
    }
    bool Part0 = false;
}
