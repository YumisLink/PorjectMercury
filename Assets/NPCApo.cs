using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCApo : NPC
{
    public static NPCApo apo;
    private void Start()
    {
        apo = this;
    }
    public override void OnTouch()
    {
        UiTextController.Dead = true;
        UiTextController.Add("胜儿啊！你可算来啦！快过来…过来。");
        UiTextController.Add("你可知道最近咱这村里净闹怪事，现在这连藏书楼里也进了些脏东西！");
        UiTextController.Add("老身这身子骨也不利索了，你替我去扫扫这藏书阁，赶走那些不干净的，还这藏书阁一太平，可好？");
    }
}
