using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStuffing : NPC
{
    bool Touch = false;
    public override void OnTouch()
    {
        if (!Touch)
        {
            UiTextController.Add("小兄弟，买包子吗......");
            UiTextController.Add("你是要去塔里冒险的吗。");
            UiTextController.Add("罢了，这包子当送你了，我们也希望这个塔楼的怪物能早点被除掉，要不这里都没人敢来了。");
            int id = Random.Range(43, 45);
            Item.CreateItem(id,transform.position);
            Touch = true;
        }
        else
        {
            UiTextController.Add("好了快去塔里冒险吧，如果你又出现了，在给你一个包子罢了。");
        }
    }
}
