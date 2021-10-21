using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initer_1 : MonoBehaviour
{
    private void Start()
    {
        var rm1 = GameManager.CreateRoom(GameManager.Rooms[2]);
        rm1.transform.position = Vector3.zero + new Vector3(27, -2, 0);
        rm1.awake = true;
        GameManager.inittr = rm1.LeftGate.transform.position;

        var rm2 = GameManager.CreateRoom(GameManager.Rooms[3]);
        rm2.transform.position = Vector3.zero + new Vector3(150, 0, 0);
        rm1.RightGate.LinkTo(rm2.LeftGate);
        GameManager.DeleteGate();

        rm2.RightGate.TPs = 2;
        GameManager.VirtualCamera.m_BoundingShape2D = rm1.Limit;
    }
}
