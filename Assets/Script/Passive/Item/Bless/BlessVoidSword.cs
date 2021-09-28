using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessVoidSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Void;
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Colors.Void;
        if (role.TryGetComponent<ActorSpearAttack>(out var c))
            c.color = Colors.Void;
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
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        Buff.GiveBuff(typeof(BuffVoidCorrosion), 1, role, target);
    }
}
