using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatels : MonoBehaviour
{
    public Gatels Link;
    public Role role = null;
    public Room room;
    private void Update()
    {
        if (role != null)
            if (Link!=null)
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameManager.VirtualCamera.m_BoundingShape2D = Link.room.Limit;
                    role.transform.position = Link.transform.position;
                }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("wocao");
        if (collision.gameObject.TryGetComponent<Player>(out var a))
        {
            role = a;
        }
    }
    public  void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var a))
        {
            role = null;
        }
    }
    public void LinkTo(Gatels gate)
    {
        Link = gate;
        gate.Link = this;
    }
}
