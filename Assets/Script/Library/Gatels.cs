using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatels : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var a))
        {
            var v2 = collision.transform.position;
            v2.x = 5.08f;
            v2.y = -34.25f;
            collision.transform.position = v2;

            GameManager.VirtualCamera.m_BoundingShape2D = GameManager.Pc2d;
        }
    }

}
