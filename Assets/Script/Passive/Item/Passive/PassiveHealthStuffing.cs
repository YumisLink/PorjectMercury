using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveHealthStuffing : Item
{
    public override void GetItem()
    {
        role.Properties.MaxHealth += role.BaseProperties.MaxHealth * 0.5f;
        role.RecoverHealth(role.Properties.MaxHealth);
    }
    public override void DiscardItem()
    {
        role.Properties.MaxHealth -= role.BaseProperties.MaxHealth * 0.5f;
        role.RecoverHealth(0);
    }
}
