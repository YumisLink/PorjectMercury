using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class cLOSE : MonoBehaviour
{
    private float TimeCount = 0;
    public Image Black;
    public static bool awak = false;
    private void Update()
    {
        if (!awak)
            return;
        Black.gameObject.SetActive(true);
        TimeCount += Time.deltaTime;
        var c = Black.color;
        c.a += Time.deltaTime;
        Black.color = c;
        if(TimeCount >= 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
