using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItem : NPC
{
    private int id;
    private void Start()
    {
        id = GameManager.ItemPool.GetItem();
    }
    public override void OnTouch()
    {
        if (GameManager.AllMoney >= 5)
        {
            GameManager.AllMoney -= 5;
            Item.CreateItem(id, transform.position);
        }
        else
        {
            UiTextController.Add("没钱就别想着要了吧");
        }
    }
}
