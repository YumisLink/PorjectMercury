using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRoyalHunter : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (Random.Range(0f,1f) <= Data[0] * 0.01f)
        {
            damage.FinalDamage += target.Properties.MaxHealth * Data[1] * 0.01f;
        }
    }
}
