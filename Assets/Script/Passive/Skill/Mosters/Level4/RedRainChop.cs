using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainChop : Skill
{
    GameObject AttackEffect = GameManager.Effect[3];
    public bool Moving = false;
    public int Arrow;
    public Vector2 v2d = new Vector2();

    public override void Init()
    {
        AddAction(0.6f, Atk);
        AddAction(0.8f, Stop);
        ReleaseTime = 1.3f;
        CoolDown = 1.5f;
        SkillState = "Chop";
        Arrow = 0;
    }
    public override void Before()
    {
        role.SetFaceToPlayer();
        role.anim.Play("RedRainReadyChop");
        role.anim.speed = 0.01f;
        Moving = false;
        if (Lib.GetPosision(gameObject, Role.PlayerGameObject).x > 0)
            v2d.x = 80;
        else
            v2d.x = -80;
        role.Move.controller.velocity = Vector2.zero;
    }
    public override bool CanUse()
    {
        if (role.SkillState != "noon")
            return false;
        if (CoolTime > 0)
            return false;
        return true;
    }
    public override void After()
    {
        role.Move.controller.velocity = Vector2.zero;
        Moving = false;
        role.anim.Play("RedRainIdle");
        if (Mathf.Abs(Lib.GetPosision(gameObject, Role.PlayerGameObject).x) <= 5)
        {
            GetComponent<RedRainStrike>().NextSkill();
        }
    }
    protected override void OnFixedUsing()
    {
        if (Moving)
        {
            role.Move.controller.velocity = v2d;
        }
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public void Atk()
    {
        role.anim.Play("RedRainChop");
        role.anim.speed = 1;
        Moving = true;
        var go = Effect.Create(AttackEffect, gameObject);
        Lib.SetMultScale(go.gameObject, 2,2);
        go.SetDamage(new Damage(100, DamageType.Normal));
        if (v2d.x < 0)
            Lib.SetFlipX(go.gameObject);
        go.SetFollow();
    }
    public void Stop()
    {
        Moving = false;
        role.Move.controller.velocity = Vector2.zero;
    }
}
