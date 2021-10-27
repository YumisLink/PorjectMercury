using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessHolySword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Holy;
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Colors.Holy;
        if (role.TryGetComponent<ActorSpearAttack>(out var c))
            c.color = Colors.Holy;
    }
    public override void DiscardItem()
    {
        if (role.TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Color.white;
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Color.white;
        if (role.TryGetComponent<ActorSpearAttack>(out var c))
            c.color = Color.white;
    }
}
