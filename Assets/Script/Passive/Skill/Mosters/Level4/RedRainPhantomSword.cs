using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainPhantomSword : Skill
{
    int cnt = 0;
    public override bool CanUse()
    {
        if (!role.Move.IsGround)
            return false;
        if (role.SkillState != "noon")
            return false;
        return true;
    }
    public override void Init()
    {
        AddAction(0.3f, PhantomSword);
        AddAction(0.6f, PhantomSword);
        AddAction(0.9f, PhantomSword);
    }
    public override void After()
    {
        cnt = 0;
    }
    public override void Before()
    {
        role.SetFaceToPlayer();
    }
    public void PhantomSword()
    {
        var eff = Effect.Create(GameManager.Effect[10], gameObject,Player.player.transform.position);
        eff.SetDamage(new Damage(Data[0]));
        eff.SetContinueAttack();
        eff.SetLater(0.5f);
    }
}
