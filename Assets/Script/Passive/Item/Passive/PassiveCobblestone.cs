using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCobblestone : Item
{
    public override void OnSustainedTrigger()
    {
        role.RecoverHealth(Data[0]);
    }
}
