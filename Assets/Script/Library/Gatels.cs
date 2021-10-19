using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gatels : MonoBehaviour
{
    public Gatels Link;
    public Role role = null;
    public Room room;
    public int TPs = -1;
    private void Update()
    {
        if (role != null)
            if (Link!=null)
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    room.LeaveEnviroment();
                    GameManager.VirtualCamera.m_BoundingShape2D = Link.room.Limit;
                    role.transform.position = Link.transform.position;
                    Link.room.ReSetEnvironment();
                    Sound.Play(GameManager.Audio[3]);
                }
        if (role != null)
            if (Input.GetKeyDown(KeyCode.UpArrow))
                if (TPs != -1)
                {
                    SceneManager.LoadScene(TPs);
                    return;
                }   
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
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
