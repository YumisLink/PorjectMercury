using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreater : MonoBehaviour
{
    public int id = 0;
    public bool RD = false;
    private void Start()
    {
        if (RD)
            id = GameManager.ItemPool.GetItem();
        Item.CreateItem(id,transform.position);
        Destroy(this,1);
    }
}
