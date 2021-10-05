using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveStrongAttack : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        target.HitBack(new Vector2(role.FaceTo * 3,0));
    }
}
