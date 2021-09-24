using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveJing : Item
{
    public int cnt = 9999;
    public override void BeforeTakeDamage(Damage damage)
    {
        if (cnt >= Data[0] * 2)
        {
            damage.FinalDamage = 0;
            cnt = 0;
        }
    }
    public override void OnSustainedTrigger()
    {
        cnt++;
    }
}
