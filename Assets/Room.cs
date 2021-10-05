using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Role> enm = new List<Role>();
    public Gatels LeftGate;
    public Gatels RightGate;
    public Gatels UpRightGate;
    public Gatels UpLeftGate;
    public Gatels DownRightGate;
    public Gatels DownLeftGate;
    public PolygonCollider2D Limit;
    public Vector2 roomAt;
    public Vector2 RoomCenter => roomAt + (Vector2)transform.position;
    void Start()
    {

    }

    void Update()
    {
        
    }
}
