using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSunFire : Effect
{
    private float TimeCount;
    private bool Trigger;
    private bool Trigger1;
    private bool Trigger2;
    private bool Trigger3;
    private bool Trigger4;
    public override void Init()
    {
        base.Init();
        var a = transform.position;
        a.y += 30;
        transform.position = a;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        TimeCount += Time.deltaTime;
        if (TimeCount > 3 && TimeCount < 4)
        {
            var a = transform.position;
            a.y -= 30 * Time.deltaTime;
            transform.position = a;
        }
        if (TimeCount >= 4 && !Trigger)
        {
            Trigger = true;
            var a = Effect.Create(GameManager.Effect[11],Master,transform.position);
            a.SetDamage(new Damage(100, DamageType.True));
            a.damage.fromSkill = "Sun";
        }
        if (TimeCount >= 6 && !Trigger1)
        {
            Trigger1 = true;
            var a = Effect.Create(GameManager.Effect[12], Master, transform.position);
            a.SetDamage(new Damage(100, DamageType.True));
            a.damage.fromSkill = "Sun";
        }
        if (TimeCount >= 8 && !Trigger2)
        {
            Trigger2 = true;
            var a = Effect.Create(GameManager.Effect[12], Master, transform.position);
            a.SetDamage(new Damage(100, DamageType.True));
            a.damage.fromSkill = "Sun";
        }
        if (TimeCount >= 10 && !Trigger3)
        {
            Trigger3 = true;
            var a = Effect.Create(GameManager.Effect[12], Master, transform.position);
            a.SetDamage(new Damage(100, DamageType.True));
            a.damage.fromSkill = "Sun";
        }
        if (TimeCount >= 12 && !Trigger4)
        {
            Trigger4 = true;
            var a = Effect.Create(GameManager.Effect[12], Master, transform.position);
            a.SetDamage(new Damage(100, DamageType.True));
            a.damage.fromSkill = "Sun";
            a = Effect.Create(GameManager.Effect[13], Master, transform.position);
        }
    }
}
