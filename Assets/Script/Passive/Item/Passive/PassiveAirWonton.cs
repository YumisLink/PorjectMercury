using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAirWonton : Item
{
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "Attack")
        if (Random.Range(0f,1f) <= 0.1f)
        {
            var eff = Effect.Create(GameManager.Effect[10],role.gameObject,10,transform.position);
            eff.SetDamage(new Damage(5, DamageType.Continue));
            eff.SetContinueAttack();
        }
    }
}
