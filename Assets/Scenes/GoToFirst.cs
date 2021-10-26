using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToFirst : MonoBehaviour
{
    public Image Black;
    public Image You;
    float TimeCount = 0;
    bool awake = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (gameObject.transform.position.y > 540)
            awake = true;
        if (!awake)
            return;
        TimeCount += Time.deltaTime;
        if (TimeCount >= 2)
        {
            var c = You.color;
            c.a += Time.deltaTime / 2;
            You.color = c;
        }
        if (TimeCount >= 8)
        {
            var c = Black.color;
            c.a += Time.deltaTime / 2;
            Black.color = c;
        }
        if (TimeCount >= 11)
            SceneManager.LoadScene(0);

    }
}
