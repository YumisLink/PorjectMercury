using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initer_2 : MonoBehaviour
{
    private void Start()
    {
        var rm3 = GameManager.CreateRoom(GameManager.Rooms[1]);
        rm3.transform.position = Vector3.zero + new Vector3(22, 0, 0);
        rm3.awake = true;


        var rm4 = GameManager.CreateRoom(GameManager.Rooms[4]);
        rm4.transform.position = Vector3.zero + new Vector3(450, 0, 0);
        rm3.RightGate.LinkTo(rm4.LeftGate);

        // 6 7 5
        var rm5 = GameManager.CreateRoom(GameManager.Rooms[7]);
        rm5.transform.position = Vector3.zero + new Vector3(600, 0, 0);
        rm4.RightGate.LinkTo(rm5.LeftGate);

        var rm6 = GameManager.CreateRoom(GameManager.Rooms[6]);
        rm6.transform.position = Vector3.zero + new Vector3(750, 0, 0);
        rm5.RightGate.LinkTo(rm6.LeftGate);

        var rm7 = GameManager.CreateRoom(GameManager.Rooms[5]);
        rm7.transform.position = Vector3.zero + new Vector3(900, 0, 0);
        rm6.RightGate.LinkTo(rm7.LeftGate);

        GameManager.VirtualCamera.m_BoundingShape2D = rm3.Limit;
        GameManager.DeleteGate();
    }
}
