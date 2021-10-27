using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainFireBall : Skill
{
    int cnt = 0;
    public override bool CanUse()
    {
        if (!role.Move.IsGround)
            return false;
        return true;
    }
    public override void Init()
    {
        AddAction(0.3f, FireBallSummer);
        AddAction(0.4f, FireBallSummer);
        AddAction(0.5f, FireBallSummer);
    }
    public override void After()
    {
        cnt = 0;
    }
    public override void Before()
    {
        role.SetFaceToPlayer();
        role.anim.Play("Mage");
    }
    public void FireBallSummer()
    {
        cnt++;
        var eff = Effect.Create(GameManager.Effect[16],gameObject,new Vector2(transform.position.x+ 4 * role.FaceTo + 2*cnt * role.FaceTo,transform.position.y + 2*2*cnt));
        eff.SetMove(Lib.GetAngle(eff.gameObject,Player.player.gameObject),0,5);
        eff.SetDamage(new Damage(100));
        eff.SetDirectionFollowMove();
    }
}
