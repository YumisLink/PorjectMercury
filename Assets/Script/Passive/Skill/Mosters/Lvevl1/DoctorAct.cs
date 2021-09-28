using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorAct : Skill
{
    GameObject AttackEffect = GameManager.Effect[1];
    public bool Moving = false;
    public int Arrow;
    public bool jump = false;
    public Vector2 v2d = new Vector2();

    public override void Init()
    {
        AddAction(0.8f, Atk);
        AddAction(0.4f, StopJump);
        ReleaseTime = 1f;
        CoolDown = 1.5f;
        SkillState = "Act";
        Arrow = 0;
    }
    public override void Before()
    {
        role.SetFaceToPlayer();
        role.anim.Play("DoctorAct");
        role.anim.speed = 0.01f;
        Moving = false;
        jump = true;
        v2d.y = -16;
        if (Lib.GetPosision(gameObject, Role.PlayerGameObject).x > 0)
            v2d.x = 30;
        else
            v2d.x = -30;
        role.Move.GravityEffect = false;
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
        role.anim.Play("Idle");
        role.Move.GravityEffect = true;
    }
    protected override void OnFixedUsing()
    {
        if (Moving)
        {
            role.Move.controller.velocity = v2d;
        }
        if (jump)
            role.Move.controller.velocity = new Vector2(0, 8);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public void Atk()
    {
        role.anim.speed = 1;
        Moving = true;
        var go = Effect.Create(AttackEffect, gameObject, 0.15f);
        go.SetDamage(new Damage(100, DamageType.Normal));
        Lib.Bigger(go.gameObject, 1.5f, 1.5f);
        Lib.SetFlipY(go.gameObject);
        if (v2d.x < 0)
            Lib.SetFlipX(go.gameObject);
        if (v2d.x < 0)
            Lib.Rotate(go.gameObject, 10);
        else
            Lib.Rotate(go.gameObject, -10);
        go.SetFollow();
    }
    public void StopJump()
    {
        jump = false;
        role.Move.controller.velocity = Vector2.zero;
    }
}
