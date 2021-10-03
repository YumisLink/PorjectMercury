using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LS : MonoBehaviour
{
    private float TimeCount;
    private bool Trigger;
    void Start()
    {
        var a = transform.position;
        a.y += 30;
        transform.position = a;
    }
    
    void Update()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount > 1 && TimeCount < 2)
        {
            var a = transform.position;
            a.y -= 30 * Time.deltaTime;
            transform.position = a;
        }
        if (TimeCount >= 2 && !Trigger)
        {
            Trigger = true;
            Effect.Create(GameManager.Effect[11],null,transform.position);
        }
    }
}
