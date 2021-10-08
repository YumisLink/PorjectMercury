using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainStrike : Skill
{
    readonly GameObject AttackEffect = GameManager.Effect[15];
    public bool Moving = false;
    public int Arrow;
    public Vector2 v2d = new Vector2();

    public override void Init()
    {
        AddAction(0.2f, Atk);
        ReleaseTime = 0.6f;
        CoolDown = 1.5f;
        SkillState = "Strike";
        Arrow = 0;
    }
    public override void Before()
    {
        role.anim.Play("Strike");
        role.SetFaceToPlayer();
        role.anim.speed = 0.01f;
        Moving = false;
        if (Lib.GetPosision(gameObject, Role.PlayerGameObject).x > 0)
            v2d.x = 20;
        else
            v2d.x = -20;
        role.Move.controller.velocity = Vector2.zero;
    }
    public override bool CanUse()
    {
        if (CoolTime > 0)
            return false;
        if (role.SkillState == "noon")
            return true;
        if (role.SkillState == "Chop")
            return true;
        if (role.SkillState != "noon")
            return false;
        return true;
    }
    public override void After()
    {
        role.Move.controller.velocity = Vector2.zero;
        Moving = false;
        role.anim.Play("RedRainIdle");
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
        role.anim.Play("RedRainStrike");
        role.anim.speed = 1;
        Moving = true;
        var go = Effect.Create(AttackEffect, gameObject);
        Lib.SetMultScale(go.gameObject, 2, 2);
        go.SetDamage(new Damage(100, DamageType.Normal));
        if (v2d.x < 0)
            Lib.SetFlipX(go.gameObject);
        go.SetFollow();
    }
}