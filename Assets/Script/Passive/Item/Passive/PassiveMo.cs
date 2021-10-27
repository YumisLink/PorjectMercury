using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMo : Item
{
    public override void BeforeTakeDamage(Damage damage)
    {
        damage.BaseDamage -= 10;
    }
}
