using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUndeadTotem : Item
{
    public override void AfterTakeDamage(Damage damage)
    {
        if (role.Health <= 0)
        {
            role.Health = role.Properties.MaxHealth * Data[0] * 0.01f;
            DestroyItem();
        }
    }
}
