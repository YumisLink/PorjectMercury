using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveGodWeapon : Item
{
    float time = 0;
    public override void AfterUseSkill(Skill skill)
    {
        if (skill.Type == SkillType.Rush)
        {
            time = Data[0];
        }
    }
    private void Update()
    {
        time -= Time.deltaTime;
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (time > 0)
        {
            damage.FinalDamage += Data[1] * 0.01f *damage.BaseDamage;
        }
    }
}
