using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSoraLight : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        damage.FinalDamage += (role.Health / role.Properties.MaxHealth) * Data[0] * 1f;
    }
}
