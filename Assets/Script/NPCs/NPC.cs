using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Player py;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !GameManager.IsStop)
            if (py!= null)
                OnTouch();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
            py = player;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
            py = null;
    }
    public virtual void OnTouch() { }
}
