using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItem : NPC
{
    private int id;
    private SpriteRenderer sprite;
    private void Start()
    {
        id = GameManager.ItemPool.GetItem();
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = GameManager.ItemImage[id];
    }
    public override void OnTouch()
    {
        if (GameManager.AllMoney >= 5)
        {
            GameManager.AllMoney -= 5;
            Item.CreateItem(id, transform.position);
            Destroy(gameObject);
        }
        else
        {
            UiTextController.Add("没钱就别想着要了吧");
        }
    }
}
