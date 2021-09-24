using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Properties
{
    /// <summary>
    /// 最大生命值
    /// </summary>
    public float MaxHealth;
    /// <summary>
    /// 最大精神值
    /// </summary>
    public float MaxSpirit;
    /// <summary>
    /// 最大魔法值
    /// </summary>
    public float MaxMana;
    /// <summary>
    /// 攻击力
    /// </summary>
    public float Attack;
    /// <summary>
    /// 击退抗性
    /// </summary>
    public float RepelResistance;
    /// <summary>
    /// 只允许修改 不允许调用
    /// </summary>
    public float Move;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed => Mathf.Max(0.25f,Move/100);

    /// <summary>
    /// 魔法恢复速度，单位为点每秒
    /// </summary>
    public float HealthRec;
    /// <summary>
    /// 生命恢复速度，单位为点每秒
    /// </summary>
    public float ManaRec;
    /// <summary>
    /// 精神恢复速度，单位为点每秒
    /// </summary>
    public float SpirtRec;
    /// <summary>
    /// 暴击率
    /// </summary>
    public float Critical;
    /// <summary>
    /// 暴击倍率
    /// </summary>
    public float CriticalRatio;
    /// <summary>
    /// 攻击距离
    /// </summary>
    public float RangeChange;
    /// <summary>
    /// 攻击距离
    /// </summary>
    public float Range => Mathf.Max(0.1f,Mathf.Min(5,RangeChange/100));
    /// <summary>
    /// 警告，不要直接调用该属性！！！！
    /// </summary>
    public float AttackSpeeds;
    /// <summary>
    /// 攻击速度
    /// </summary>
    public float AttackSpeed => Mathf.Max(0.1f, AttackSpeeds/100);


    public static Properties Create()
    {
        Properties ret;
        ret.MaxHealth = 100;
        ret.MaxMana = 0;
        ret.Attack = 0;
        ret.RepelResistance = 0;
        ret.MaxSpirit = 100;
        ret.HealthRec = 0;
        ret.ManaRec = 0;
        ret.SpirtRec = 0;
        ret.Critical = 0;
        ret.CriticalRatio = 1.5f;
        ret.RangeChange = 1;
        ret.Move = 1;
        ret.AttackSpeeds = 100;
        return ret;
    }
    public static Properties Zero()
    {

        Properties ret;
        ret.MaxHealth = 0;
        ret.MaxMana = 0;
        ret.Attack = 0;
        ret.RepelResistance = 0;
        ret.Move = 0;
        ret.MaxSpirit = 0;
        ret.HealthRec = 0;
        ret.ManaRec = 0;
        ret.SpirtRec = 0;
        ret.Critical = 0;
        ret.CriticalRatio = 0;
        ret.RangeChange = 0;
        ret.AttackSpeeds = 0;
        return ret;
    }
    public static Properties operator+(Properties A,Properties B)
    {
        A.MaxHealth += B.MaxHealth;
        A.MaxMana += B.MaxMana;
        A.Attack += B.Attack;
        A.RepelResistance += B.RepelResistance;
        A.Move += B.Move;
        A.MaxSpirit += B.MaxSpirit;
        A.HealthRec += B.HealthRec;
        A.ManaRec += B.ManaRec;
        A.SpirtRec += B.SpirtRec;
        A.Critical += B.Critical;
        A.CriticalRatio += B.CriticalRatio;
        A.RangeChange += B.RangeChange;
        A.AttackSpeeds += B.AttackSpeeds;
        return A;
    }
    public static Properties operator-(Properties A, Properties B)
    {
        A.MaxHealth -= B.MaxHealth;
        A.MaxMana -= B.MaxMana;
        A.Attack -= B.Attack;
        A.RepelResistance -= B.RepelResistance;
        A.Move -= B.Move;
        A.MaxSpirit -= B.MaxSpirit;
        A.HealthRec -= B.HealthRec;
        A.ManaRec -= B.ManaRec;
        A.SpirtRec -= B.SpirtRec;
        A.Critical -= B.Critical;
        A.CriticalRatio -= B.CriticalRatio;
        A.RangeChange -= B.RangeChange;
        A.AttackSpeeds -= B.AttackSpeeds;
        return A;
    }
}
