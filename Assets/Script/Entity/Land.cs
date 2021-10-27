using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : Entity
{
    public bool WillDamage;
    public bool CanAttacked;
    public List<Entity> list = new List<Entity>();
    void OnCollisionEnter2D(Collision2D collision)
    {
        var ent = collision.gameObject.GetComponent<Entity>();
        list.Add(ent);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        var ent = collision.gameObject.GetComponent<Entity>();
        list.Remove(ent);
    }
}
