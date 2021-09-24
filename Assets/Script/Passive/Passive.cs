using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Role))]
public class Passive : MonoBehaviour
{
    public Role role;
    private void Start()
    {
        role = GetComponent<Role>();
        OnStart();
        role.possives.Add(this);
    }
    /// <summary>
    /// 这只在Skill类以及Item类中引用！
    /// </summary>
    protected virtual void OnStart(){}
    /// <summary>
    /// 在受到有目标伤害之前调用一次。
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    public virtual void BeforeTakeDamage(Damage damage,Role target){ }
    /// <summary>
    /// 在受到伤害之前调用一次，无论有无目标。
    /// </summary>
    /// <param name="damage"></param>
    public virtual void BeforeTakeDamage(Damage damage) { }
    /// <summary>
    /// 在受到有目标的伤害之后调用一次。
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    public virtual void AfterTakeDamage(Damage damage, Role target) { }
    /// <summary>
    /// 在受到伤害之后调用一次，无论有无目标。
    /// </summary>
    /// <param name="damage"></param>
    public virtual void AfterTakeDamage(Damage damage) { }
    /// <summary>
    /// 在成功造成伤害之后调用一次
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    public virtual void OnSucceedDamage(Damage damage, Role target) { }
    /// <summary>
    /// 在准备造成伤害之前调用一次。
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    public virtual void BeforeDealDamage(Damage damage, Role target) { }
    /// <summary>
    /// 在成功击中目标之后调用，这里的目标包含敌人，墙体，尖刺。
    /// </summary>
    public virtual void OnSucceedAttack(Entity entity) { }
    /// <summary>
    /// 相应幸运天赋的 幸运一击。
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target"></param>
    public virtual void BeforeFinalAttack(Damage damage, Role target) { }
    /// <summary>
    /// 在获取Item的时候会调用一次
    /// </summary>
    /// <param name="item"></param>
    public virtual void OnGetItem(Item item) { }
    /// <summary>
    /// 在获取Item的时候会调用一次,用于判断物品是否发生冲突
    /// </summary>
    /// <param name="item"></param>
    public virtual bool OnConflictItem(Item item) { return false; }
    /// <summary>
    /// 在获取Buff的时候会调用一次,用于判断Buff是否发生冲突
    /// </summary>
    public virtual void OnConflictBuff(Buff buff) { }
    /// <summary>
    /// 在获得buff之前会调用一次
    /// </summary>
    /// <param name="buff"></param>
    public virtual void BeforeGetBuff(Buff buff) { }
    /// <summary>
    /// 在获得buff之后调用一次
    /// </summary>
    /// <param name="buff"></param>
    public virtual void AfterGetBuff(Buff buff) { }
    /// <summary>
    /// 在使得他人获得buff时调用一次
    /// </summary>
    /// <param name="buff"></param>
    public virtual void OnGiveBuff(Buff buff) { }
    /// <summary>
    /// 使用技能之前会调用一次
    /// </summary>
    /// <param name="skill"></param>
    public virtual void BeforeUseSkill(Skill skill) { }
    /// <summary>
    /// 使用技能之后会调用一次
    /// </summary>
    /// <param name="skill"></param>
    public virtual void AfterUseSkill(Skill skill) { }
    /// <summary>
    /// 每0.5秒触发一次，用于触发持续伤害
    /// </summary>
    public virtual void OnSustainedTrigger() { }
    /// <summary>
    /// 在获得物品之后会获得。
    /// </summary>
    public virtual void AfterGetItem(Item item) { }

}
