using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMove : MonoBehaviour
{
    public float mut = 1;
    void Update()
    {
        Lib.MoveTo(gameObject, new Vector2(transform.position.x, transform.position.y + 100 * Time.deltaTime));
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return))
            Lib.MoveTo(gameObject, new Vector2(transform.position.x, transform.position.y + 400 * Time.deltaTime));
    }
}
