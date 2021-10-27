using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDreamBeak : Item
{
    public override void GetItem()
    {
        role.Properties.Critical -= Data[0];
    }
    public override void DiscardItem()
    {
        role.Properties.Critical += Data[0];
    }
    public override void BeforeFinalAttack(Damage damage, Role target)
    {
        if (!damage.Critical)
            damage.FinalDamage += Data[1] * 0.01f * damage.FinalDamage;
    }
}
