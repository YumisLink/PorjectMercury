using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSoraSun : Item
{
    public override void AfterTakeDamage(Damage damage)
    {
        role.RecoverHealth(Data[0]);
    }
}
