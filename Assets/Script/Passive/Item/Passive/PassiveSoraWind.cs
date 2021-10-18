using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSoraWind : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<Dash>(out var dash))
        {
            dash.LimitSpeed += 20 * Data[0] * 0.01f;
        }
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<Dash>(out var dash))
        {
            dash.LimitSpeed += 20 * Data[0] * 0.01f;
        }
    }
}
