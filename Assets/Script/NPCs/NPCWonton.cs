using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWonton : NPC
{
    bool Touch = false;
    public override void OnTouch()
    {
        if (!Touch)
        {
            UiTextController.Add("诶，小伙子，你又来了。");
            UiTextController.Add("来一碗吧...");
            UiTextController.Add("阿姨这里就只能给你一点混沌了，路上小心啊...");
            int id = Random.Range(46, 48);
            if (id == 47) id++;
            Item.CreateItem(id, transform.position);
            Touch = true;
        }
        else
        {
            UiTextController.Add("哎，能不能醒来就全靠你了。");
        }
    }
}
