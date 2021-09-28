﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessDarkSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Dark;
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Color.white;
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
