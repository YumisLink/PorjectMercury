using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType
{
    Normal,True,Continue
}
public enum DamageSound
{
    def,katana
}
public class Damage
{
    public DamageType type;
    public DamageSound sound;
    public float FinalDamage;
    public float BaseDamage;
    public string fromSkill;
    public bool Critical = false;
    public bool Miss = false;
    //public string FromSkill { get => fromSkill; set => fromSkill = value; }
    public Damage(float damage)
    {
        BaseDamage = damage;
        type = DamageType.Normal;
        sound = DamageSound.def;
        fromSkill = "";
    }
    public Damage(float damage, DamageType type) : this(damage)
    {
        this.type = type;
    }
    public Damage(Damage dam)
    {
        type = dam.type;
        sound = dam.sound;
        BaseDamage = dam.BaseDamage;
        FinalDamage = dam.FinalDamage;
        fromSkill = dam.fromSkill;
    }
    public static void DealDamage(Damage damage,Role from,Role to) 
    {
        if (to.InvisibleTime > 0)
            return;
        if (damage.Miss)
            return;
        Damage To = new Damage(damage);
            To.FinalDamage = To.BaseDamage;
        if (from)
            from.BeforeDealDamage(To, to);
        
        To.FinalDamage = To.FinalDamage + Random.Range(-0.05f, 0.05f) * To.FinalDamage;

        if (from)
            from.BeforeFinalAttack(To, to);

        if (from)
            if (To.Critical) 
                To.FinalDamage *= from.Properties.CriticalRatio;
        to.UnderAttack(To, from);
        float bd;
        if (from != null)
            bd = from.Properties.Attack;
        else
            bd = 9999;
        if (To.FinalDamage > 0)
            UiManager.CreateDamageShow(To, to.transform.position,Mathf.Min(5,Mathf.Max(1, To.FinalDamage / bd / 6)));
    }

}