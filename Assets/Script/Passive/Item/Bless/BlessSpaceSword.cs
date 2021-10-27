using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessSpaceSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
        {
            a.color = Colors.Space;
            a.BiggerAttack += 0.3f;
        }
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Colors.Space;
        if (role.TryGetComponent<ActorSpearAttack>(out var c))
            c.color = Colors.Space;
    }
    public override void DiscardItem()
    {
        if (role.TryGetComponent<ActorRiderAttack>(out var a))
            a.color = Color.white;
        if (role.TryGetComponent<ActorSpearAttack>(out var b))
            b.color = Color.white;
        if (TryGetComponent<ActorSwordAttack>(out var c))
        {
            a.color = Color.white;
            a.BiggerAttack -= 0.3f;
        }
    }
}
