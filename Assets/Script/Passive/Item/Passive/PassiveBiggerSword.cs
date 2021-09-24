using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBiggerSword : Item
{
    public override void AfterGetItem(Item item)
    {
        base.AfterGetItem(item);
    }
    float coolDownAttack;
    public override void GetItem()
    {
        role.Properties.RangeChange += Data[0]/ 100 * role.BaseProperties.RangeChange;
        role.Properties.Attack += (Data[1]/100) * role.BaseProperties.Attack;
        role.Properties.AttackSpeeds -= Data[2];
        if (TryGetComponent<Attack>(out var a))
        {
            a.IgnoreHitBack++;
            coolDownAttack = a.CoolDown;
            a.CoolDown += 1.5f * coolDownAttack;
        }
    }
    public override void DiscardItem()
    {

        role.Properties.RangeChange -= Data[0] / 100;
        role.Properties.Attack -= Data[1] / 100;
        role.Properties.AttackSpeeds += Data[2];
        if (TryGetComponent<Attack>(out var a))
        {
            a.IgnoreHitBack--;
            a.CoolDown -= 1.5f * coolDownAttack;
        }
    }
}
