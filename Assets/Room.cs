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
    private Vector2 roomAt;
    public List<Role> AllMonsters = new List<Role>();
    public bool awake = false;
    public  List<Gatels> AllGates = new List<Gatels>();
    public Vector2 RoomCenter => roomAt + (Vector2)transform.position;
    bool GateIsOpen = true;
    void Start()
    {
        GateInit();
        foreach (var a in GetComponentInChildren<Transform>())
        {
            var j = (Transform)a;
            if (j.TryGetComponent<Environment>(out var k))
            {
                k.room = this; 
                k.SetPosition();
            }
        }
        if (AllMonsters.Count > 0)
        {
            CloseTheGate();
        }
        if (AllMonsters.Count == 0 && !GateIsOpen)
        {
            OpenTheGate();
        }
        foreach (var a in AllMonsters)
            a.gameObject.SetActive(false);
    }
    public void ReSetEnvironment()
    {
        foreach (var a in AllMonsters)
            a.gameObject.SetActive(true);
        foreach (var a in GetComponentInChildren<Transform>())
        {
            var j = (Transform)a;
            if (j.TryGetComponent<Environment>(out var k))
            {
                k.ReInit();
                j.gameObject.SetActive(true);
            }
            awake = false;
        }
    }
    public void LeaveEnviroment()
    {
        foreach (var a in GetComponentInChildren<Transform>())
        {
            var j = (Transform)a;
            if (j.TryGetComponent<Environment>(out var k))
            {
                j.gameObject.SetActive(false);
            }
        }
    }
    int tc = 0;
    private void FixedUpdate()
    {
        tc++;
        if (tc > 2 && awake)
        {
            ReSetEnvironment();
        }
        while (AllMonsters.Count > 0)
        {
            if (AllMonsters[0] == null)
                AllMonsters.RemoveAt(0);
            else if (AllMonsters[0].Health <= -0.1f)
            {
                GameManager.AllMoney += 2;
                AllMonsters[0].gameObject.GetComponent<Role>().Dead();
                //Destroy(AllMonsters[0].gameObject);
                AllMonsters.RemoveAt(0);
            }
            else
                break;
        }
        if (AllMonsters.Count == 0 && !GateIsOpen)
        {
            OpenTheGate();
        }
    }
    void GateInit()
    {
        if (LeftGate)
            LeftGate.room = this;
        if (RightGate)
            RightGate.room = this;
        if (UpRightGate)
            UpRightGate.room = this;
        if (UpLeftGate)
            UpLeftGate.room = this;
        if (DownRightGate)
            DownRightGate.room = this;
        if (DownLeftGate)
            DownLeftGate.room = this;

        if (LeftGate != null) AllGates.Add(LeftGate);
        if (RightGate != null) AllGates.Add(RightGate);
        if (UpRightGate != null) AllGates.Add(UpRightGate);
        if (UpLeftGate != null) AllGates.Add(UpLeftGate);
        if (DownRightGate != null) AllGates.Add(DownRightGate);
        if (DownLeftGate != null) AllGates.Add(DownLeftGate);
    }
    public void CloseTheGate()
    {
        foreach(var a in AllGates)
            a.gameObject.SetActive(false); 
        GateIsOpen = false;
    }
    public void OpenTheGate()
    {
        foreach (var a in AllGates)
            a.gameObject.SetActive(true);
        GateIsOpen = true;
        EndTheBattle();
    }
    public virtual void EndTheBattle() { }

}
