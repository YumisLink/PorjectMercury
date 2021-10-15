using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EasyAttack : Skill
{
    public override bool CanUse()
    {
        if (CoolTime > 0)
        {
            return false;
        }
        if (role.SkillState == Role.NormalState)
        {
            return true;
        }
        return false;
    }

    public override void Before()
    {
        base.Before();
        if (role.Move.IsGround)
        {
            role.Stop();
        }
    }

    public override void Init()
    {
        base.Init();
        AddAction(0.5f, OnAttack);
    }

    public void OnAttack()
    {
        var eff = Effect.Create(GameManager.Effect[1], gameObject);
        eff.SetDamage(new Damage(Data[0], DamageType.Normal));
        eff.SetFollow();
        if(role.FaceTo == -1)
        {
            Lib.SetFlipX(eff.gameObject);
        }
    }
}