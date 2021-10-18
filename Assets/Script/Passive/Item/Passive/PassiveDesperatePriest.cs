using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDesperatePriest : Item
{
    public override float BeforeHealing(float heal)
    {
        return heal * Data[0] * 0.01f;
    }
}
