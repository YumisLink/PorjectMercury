using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShower : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt;
    void Start()
    {
        txt = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        txt.text = GameManager.AllMoney.ToString();
    }
}
