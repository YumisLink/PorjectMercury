using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffBleeding : Buff
{
    public override void Init()
    {
        BuffName = "流血";
        IsStack = true;
        Type = BuffType.Bleeding;
    }
    public override void BeforeUseSkill(Skill skill)
    {
        if (skill is Jump || skill is Dash)
            return;
        Damage.DealDamage(new Damage(Stack, DamageType.Normal), null, role);
        Stack = Stack * 3 / 4;
    }
}
