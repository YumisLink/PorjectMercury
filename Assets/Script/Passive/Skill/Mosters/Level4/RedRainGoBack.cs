using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainGoBack : Skill
{
    float TimeCount = 0;
    public override bool CanUse()
    {
        //GetComponent<RedRainChop>().AddSkillQueue();
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
    public override void AfterUseSkill(Skill skill)
    {
        TimeCount = 0;
        role.SetFaceToPlayer();
    }
    public override void BeforeUseSkill(Skill skill)
    {
        role.Stop();
        if (skill.SkillState != "RedRainGoBack" && SkillState != "RedRainStrike")
        {
            if (Random.Range(0,5) == 0)
            {
                this.WantSkill();
                skill.AddSkillQueue();
            }
        }
    }
    public override void OnUpdate()
    {
        if (SkillState == "noon")
            TimeCount += Time.deltaTime;
        if (TimeCount >= 0.6f)
        {
            this.WantSkill();
            TimeCount = 0;
        }
    }

}
