using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainBlink : Skill
{
    Skill sk;
    public override void Init()
    {
        sk = GetComponent<RedRainStrike>();
        AddAction(0.2f, Blink);
    }
    public void Blink()
    {
        var v3 = Player.player.transform.position;
        role.MoveTo(new Vector2(v3.x + Player.player.FaceTo * -2,v3.y));
    }
    public override void After()
    {
        sk.NextSkill();
    }
    public override bool CanUse()
    {
        return true;
    }
}
