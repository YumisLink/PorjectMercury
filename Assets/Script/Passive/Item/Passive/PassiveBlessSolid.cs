using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBlessSolid : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
        {
            a.IgnoreHitBack ++;
        }
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
        {
            a.IgnoreHitBack --;
        }
    }
}
