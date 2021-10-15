using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveShieldStuffing : Item
{
    public override void BeforeTakeDamage(Damage damage)
    {
        damage.FinalDamage -= 5;
    }
}
