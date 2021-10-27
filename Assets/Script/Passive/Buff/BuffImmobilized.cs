using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定身+打断
/// </summary>
public class BuffImmobilized : Buff
{
    public override void GetBuff()
    {
        var v = role.Move.controller.velocity;
        v.x = 0;
        v.y = 0;
        role.Move.controller.velocity = v;
        role.SkillState = "Immobilized";
    }
    protected override void OnUpdate()
    {
        role.SkillState = "Immobilized";
    }
    public override void LoseBuff()
    {
        role.SkillState = Role.NormalState;
    }
}
