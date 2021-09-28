using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlessLuckSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Luck;
        if (role.TryGetComponent<ActorRiderAttack>(out var k))
            k.color = Colors.Luck;
        if (role.TryGetComponent<ActorSpearAttack>(out var k))
            k.color = Colors.Luck;
    }
    public override void DiscardItem()
    {
        if (role.TryGetComponent<ActorSwordAttack>(out var k))
            k.color = Color.white;
        if (role.TryGetComponent<ActorRiderAttack>(out var k))
            k.color = Color.white;
        if (role.TryGetComponent<ActorSpearAttack>(out var k))
            k.color = Color.white;
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "Attack")
            if (Random.Range(0f,1f) <= 0.5f)
                damage.Critical = true;
    }
}
