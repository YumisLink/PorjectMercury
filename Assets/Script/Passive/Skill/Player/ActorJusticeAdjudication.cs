using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorJusticeAdjudication : Skill
{
    ActorChangeFace Face;
    GameObject EffectAttack;
    float Att;
    public override void Init()
    {
        Face = GetComponent<ActorChangeFace>();
        EffectAttack = GameManager.Effect[8];
        Att = Data[0];
        AddAction(0, Atk);
    }
    public override bool CanUse()
    {
        if (Face == null)
            return false;
        if (Face.state != 3)
            return false;
        if (CoolTime > 0)
            return false;
        if (role.SkillState != "noon")
            return false;
        if (!role.Move.IsGround)
            return false;
        return true;
    }
    public void Atk()
    {
        role.anim.Play("Attack");
        var ef = Effect.Create(EffectAttack,gameObject,transform.position);
        if (role.FaceTo == -1)
            Lib.SetFlipX(ef.gameObject);
        Lib.SetMultScale(ef.gameObject,2,2);
        ef.damage = new Damage(Att, DamageType.Normal);
        ef.damage.fromSkill = "ActorJusticeAdjudication";
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "ActorJusticeAdjudication")
        {
            var V = target.Move.controller.velocity;
            V.x += role.FaceTo * 30;
            target.Move.controller.velocity = V;
        }
    }








}
