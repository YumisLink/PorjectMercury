using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveYang : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "Attack")
            damage.type = DamageType.True;
    }
}
