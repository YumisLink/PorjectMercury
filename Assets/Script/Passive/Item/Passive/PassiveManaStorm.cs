using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PassiveManaStorm: Item
{
    public override void BeforeUseSkill(Skill skill)
    {
        if (skill.Type == SkillType.Skill || skill.Type == SkillType.State)
        {
            var eff = Effect.Create(GameManager.Effect[10],gameObject,Data[0],transform.position);
            eff.SetFollow();
            eff.SetDamage(new Damage(role.Properties.Attack * Data[1] * 0.01f,DamageType.True));
            eff.SetContinueAttack();
        }
    }
}