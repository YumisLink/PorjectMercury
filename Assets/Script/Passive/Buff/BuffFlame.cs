using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFlame : Buff
{
    public override void Init()
    {
        Type = BuffType.Flame;
        BuffName = "灼烧";
        IsStack = true;
    }
    public override void OnSustainedTrigger()
    {
        Damage.DealDamage(new Damage(Stack, DamageType.True), null, role);
        Stack -= 1;
    }
}
