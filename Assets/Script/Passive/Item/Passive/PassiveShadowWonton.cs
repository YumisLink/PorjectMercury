using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveShadowWonton : Item
{
    public override void BeforeUseSkill(Skill skill)
    {
        if (skill.GetType() == typeof(Dash))
        {
            var eff = Effect.Create(GameManager.Effect[4],gameObject);
            eff.SetFollow();
            eff.SetDamage(new Damage(role.Properties.Attack * Data[0] * 0.01f,DamageType.True));
            Lib.SetMultScale(eff.gameObject, 0.1f, 0.1f);
        }
    }
}
