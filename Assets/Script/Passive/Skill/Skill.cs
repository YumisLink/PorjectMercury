using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect
{
    public float Time;
    public Action SkillAction;
    public bool Use = false;
    public SkillEffect(float time,Action action)
    {
        Time = time;
        SkillAction = action;
    }
}
public class Skill : Passive
{
    public float InputTime;
    public string SkillState;
    protected override void OnStart()
    {
        InputTime = Time.time;
        CoolTime = 0;
        Init();
    }
    /// <summary>
    /// 技能释放到的时间
    /// </summary>
    public float UsingTime;
    /// <summary>
    /// 技能释放的总时间
    /// </summary>
    public float ReleaseTime = 1;
    /// <summary>
    /// 技能冷却时间
    /// </summary>
    public float CoolDown = 1;
    /// <summary>
    /// 技能正在冷却时间
    /// </summary>
    public float CoolTime = 0;
    protected void EndSkill()
    {
        UsingTime = 0;
    }
    /// <summary>
    /// 技能的会执行的动作
    /// </summary>
    protected List<SkillEffect> list = new List<SkillEffect>();
    //public string skill {get{return }};
    /// <summary>
    /// 技能被加入的时候初始化
    /// </summary>
    public virtual void Init() { }
    /// <summary>
    /// 技能可以释放之后会调用一次
    /// </summary>
    public virtual void Before() { }
    /// <summary>
    /// 技能释放时候会持续调用
    /// </summary>
    public virtual void OnUsing() { }
    /// <summary>
    /// 技能释放之后会调用一次
    /// </summary>
    public virtual void After() { }
    /// <summary>
    /// 技能能否使用，return true则为能释放否则不能。
    /// </summary>
    /// <returns></returns>
    public virtual bool CanUse() 
    {
        return false;
    }
    /// <summary>
    /// 不论技能是否在使用，每一帧都会调用
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 通过这个可以让你在释放技能时候的第几秒使用什么技能。
    /// </summary>
    /// <param name="time"></param>
    /// <param name="action"></param>
    protected void AddAction(float time, Action action)
    {
        list.Add(new SkillEffect(time, action));
    }
    private void Release()
    {
        foreach (var a in list)
            if (a.Time <= UsingTime && !a.Use)
            {
                a.SkillAction();
                a.Use = true;
            }
    }
    private bool Request;
    void Update()
    {
        if (CoolTime >= 0)
            CoolTime -= Time.deltaTime;
        if (Request)
        {
            Request = false;
            if (CanUse())
            {
                foreach (var a in list)
                    a.Use = false;
                Before();
                role.BeforeUseSkill(this);
                CoolTime = CoolDown;
                role.SkillState = SkillState;
                UsingTime = 0;
            }
        }
        if (role.SkillState == SkillState)
        {
            if (UsingTime < ReleaseTime)
            {
                UsingTime += Time.deltaTime * role.Properties.AttackSpeed;
                OnUsing();
                Release();
            }
            else
            {
                role.SkillState = "noon";
                After();
                role.AfterUseSkill(this);
            }
        }
        OnUpdate();
    }
    protected virtual void OnFixedUpdate() { }
    protected virtual void OnFixedUsing() { }
    private void FixedUpdate()
    {
        OnFixedUpdate();
        if (role.SkillState == SkillState)
            OnFixedUsing();
    }
    /// <summary>
    /// 使得角色释放完这个技能之后立即释放下一个技能。
    /// </summary>
    public void NextSkill()
    {
        role.SkillQueue.Clear();
        this.InputTime = Time.time;
        InputTime += 0.15f;
        role.SkillQueue.Enqueue(this);
    }
    /// <summary>
    /// 加入技能释放队列
    /// </summary>
    public void AddSkillQueue()
    {
        this.InputTime = Time.time;
        InputTime += 100f;
        role.SkillQueue.Enqueue(this);
    }
    /// <summary>
    /// 立即释放这个技能，强烈不建议直接这样调用！！！！！！
    /// <para>因为可能因为不在状态中而丢失该技能输入</para>
    /// </summary>
    public void WantSkill()
    {
        Request = true;
    }

}
