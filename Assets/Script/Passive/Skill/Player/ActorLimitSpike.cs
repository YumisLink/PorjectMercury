using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorLimitSpike : Skill
{
    ActorChangeFace Face;
    GameObject EffectAttack;
    float Att;
    int limit;
    int cnt = 0;
    bool CanNext = false;
    int face = 0;
    float GoTime = 0.0f;
    bool DoThis = false;
    public override void Init()
    {
        Face = GetComponent<ActorChangeFace>();
        EffectAttack = GameManager.Effect[4];
        Att = Data[0];
        limit = (int)Data[1];
        AddAction(0, Atk);
    }
    public override bool CanUse()
    {
        if (Face == null)
            return false;
        if (Face.state != 2)
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
        CanNext = false;
        cnt = 0;
        face = -role.FaceTo;
    }
    public override void OnUpdate()
    {
        if (SkillState != role.SkillState)
            return;
        if (CanNext && UsingTime >= 0.30f && cnt < limit && DoThis)
        {
            UsingTime = 0;
            cnt++;
            DoThis = false;
            Debug.Log(cnt);
        }
        if (cnt > 0  && CanNext && UsingTime >= 0.2 && !DoThis)
        {
            Atk();
        }
        GoTime -= Time.deltaTime;
        if (GoTime > 0)
        {
            var v = role.Move.controller.velocity;
            v.x = face * Mathf.Lerp(10,40,GoTime*5);
            if (cnt > 0)
                v.x *= 1.3f;
            v.y = 0;
            role.Move.controller.velocity = v;
        }
    }
    public void Atk()
    {
        face = face == 1 ? -1 : 1;
        if (face == 1)
            role.SetFaceToRight();
        if (face == -1)
            role.SetFaceToLeft();
        CanNext = false;
        GoTime = 0.2f;
        var eff = Effect.Create(EffectAttack, gameObject, transform.position);
        eff.SetFollow();
        if (role.FaceTo == -1)
            Lib.SetFlipX(eff.gameObject);
        eff.damage = new Damage(Att,DamageType.Normal);
        eff.damage.fromSkill = "ActorLimitSpike";
    }
    public override void BeforeFinalAttack(Damage damage, Role target)
    {
        if ((damage.FinalDamage >= damage.BaseDamage - 0.025f * damage.BaseDamage) && !CanNext)
        {
            CanNext = true;
            DoThis = true;
        }
    }
}
