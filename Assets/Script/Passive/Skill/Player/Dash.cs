
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    private int MoveDirection;
    public float InvTime = 0.05f;
    public float LimitSpeed = 25;
    public override void Init()
    {
        ReleaseTime = 0.2f;
        CoolDown = 0.7f;
        SkillState = "Dash";
    }
    public override void Before()
    {
        MoveDirection = role.FaceTo;
        role.Move.GravityEffect = false;
        role.Move.CanMove = false;
        role.anim.Play("Rush");
    }
    public override bool CanUse()
    {
        if (role.SkillState != "noon")
            return false;
        if (CoolTime > 0)
            return false;
        return true;
    }
    protected override void OnFixedUpdate()
    {
        if (SkillState == role.SkillState)
        {
            role.InvisibleTime = Mathf.Max(role.InvisibleTime, InvTime);
        }
    }
    public override void After()
    {
        role.Move.CanMove = true;
        role.Move.GravityEffect = true;

        var Velocity = role.Move.controller.velocity;
        Velocity.x = MoveDirection * 8;
        role.Move.controller.velocity = Velocity;
    }
    public override void OnUpdate()
    {
        UsingTime += (1 - role.Properties.AttackSpeed) * Time.deltaTime;
    }
    protected override void OnFixedUsing()
    {
        var Velocity = role.Move.controller.velocity;
        role.Move.CanMove = false;
        role.Move.GravityEffect = false;
        Velocity.x = MoveDirection * LimitSpeed;
        Velocity.y = 0;
        role.Move.controller.velocity = Velocity;
    }
}
