using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRoyalPriest : Item
{
    public override float BeforeHealing(float heal)
    {
        return heal * 0.01f * Data[0];
    }
}
