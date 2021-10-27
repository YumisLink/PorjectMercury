using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveViolet : Item
{
    float HealthUp;
    public override void GetItem()
    {
        HealthUp = Data[0];
        role.BaseProperties.MaxHealth += HealthUp;
        role.Properties.MaxHealth += HealthUp;
        role.Health += HealthUp;
    }
}
