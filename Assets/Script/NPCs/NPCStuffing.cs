using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStuffing : NPC
{
    bool Touch = false;
    public override void OnTouch()
    {
        if (GameManager.AllMoney >= 5)
        {
            GameManager.AllMoney -= 5;
            UiTextController.Add("多谢惠顾");
            int id = Random.Range(43, 45);
            Item.CreateItem(id,transform.position);
            Touch = true;
        }
        else
        {
            UiTextController.Add("没钱，就只能回家！");
        }
    }
}
