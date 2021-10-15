using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRoyalMagicrafter : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        foreach (var a in role.passives)
        {
            var b = a as Skill;
            if (b != null)
            {
                b.CoolTime -= Data[0];
            }
        }
    }
}
