using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterSummonDoctor : Room
{
    bool getch = false;
    public override void EndTheBattle()
    {
        if (getch)
            return;
        var role = GameManager.CreateEntity(GameManager.Roles[2]);
        role.transform.position = RoomCenter;
        AllMonsters.Add(role);
        CloseTheGate();
        getch = true;
    }
}
