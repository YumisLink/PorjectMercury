using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveShadowStuffing : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<Dash>(out var a))
        {
            a.InvTime += 0.1f;
        }
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<Dash>(out var a))
        {
            a.InvTime -= 0.1f;
        }
    }

}
