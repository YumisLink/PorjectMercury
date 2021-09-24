using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveChou : Item
{
    public override void BeforeTakeDamage(Damage damage)
    {
        if (UnityEngine.Random.Range(0f, 1f) < 0.1f)
            damage.Miss = true;
    }
}
