using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTextShower : MonoBehaviour
{
    Text txt; 
    public float ShowerTime;
    void Start()
    {
        txt = GetComponent<Text>();
    }
    public void Show(float time,string str)
    {
        txt.text = str;
        ShowerTime = time;
    }
    void Update()
    {
        ShowerTime -= Time.deltaTime;
        if (ShowerTime >= 1)
        {
            var col = txt.color;
            col.a = 1;
            txt.color = col;
        }
        else
        {
            var col = txt.color;
            col.a = ShowerTime;
            txt.color = col;
        }
    }
}
