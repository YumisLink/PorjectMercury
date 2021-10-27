using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessBloodSword : Item
{
    public override void Init()
    {
        Type = ItemType.BlessAttack;
    }
    public override void GetItem()
    {
        if (role.TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Blood;
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Colors.Blood;
        if (role.TryGetComponent<ActorSpearAttack>(out var c))
            c.color = Colors.Blood;
        Rare = ItemRare.Rare;
        role.Properties.MaxHealth -= role.BaseProperties.MaxHealth *0.6f;
        role.Health = Mathf.Min(role.Properties.MaxHealth, role.Health);
    }
    public override void DiscardItem()
    {
        if (role.TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Color.white;
        if (role.TryGetComponent<ActorRiderAttack>(out var b))
            b.color = Color.white;
        if (role.TryGetComponent<ActorSpearAttack>(out var k))
            k.color = Color.white;
        role.Properties.MaxHealth += role.BaseProperties.MaxHealth * 0.6f;
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (damage.FinalDamage > 0 && damage.fromSkill == "Attack")
            role.RecoverHealth(damage.FinalDamage * 0.05f);
        Buff.GiveBuff(typeof(BuffBleeding), 3, role, target);
    }
}
