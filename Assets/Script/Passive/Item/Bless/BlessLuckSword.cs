using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlessLuckSword : Item
{
    public override void GetItem()
    {
        if (TryGetComponent<Attack>(out var a))
            a.color = Colors.Luck;
    }
    public override void DiscardItem()
    {
        if (TryGetComponent<Attack>(out var a))
            a.color = Color.white;
    }
    public override void BeforeDealDamage(Damage damage, Role target)
    {
        if (damage.fromSkill == "Attack")
            if (Random.Range(0f,1f) <= 0.5f)
                damage.Critical = true;
    }
}
