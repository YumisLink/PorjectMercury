using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessTimeSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var attack))
        {
            attack.color = Colors.Time;
            attack.CoolDown -= 0.1f;
        }
        if (role.TryGetComponent<ActorRiderAttack>(out var k))
            k.color = Colors.Time;
        if (role.TryGetComponent<ActorSpearAttack>(out var k))
            k.color = Colors.Time;
    }
    public override void DiscardItem()
    {
        if (role.TryGetComponent<ActorRiderAttack>(out var k))
            k.color = Color.white;
        if (role.TryGetComponent<ActorSpearAttack>(out var k))
            k.color = Color.white;
        if (TryGetComponent<ActorSwordAttack>(out var attack))
        {
            attack.color = Color.white;
            attack.CoolDown += 0.1f;
        }
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        //if (damage.fromSkill == "Attack")
        //{
        //    damage.FinalDamage += damage.BaseDamage * 0.3f;
        //}
    }
}
