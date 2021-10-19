using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var k))
        {
            Execute(k);
            Destroy(gameObject);
        }
    }
    protected virtual void Execute(Role k){ }
}
