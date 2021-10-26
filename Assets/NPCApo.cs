using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCApo : NPC
{
    public static Room ToLink;
    public static NPCApo apo;
    private int t = 0;
    public Gatels gs;
    public Room rm;
    private void Start()
    {
        apo = this;
        t = 0;
    }

    public override void OnTouch()
    {
        if (t == 1)
            return;
        t = 1;
        UiTextController.Dead = true;
        UiTextController.Add("胜儿啊！你过来，可是为了帮我？");
    }
}
