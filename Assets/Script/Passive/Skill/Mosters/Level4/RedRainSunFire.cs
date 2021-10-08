using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainSunFire : Skill
{
    Vector3 v3;
    public override void Before()
    {
        role.anim.Play("SeikaiNoITami");
        var k = transform.position;
        k.y += 3;
        transform.position = k;
        v3 = transform.position;
        var eff = Effect.Create(GameManager.Effect[14], gameObject, 12, transform.position,270);

        eff.SetDamage(new Damage(100,DamageType.True));
        eff.SetContinueAttack();
    }
    public override bool CanUse()
    {
        if (role.SkillState != "noon")
            return false;
        return true;
    }
    public override void After()
    {
        var t = role.Move.controller.velocity;
        t.x = 0;
        t.y = 0;
        role.Move.controller.velocity = t;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (SkillState != role.SkillState)
            return;
        transform.position = v3;
        var t = role.Move.controller.velocity;
        t.x = 0;
        t.y = 0;
        role.Move.controller.velocity = t;
        role.SetFaceToPlayer();
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    { 
        if (damage.fromSkill == "Sun")
        {
            target.HitBack(new Vector2(100*role.FaceTo,0));
        }
    }
    public override void AfterUseSkill(Skill skill)
    {
        role.anim.Play("Idle");
    }
}
