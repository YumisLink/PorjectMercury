using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : Entity
{

    public static GameObject PlayerGameObject;
    public Renderer render;
    public string SkillState = "noon";
    public float Speed = 1;
    public CharacterController2D Move;
    public Queue<Skill> SkillQueue = new Queue<Skill>();
    public Animator anim;
    public float InvisibleTime = 0;
    public List<Passive> passives = new List<Passive>();
    public Queue<Passive> DeletePossiveQueue = new Queue<Passive>();
    public Skill nowSkill;
    protected bool die;
    /// <summary>
    /// 被攻击到了，变红的持续时间
    /// </summary>
    public float AttackedRed;
    /// <summary>
    /// 角色属性 
    /// </summary>
    public Properties Properties = Properties.Create();
    /// <summary>
    /// 基础属性
    /// </summary>
    public Properties BaseProperties = Properties.Create();
    /// <summary>
    /// 当前生命
    /// </summary>
    public float Health;
    /// <summary>
    /// 当前精神
    /// </summary>
    public float Spirit;
    /// <summary>
    /// 当前魔力
    /// </summary>
    public float Mana;
    float TimeCnt = 0;
    List<SpriteRenderer> sprlist = new List<SpriteRenderer>();


    private void Awake()
    {
        Move = gameObject.AddComponent<CharacterController2D>();
    }
    public override void Init()
    {
        SkillState = "noon";
        anim = GetComponent<Animator>();
        render = GetComponent<Renderer>();
        Properties = BaseProperties;
        Health = Properties.MaxHealth;
        Spirit = Properties.MaxSpirit;

        foreach(var a in GetComponentInChildren<Transform>())
            foreach (var b in ((Transform)a).GetComponentInChildren<Transform>())
                if (((Transform)b).TryGetComponent<SpriteRenderer>(out var k))
                sprlist.Add(k);
        GameManager.AllRoles.Add(this);
    }
    public virtual void Dead() { }
    public override void OnUpdate()
    {
        if (SkillState == SkillStiff && Move.CanMoveTime <= 0)
        {
            SkillState = "noon";
        }
        if (Health <= 0 && !die)
        {
            //Effect.Create(GameManager.Particle[1], gameObject, transform.position);
            Dead();
            die = true;
            Move.CanMove = false;
            SkillState = "Die";
            //k.transform.position = transform.position;
            //k.GetComponent<Effect>().Master = gameObject;
            //k.GetComponent<Effect>().SetFollow();
            Move.controller.velocity = Vector2.zero;
        }
        InvisibleTime -= Time.deltaTime;
        if (SkillQueue.Count > 0)
            if (SkillQueue.Peek().CanUse())
            {
                var skill = SkillQueue.Dequeue();
                skill.WantSkill();
            }
        if (SkillQueue.Count > 0)
            if (SkillQueue.Peek().InputTime < Time.time)
                SkillQueue.Dequeue();
    }
    private void FixedUpdate()
    {
        TimeCnt+= Time.fixedDeltaTime;
        if (TimeCnt >= 0.5f)
        {
            TimeCnt -= 0.5f;
            OnSustainedTrigger();
        }
        UnderAttackUpdate();
        DeleteQueue();
    }


    bool IsRed = false;
    public virtual void UnderAttackUpdate()
    {
        if (AttackedRed > 0 && !IsRed)
        {
            foreach(var arender in sprlist)
            {
                var col = arender.material.color;
                col.g = 0.5f;
                col.b = 0.5f;
                arender.material.color = col;
            }
            IsRed = true;
        }
        if (IsRed && AttackedRed < 0)
        {
            foreach (var arender in sprlist)
            {
                arender.material.color = Color.white;
            }
            IsRed = false;
        }
                
        AttackedRed -= Time.fixedDeltaTime;
    }

    public virtual void UnderAttack(Damage dam, Role from)
    {
        if (from)
            BeforeTakeDamage(dam,from);
        BeforeTakeDamage(dam);
        Health -= dam.FinalDamage;
        AttackedRed = 0.12f;
        var k = Lib.GetAngle(from.gameObject,gameObject);
        if (dam.damageEffect == DamageEffect.katana)
        {
            var eff = Effect.Create(GameManager.Particle[4],gameObject,transform.position);
            Lib.RotateX(eff.gameObject, k);
        }
        if (from)
            AfterTakeDamage(dam,from);
        AfterTakeDamage(dam);
        if (from)
            from.OnSucceedDamage(dam,this);
        Effect.CreateUnderAttackEffect(dam,gameObject);

    }
    /// <summary>
    /// 获取角色朝向的角度，左：180° 右：0°
    /// </summary>
    public float FaceAngle => (transform.localScale.x > 0) ? 0 : 180;
    /// <summary>
    /// 获取角色朝向，为左为-1 右为1
    /// </summary>
    /// <returns></returns>
    public int FaceTo => (transform.localScale.x > 0) ? 1 : -1;
    /// <summary>
    /// 使得角色朝向左边
    /// </summary>
    public void SetFaceToLeft()
    {
        if (transform.localScale.x > 0)
            Lib.SetFlipX(gameObject);
    }
    /// <summary>
    /// 使得角色朝向右边
    /// </summary>
    public void SetFaceToRight()
    {
        if (transform.localScale.x < 0)
            Lib.SetFlipX(gameObject);
    }
    /// <summary>
    /// 使该角色面向玩家，如果是玩家会默认朝向左。
    /// </summary>
    public void SetFaceToPlayer()
    {
        if (Lib.GetPosision(gameObject, PlayerGameObject).x > 0)
            SetFaceToRight();
        else
            SetFaceToLeft();
    }

    public void GetItem(int id)
    {
        GameManager.GetItem(id, this);
    }
    /// <summary>
    /// 恢复生命值
    /// </summary>
    /// <param name="Healing">恢复的量，如果为负数则为失去生命值</param>
    public void RecoverHealth(float Point)
    {
        float cnt = Point;
        foreach(var a in passives)
            Point += a.BeforeHealing(cnt);
        Health += Point;
        if (Health > Properties.MaxHealth)
        {
            Point = Properties.MaxHealth - Health;
            Health = Properties.MaxHealth;
        }
        UiManager.CreateNumShow(Mathf.Max(0,Point), transform.position, Color.green);
    }
    public void RecoverMana(float Point)
    {
        Mana += Point;
        if (Mana > Properties.MaxMana)
            Mana = Properties.MaxMana;
    }
    public void RecoverSpirit(float Point)
    {
        Spirit += Point;
        if (Spirit > Properties.MaxSpirit)
            Spirit = Properties.MaxSpirit;
    }
    public void HitBack(Vector2 v2)
    {
        if (!Move.controller)
            return;
        var v = Move.controller.velocity;
        v.x += v2.x;
        v.y += v2.y;
        Move.controller.velocity = v;
    }









    public virtual void BeforeTakeDamage(Damage damage, Role target) { foreach (var i in passives) i.BeforeTakeDamage(damage, target); }
    public virtual void BeforeTakeDamage(Damage damage) { foreach (var i in passives) i.BeforeTakeDamage(damage); }
    public virtual void AfterTakeDamage(Damage damage, Role target) { foreach (var i in passives) i.AfterTakeDamage(damage, target); }
    public virtual void AfterTakeDamage(Damage damage) { foreach (var i in passives) i.AfterTakeDamage(damage); }
    public virtual void OnSucceedDamage(Damage damage, Role target) { foreach (var i in passives) i.OnSucceedDamage(damage, target); }
    public virtual void BeforeDealDamage(Damage damage, Role target) { foreach (var i in passives) i.BeforeDealDamage(damage, target); }
    public virtual void OnSucceedAttack(Entity entity) { foreach (var i in passives) i.OnSucceedAttack(entity); }
    public virtual void BeforeFinalAttack(Damage damage, Role target) { foreach (var i in passives) i.BeforeFinalAttack(damage, target); }
    public virtual void OnGetItem(Item item) { foreach (var i in passives) i.OnGetItem(item); }
    public virtual void OnConflictItem(Item item) { foreach (var i in passives) i.OnConflictItem(item); }
    public virtual void OnConflictBuff(Buff buff) { foreach (var i in passives) i.OnConflictBuff(buff); }
    public virtual void BeforeGetBuff(Buff buff) { foreach (var i in passives) i.BeforeGetBuff(buff); }
    public virtual void AfterGetBuff(Buff buff) { foreach (var i in passives) i.AfterGetBuff(buff); }
    public virtual void OnGiveBuff(Buff buff) { foreach (var i in passives) i.OnGiveBuff(buff); }
    public virtual void BeforeUseSkill(Skill skill) { foreach (var i in passives) i.BeforeUseSkill(skill); }
    public virtual void AfterUseSkill(Skill skill) { foreach (var i in passives) i.AfterUseSkill(skill); }
    public virtual void OnSustainedTrigger() { foreach (var i in passives) i.OnSustainedTrigger(); }
    public virtual void AfterGetItem(Item item) { foreach (var i in passives) i.AfterGetItem(item); }
    public virtual void AfterFencing(Effect From,Effect To) { foreach (var i in passives) i.AfterFencing(From, To); }
    //public virtual float BeforeHealing(float ft)


    private void DeleteQueue()
    {
        while(DeletePossiveQueue.Count != 0)
        {
            foreach (var i in passives)
                if (i == DeletePossiveQueue.Peek())
                {
                    passives.Remove(i);
                    Destroy(i);
                    break;
                }
            DeletePossiveQueue.Dequeue();
        }
    }

    public Vector2 GetDistance()
    {
        return Lib.GetPosision(gameObject, PlayerGameObject);
    }

    public static string NormalState = "noon";
    public void MoveTo(Vector2 v2)
    {
        var tr = transform.position;
        tr.x = v2.x;
        tr.y = v2.y;
        transform.position = tr;
    }
    public void Stop()
    {
        var v = Move.controller.velocity;
        v.x = 0;
        v.y = 0;
        Move.controller.velocity = v;
    }
    public void SetInvisible(float time)
    {
        InvisibleTime = Mathf.Max(InvisibleTime, time);
    }
    public static string SkillStiff = "Stiff";
}
