using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem particle;
    float TimeCount = 0;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount >= 3)
        {
            Destroy(gameObject);
        }
    }
}
