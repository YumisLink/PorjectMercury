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
    public List<Passive> possives = new List<Passive>();
    public Queue<Passive> DeletePossiveQueue = new Queue<Passive>();
    public Skill nowSkill;


    public bool die;
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



    public override void Init()
    {
        SkillState = "noon";
        Move = gameObject.AddComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        render = GetComponent<Renderer>();
        Properties = BaseProperties;
        Health = Properties.MaxHealth;
        Spirit = Properties.MaxSpirit;

    }
    public override void OnUpdate()
    {
        if (SkillState == SkillStiff && Move.CanMoveTime <= 0)
        {
            SkillState = "noon";
        }
        if (Health <= 0 && !die)
        {
            Effect.Create(GameManager.Particle[1], gameObject, transform.position);
            //var k = Instantiate(GameManager.Particle[1]);
            die = true;
            return;
            //Move.CanMove = false;
            //SkillState = "???";
            //k.transform.position = transform.position;
            //k.GetComponent<Effect>().Master = gameObject;
            //k.GetComponent<Effect>().SetFollow();
            //Move.controller.velocity = Vector2.zero;
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


    public virtual void UnderAttackUpdate()
    {
        if (render!=null)
            if (AttackedRed >= 0)
            {
                var col = render.material.color;
                col.g = 0.5f;
                col.b = 0.5f;
                render.material.color = col;
                //Debug.Log(render.gameObject.name);
            }
            else

                render.material.color = Color.white;
        AttackedRed -= Time.fixedDeltaTime;
    }

    public virtual void UnderAttack(Damage dam, Role from)
    {
        if (from)
            BeforeTakeDamage(dam,from);
        BeforeTakeDamage(dam);
        Health -= dam.FinalDamage;
        AttackedRed = 0.12f;
        if (from)
            AfterTakeDamage(dam,from);
        AfterTakeDamage(dam);
        if (from)
            from.OnSucceedDamage(dam,this);

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
        var v = Move.controller.velocity;
        v.x += v2.x;
        v.y += v2.y;
        Move.controller.velocity = v;
    }









    public virtual void BeforeTakeDamage(Damage damage, Role target) { foreach (var i in possives) i.BeforeTakeDamage(damage, target); }
    public virtual void BeforeTakeDamage(Damage damage) { foreach (var i in possives) i.BeforeTakeDamage(damage); }
    public virtual void AfterTakeDamage(Damage damage, Role target) { foreach (var i in possives) i.AfterTakeDamage(damage, target); }
    public virtual void AfterTakeDamage(Damage damage) { foreach (var i in possives) i.AfterTakeDamage(damage); }
    public virtual void OnSucceedDamage(Damage damage, Role target) { foreach (var i in possives) i.OnSucceedDamage(damage, target); }
    public virtual void BeforeDealDamage(Damage damage, Role target) { foreach (var i in possives) i.BeforeDealDamage(damage, target); }
    public virtual void OnSucceedAttack(Entity entity) { foreach (var i in possives) i.OnSucceedAttack(entity); }
    public virtual void BeforeFinalAttack(Damage damage, Role target) { foreach (var i in possives) i.BeforeFinalAttack(damage, target); }
    public virtual void OnGetItem(Item item) { foreach (var i in possives) i.OnGetItem(item); }
    public virtual void OnConflictItem(Item item) { foreach (var i in possives) i.OnConflictItem(item); }
    public virtual void OnConflictBuff(Buff buff) { foreach (var i in possives) i.OnConflictBuff(buff); }
    public virtual void BeforeGetBuff(Buff buff) { foreach (var i in possives) i.BeforeGetBuff(buff); }
    public virtual void AfterGetBuff(Buff buff) { foreach (var i in possives) i.AfterGetBuff(buff); }
    public virtual void OnGiveBuff(Buff buff) { foreach (var i in possives) i.OnGiveBuff(buff); }
    public virtual void BeforeUseSkill(Skill skill) { foreach (var i in possives) i.BeforeUseSkill(skill); }
    public virtual void AfterUseSkill(Skill skill) { foreach (var i in possives) i.AfterUseSkill(skill); }
    public virtual void OnSustainedTrigger() { foreach (var i in possives) i.OnSustainedTrigger(); }
    public virtual void AfterGetItem(Item item) { foreach (var i in possives) i.AfterGetItem(item); }







    private void DeleteQueue()
    {
        while(DeletePossiveQueue.Count != 0)
        {
            foreach (var i in possives)
                if (i == DeletePossiveQueue.Peek())
                {
                    possives.Remove(i);
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
    public static string SkillStiff = "Stiff";
}
