using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessVoidSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Void;
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Color.white;
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        Buff.GiveBuff(BuffType.VoidCorrosion, 1, role, target);
    }
}
