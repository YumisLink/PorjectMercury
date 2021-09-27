using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDan : Item
{
    public override void GetItem()
    {
        float k = role.Properties.MaxHealth;
        k-=1;
        if (k > 0)
        {
            role.Properties.MaxHealth = 1;
            role.Health = 1;
            k /= 100;
            var a = Properties.Zero();
            a.Attack = k * Data[0];
            a.Move = k * Data[1];
            a.RangeChange = k  * Data[2];
            a.AttackSpeeds = k * Data[3];
            role.Properties += a;
        }
    }
    public override void AfterGetItem(Item item)
    {
        float k = role.Properties.MaxHealth;
        k -= 1;
        if (k > 1)
        {
            role.Properties.MaxHealth = 1;
            role.Health = 1;
            var a = Properties.Zero();
            k /= 100;
            a.Attack = k * Data[0];
            a.Move = k * Data[1];
            a.RangeChange = k * Data[2];
            a.AttackSpeeds = k * Data[3];
            role.Properties += a;
        }
    }
}
