﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSoraBreath : Item
{
    float time = 0;
    public override void AfterTakeDamage(Damage damage)
    {
        time = Data[0];
    }
    public override void OnSustainedTrigger()
    {
        time -= 0.5f;
        if (time > 0)
            role.RecoverHealth(role.Properties.MaxHealth*0.01f*Data[1]);
    }
}
