using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBlessSpeed : Item
{
    public override void GetItem()
    {
        role.Properties.Move += Data[0];
    }
    public override void DiscardItem()
    {
        role.Properties.Move -= Data[0];
    }
}
