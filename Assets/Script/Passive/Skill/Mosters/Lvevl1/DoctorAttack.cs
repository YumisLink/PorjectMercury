using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorAttack : Skill
{
    GameObject AttackEffect = GameManager.Effect[1];
    public bool Moving = false;
    public int Arrow;
    public Vector2 v2d = new Vector2();
    
    public override void Init()
    {
        AddAction(0.9f, Atk);
        ReleaseTime = 1.1f;
        CoolDown = 1.5f;
        SkillState = "Attack";
        Arrow = 0;
    }
    public override void Before()
    {
        role.SetFaceToPlayer();
        role.anim.Play("DoctorAttack");
        role.anim.speed = 0.01f;
        Moving = false;
        if (Lib.GetPosision(gameObject, Role.PlayerGameObject).x > 0)
            v2d.x = 40;
        else
            v2d.x = -40;
        role.Move.controller.velocity = Vector2.zero;
    }
    public override bool CanUse()
    {
        if (role.SkillState != "noon")
            return false;
        return true;
    }
    public override void After()
    {
        role.Move.controller.velocity = Vector2.zero;
        Moving = false;
        role.anim.Play("DoctorIdle");
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
        role.anim.speed = 1;
        Moving = true;
        var go = Effect.Create(AttackEffect,gameObject,0.15f);
        go.SetDamage(new Damage(100, DamageType.Normal));
        Lib.SetFlipY(go.gameObject);
        if (v2d.x < 0)
            Lib.SetFlipX(go.gameObject);
        go.SetFollow();
    }
}
