﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlessHolySword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Colors.Holy;
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<ActorSwordAttack>(out var a))
            a.color = Color.white;
    }
}