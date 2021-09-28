using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorRiderAttack : Skill
{
    bool RushAttack = false;
    int attackFace = 0;
    int hitplace = 0;
    /// <summary>
    /// 是否无视击退
    /// </summary>
    public int IgnoreHitBack = 0;
    /// <summary>
    /// 攻击的颜色
    /// </summary>
    public Color color = Color.white;
    /// <summary>
    /// 冲刺攻击的颜色
    /// </summary>    
    public Color RushColor = Color.white;
    /// <summary>
    /// 攻击大小增强
    /// </summary>
    public float BiggerAttack = 1;
    /// <summary>
    /// 冲刺攻击范围增强
    /// </summary>
    public float BiggerRushAttack = 1;
    private ActorChangeFace Face;
    private float AttackDamage;
    Effect eff;
    bool tip = false;
    public override void Init()
    {
        AddAction(0.15f, Atk);
        AttackDamage = Data[0] / 100;
        Face = GetComponent<ActorChangeFace>();
    }
    public override void OnUpdate()
    {
        if (Face == null)
            return;
        if (Face.state != 3)
            return;
        if (role.SkillState == "Dash" )
        {
            if (Player.player.dash.UsingTime >= 0)
            {
                this.NextSkill();
            }
        }
        if (SkillState != role.SkillState)
            return;
        if (eff != null)
            if (eff.list.Count > 0)
                tip = true;
        if (hitplace == 1)
        {
            if (role.Move.IsGround || tip)
            {
                eff.GetComponent<Animator>().speed = 1;
                var V = role.Move.controller.velocity;
                role.Move.controller.velocity = V;
                tip = true;
            }
            else
            {
                eff.Duration = 0.2f;
                UsingTime = 0.15f;
                var V = role.Move.controller.velocity;
                V.y = -18;
                role.Move.controller.velocity = V;
            }
        }
        if (RushAttack)
        {
            var V = role.Move.controller.velocity;
            V.x = attackFace * 12;
            if ((role.Move.IsGround || tip) && UsingTime >= 0.15f)
            {
                eff.GetComponent<Animator>().speed = 1;
                tip = true;
            }
            else if (UsingTime >= 0.15f)
            {
                eff.Duration = 0.2f;
                UsingTime = 0.15f;
                V.y -= 160 * Time.deltaTime;
            }
            else
            {
                V.y += 160 * Time.deltaTime;
            }
            role.Move.controller.velocity = V;
        }
    }
    public override void Before()
    {
        role.Move.GravityEffect = false;
        tip = false;
        attackFace = role.FaceTo;
        role.anim.Play("Attack");
        RushAttack = false;
        hitplace = 0;
        if (role.SkillState == "Dash")
        {
            role.anim.Play("DashAttack");
            RushAttack = true;
            role.Move.CanMove = true;
            role.Move.GravityEffect = true;
            var Velocity = role.Move.controller.velocity;
            Velocity.x = 0;
            Velocity.y = 0;
            role.Move.controller.velocity = Velocity;
        }
        else
        {
            var Velocity = role.Move.controller.velocity;
            Velocity.x = 0;
            role.Move.controller.velocity = Velocity;
        }

    }
    public override void After()
    {
        role.Move.GravityEffect = true;
    }
    public override bool CanUse()
    {
        if (Face == null)
            return false;
        if (Face.state != 3)
            return false;
        if (CoolTime > 0)
            return false;
        if (role.SkillState == "Dash")
            return true;
        if (role.SkillState != "noon")
            return false;
        return true;
    }
    public override void OnSucceedAttack(Entity entity)
    {
        if (role.SkillState != SkillState)
            return;
        if (hitplace != 0)
        {
            var Velocity = role.Move.controller.velocity;
            Velocity.y = 0;
            Velocity.x = 0;
            role.Move.controller.velocity = Velocity;
            if (hitplace == 1)
                role.GetComponent<Jump>().JumpCount = 1;
        }
        if (entity.TryGetComponent<Role>(out _))
            return;
        if (hitplace == 0)
        {
            var Velocity = role.Move.controller.velocity;
            Velocity.x = 0;
            role.Move.controller.velocity = Velocity;
        }
    }
    public void Atk()
    {
        if (RushAttack)
        {
            var go = Effect.Create(GameManager.Effect[6], gameObject, transform.position);
            go.SetFollow();
            go.GetComponent<SpriteRenderer>().color = RushColor;
            go.SetDamage(new Damage(AttackDamage * role.Properties.Attack, DamageType.Normal));
            go.damage.fromSkill = "RushAttack";
            Lib.SetMultScale(go.gameObject, BiggerRushAttack, BiggerRushAttack);
            Lib.SetMultScale(go.gameObject, role.Properties.Range, role.Properties.Range);
            Lib.SetMultScale(go.gameObject, 1.0f, 0.75f);
            if (role.FaceTo == -1)
                Lib.SetFlipX(go.gameObject);
            hitplace = -1;
            eff = go;
            eff.GetComponent<Animator>().speed = 0;
        }
        else
        {
            var go = Effect.Create(GameManager.Effect[6], gameObject, transform.position);
            eff = go;
            go.SetFollow();
            go.SetDamage(new Damage(AttackDamage * role.Properties.Attack, DamageType.Normal));
            go.damage.fromSkill = "Attack";
            Lib.SetMultScale(go.gameObject, BiggerAttack, BiggerAttack);
            Lib.SetMultScale(go.gameObject, role.Properties.Range, role.Properties.Range);
            Lib.SetMultScale(go.gameObject, 0.75f, 1);

            go.GetComponent<SpriteRenderer>().color = color;
            hitplace = 0;
            if (!role.Move.IsGround)
            {
                if (attackFace == -1)
                    Lib.SetFlipX(go.gameObject);
                hitplace = 1;
                eff.GetComponent<Animator>().speed = 0;
            }
            if (hitplace == 0 && attackFace == -1)
                Lib.SetFlipX(go.gameObject);
        }

    }
}
