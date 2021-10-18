using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PassiveShuriken : Item
{
    public override void AfterUseSkill(Skill skill)
    {
        if (skill.Type == SkillType.Attack)
        {
            var eff = Effect.Create(GameManager.Effect[9],gameObject,10,transform.position);
            eff.SetMove(new Vector2(15 * role.FaceTo,0));
            eff.SetDamage(new Damage(role.Properties.Attack * Data[0] * 0.01f,DamageType.Normal));
            if (role.FaceTo == -1)
                Lib.SetFlipX(eff.gameObject);
            eff.Type = EffectType.Missile;
            eff.SetTriggerTimes(1);
        }
    }
}