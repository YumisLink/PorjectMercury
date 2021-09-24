using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    float TimeCount = 0;
    bool isDead = false;
    
    public void Death()
    {
        isDead = true;
        GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        if (isDead)
            TimeCount += Time.deltaTime;
        if (TimeCount >= 3)
            Destroy(gameObject);
    }
}
