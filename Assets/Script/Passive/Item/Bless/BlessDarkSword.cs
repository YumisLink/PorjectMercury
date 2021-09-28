using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessDarkSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Dark;
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Colors.Dark;
        if (role.TryGetComponent<ActorSpearAttack>(out var c))
            c.color = Colors.Dark;
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
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "Attack")
            damage.BaseDamage += damage.BaseDamage * 0.5f;
    }
    private void Update()
    {
        if (role.Health/role.Properties.MaxHealth <= 0.25f)
            DestroyItem();
    }
}
