using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessSpaceSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<Attack>(out var a))
        {
            a.color = Colors.Space;
            a.BiggerAttack += 0.3f;
        }
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<Attack>(out var a))
        {
            a.color = Color.white;
            a.BiggerAttack -= 0.3f;
        }
    }
}
