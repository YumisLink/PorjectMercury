using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSwordAttack : Skill
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
    public override void Init()
    {
        AddAction(0.05f, Atk);
        AttackDamage = Data[0]/100;
        Face = GetComponent<ActorChangeFace>();
    }
    public override void Before()
    {
        attackFace = role.FaceTo;
        role.anim.Play("Attack"); 
        RushAttack = false;
        if (role.SkillState == "Dash")
        {
            role.anim.Play("DashAttack");
            RushAttack = true;

            role.Move.CanMove = true;
            role.Move.GravityEffect = true;
            var Velocity = role.Move.controller.velocity;
            if (Velocity.x != 0)
                Velocity.x = (Velocity.x / Mathf.Abs(Velocity.x)) * 8;
            else
                Velocity.x = attackFace * 8;
            role.Move.controller.velocity = Velocity;
        }
    }
    public override bool CanUse()
    {
        if (Face == null)
            return false;
        if (Face.state != 1)
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
            Velocity.y = 7 * hitplace * ((role.Properties.RangeChange/100-1) * 0.4f +1);
            role.Move.controller.velocity = Velocity;
            if (hitplace == 1)
                role.GetComponent<Jump>().JumpCount = 1;
        }
        if (entity.TryGetComponent<Role>(out _))
            return;
        if (hitplace == 0)
        {
            var Velocity = role.Move.controller.velocity;
            Velocity.x = -6 * attackFace;
            role.Move.controller.velocity = Velocity;
        }
    }
    public override void OnSucceedDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "Attack")
        {
            target.HitBack(new Vector2(2*attackFace,0));
        }
        if (IgnoreHitBack > 0)
            return;
        if (damage.fromSkill == "RushAttack")
            return;
        if (role.SkillState != SkillState)
            return;
        if (hitplace == 0)
        {
            var Velocity = role.Move.controller.velocity;
            Velocity.x = -6 * attackFace;
            role.Move.controller.velocity = Velocity;
        }
        if (hitplace != 0)
        {
            var Velocity = role.Move.controller.velocity;
            Velocity.y = 8 * hitplace;
            role.Move.controller.velocity = Velocity;
            if (hitplace == 1)
                role.GetComponent<Jump>().JumpCount = 1;
        }
    }
    public void Atk()
    {
        if (RushAttack)
        {
            var go = Effect.Create(GameManager.Effect[1], gameObject, transform.position);
            go.GetComponent<SpriteRenderer>().color = RushColor;
            go.SetDamage(new Damage(AttackDamage * role.Properties.Attack, DamageType.Normal));
            go.damage.fromSkill = "RushAttack";
            Lib.SetMultScale(go.gameObject, BiggerRushAttack, BiggerRushAttack);
            Lib.SetMultScale(go.gameObject, role.Properties.Range, role.Properties.Range);
            Lib.SetMultScale(go.gameObject, 1.5f, 0.75f);
            if (attackFace == -1) 
                Lib.SetFlipX(go.gameObject);
        }
        else
        {
            var go = Effect.Create(GameManager.Effect[1],gameObject, transform.position);
            go.SetDamage(new Damage(AttackDamage * role.Properties.Attack, DamageType.Normal));
            go.damage.fromSkill = "Attack";
            Lib.SetMultScale(go.gameObject, BiggerAttack, BiggerAttack);
            Lib.SetMultScale(go.gameObject, role.Properties.Range, role.Properties.Range);

            go.GetComponent<SpriteRenderer>().color = color;
            hitplace = 0;
            if (Input.GetKey(KeyCode.DownArrow) && !role.Move.IsGround)
            {
                Lib.Rotate(go.gameObject, 270);
                if (attackFace == 1)
                {
                    Lib.Rotate(go.gameObject,-180);
                    Lib.SetFlipX(go.gameObject);
                }
                hitplace = 1;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Lib.Rotate(go.gameObject, 90);
                if (attackFace == 1)
                {
                    Lib.Rotate(go.gameObject, 180);
                    Lib.SetFlipX(go.gameObject);
                }
                hitplace = -1;
            }
            if (attackFace == -1 && hitplace == 0)
                Lib.SetFlipX(go.gameObject);
        }
        
    }

}
