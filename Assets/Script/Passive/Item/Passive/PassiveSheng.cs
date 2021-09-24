using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSheng : Item
{
    Properties del;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            DestroyItem();
    }
    public override void GetItem()
    {
        Properties add = Properties.Zero();
        add.Attack = Data[0];
        add.MaxHealth = Data[1];
        add.Move = Data[2];
        add.RangeChange = Data[3];
        add.AttackSpeeds = Data[4];
        del = add;
        role.Properties += del;
    }
    public override void DiscardItem()
    {
        role.Properties -= del;
    }
}
