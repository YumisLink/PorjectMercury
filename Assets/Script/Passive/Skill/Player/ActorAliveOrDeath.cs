using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAliveOrDeath : Skill
{
    GameObject DIE;
    GameObject RushEffect;
    float damage;
    float Mut;
    ActorChangeFace Face;
    Effect eff;
    bool IsRush;
    int SkillFace;
    public override void Init()
    {
        DIE = GameManager.Effect[7];
        RushEffect = GameManager.Effect[4];
        damage = Data[0] / 100;
        Mut = Data[1];
        Face = GetComponent<ActorChangeFace>();
        AddAction(0.05f, StartRush);
        AddAction(0.25f, EndRush);
        AddAction(0.85f, DealDamage);
    }
    public override bool CanUse()
    {
        if (Face == null)
            return false;
        if (Face.state != 1)
            return false;
        if (CoolTime > 0)
            return false;
        if (role.SkillState != "noon")
            return false;
        if (!role.Move.IsGround)
            return false;
        return true;
    }
    public override void Before()
    {
        IsRush = false;
        var V = role.Move.controller.velocity;
        V.x = 0;
        V.y = 0;
        role.Move.controller.velocity = V;
        SkillFace = role.FaceTo;
    }
    public override void OnUpdate()
    {
        if (SkillState != role.SkillState)
            return;
        if (IsRush)
        {
            var V = role.Move.controller.velocity;
            V.x = SkillFace * 50;
            V.y = 0;
            role.Move.controller.velocity = V;
        }
    }
    public void StartRush()
    {
        eff = Effect.Create(RushEffect, gameObject, 3f, transform.position + new Vector3(5*SkillFace,0));
        eff.GetComponent<Animator>().speed = 0;
        eff.damage = new Damage(0);
        Lib.SetMultScale(eff.gameObject,3,1);
        eff.damage.fromSkill = "AliveOrDeathReady";
        //eff.SetFollow();
        IsRush = true;
    }
    public void EndRush()
    {
        IsRush = false;
        Lib.SetMultScale(eff.gameObject, 0, 0);
        var V = role.Move.controller.velocity;
        V.x = 0;
        V.y = 0;
        role.Move.controller.velocity = V;
    }
    public void DealDamage()
    {
        foreach (var a in eff.list)
        {
            var d = new Damage(damage * role.Properties.Attack,DamageType.Normal);
            d.fromSkill = "AliveOrDeath";
            Damage.DealDamage(d,role,a.GetComponent<Role>());
        }
    }
    public override void BeforeFinalAttack(Damage damage, Role target)
    {
        if (damage.FinalDamage > damage.BaseDamage)
            if (damage.fromSkill == "AliveOrDeath")
            {
                GameManager.SpeedDownTime = 1;
                Effect.Create(DIE, gameObject, target.transform.position);
                damage.FinalDamage *= Mut;
            }
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "AliveOrDeathReady")
        {
            Buff.GiveBuff(typeof(BuffImmobilized),1,1,role,target);
        }
    }
}
