using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainGoBack : Skill
{
    public override bool CanUse()
    {
        GetComponent<RedRainChop>().AddSkillQueue();
        if (CoolTime > 0)
            return false;
        if (role.SkillState == "RedRainChop")
            return true;
        if (role.SkillState != "noon")
            return false;
        return true;
    }
    public override void Before()
    {
        role.anim.Play("GoBack");
    }
    protected override void OnFixedUpdate()
    {
        role.anim.speed = 1f;
        if (role.SkillState == SkillState)
        {
            var l = role.Move.controller.velocity;
            l.x = -40 * role.FaceTo;
            role.Move.controller.velocity = l;
        }
    }
    public override void After()
    {
        var l = role.Move.controller.velocity;
        l.x = 0;
        role.Move.controller.velocity = l;
    }
}
