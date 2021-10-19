using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Player py;
    private Sprite shower;
    public SpriteRenderer sr;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !GameManager.IsStop)
            if (py!= null)
                OnTouch();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            py = player;
            var c = sr.color;
            c.a = 1;
            sr.color = c;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            py = null;
            var c = sr.color;
            c.a = 0;
            sr.color = c;
        }
    }
    public virtual void OnTouch() { }
}
