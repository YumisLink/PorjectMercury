using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveYin : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        damage.type = DamageType.True;
        //damage.type = DamageType.Normal 
    }
}
