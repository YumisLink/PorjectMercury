using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDarkDeath : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        var l = role.Health / role.Properties.MaxHealth;
        if (l <= 0.3f)
        {
            damage.FinalDamage += damage.BaseDamage * (1-Lib.Fitting(0,0.3f,l));
        }
    }
}
