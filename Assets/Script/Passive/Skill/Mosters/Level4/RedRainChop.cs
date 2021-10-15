using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRainChop : Skill
{
    GameObject AttackEffect = GameManager.Effect[1];
    public bool Moving = false;
    public int Arrow;
    public Vector2 v2d = new Vector2();

    public override void Init()
    {
        AddAction(0.7f, Atk);
        AddAction(0.9f, Stop);
        Arrow = 0;
    }
    public override void Before()
    {
        if (Random.Range(0,5) <= 1)
            GetComponent<RedRainGoBack>().WantSkill();
        role.SetFaceToPlayer();
        role.anim.Play("ChopReady");
        Moving = false;
        if (Lib.GetPosision(gameObject, Role.PlayerGameObject).x > 0)
            v2d.x = 80;
        else
            v2d.x = -80;
        role.Move.controller.velocity = Vector2.zero;
    }
    public override bool CanUse()
    {
        if (role.SkillState != "noon")
            return false;
        if (CoolTime > 0)
            return false;
        return true;
    }
    public override void After()
    {
        role.Move.controller.velocity = Vector2.zero;
        Moving = false;
        role.anim.Play("RedRainIdle");
        if (Mathf.Abs(Lib.GetPosision(gameObject, Role.PlayerGameObject).x) <= 5)
        {
            GetComponent<RedRainStrike>().NextSkill();
        }
    }
    protected override void OnFixedUsing()
    {
        if (Moving)
        {
            role.Move.controller.velocity = v2d;
        }
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public void Atk()
    {


        role.anim.Play("Chop");
        role.anim.speed = 1;
        Moving = true;
        var go = Effect.Create(AttackEffect, gameObject);
        Lib.SetMultScale(go.gameObject, 1.3f,1.3f);
        go.SetDamage(new Damage(100, DamageType.Normal));
        go.damage.damageEffect = DamageEffect.katana;
        if (v2d.x < 0)
            Lib.SetFlipX(go.gameObject);
        go.SetFollow();
    }
    public void Stop()
    {
        float m = Player.player.transform.position.x;
        float sf = transform.position.x;
        if (role.FaceTo == 1 && m > sf || role.FaceTo == -1 && m < sf)
        {
            role.SkillState = "noon";
            After();
            role.AfterUseSkill(this);
            GetComponent<RedRainBlink>().WantSkill();
        }
        Moving = false;
        role.Move.controller.velocity = Vector2.zero;
    }
}
