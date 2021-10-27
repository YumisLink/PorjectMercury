using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDarkTransaction : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        damage.FinalDamage += damage.FinalDamage;
    }
    public override void BeforeTakeDamage(Damage damage)
    {
        damage.FinalDamage += damage.FinalDamage;
    }
}
