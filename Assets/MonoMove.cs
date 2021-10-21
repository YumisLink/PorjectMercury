using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoMove : MonoBehaviour
{
    void Update()
    {
        Lib.MoveTo(gameObject, new Vector2(transform.position.x, transform.position.y + 100 * Time.deltaTime));
    }
}
