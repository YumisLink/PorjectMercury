using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSoraSpeed : Item
{
    public override void GetItem()
    {
        foreach(var a in role.passives)
        {
            var b = a as Skill;
            if (b != null)
            {
                if (b.Type == SkillType.Attack)
                {
                    b.CoolDown -= b.CoolDown * Data[0] * 0.01f;
                }
            }
        }
    }
}