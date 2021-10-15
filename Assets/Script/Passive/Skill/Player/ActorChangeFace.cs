using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorChangeFace : Skill
{
    public Sprite img1;
    public Sprite img2;
    public Sprite img3;
    public int state;
    public Player py;
    public override void Init()
    {
        py = GetComponent<Player>();
        img1 = GameManager.SkillImage[0];
        img2 = GameManager.SkillImage[1];
        img3 = GameManager.SkillImage[2];
        state = 1;
        SkillImage = img1;
    }
    public override bool CanUse()
    {
        if (CoolTime > 0)
            return false;
        if (SkillState == "Vertigo")
            return false;
        return true;
    }
    public override void Before()
    {
        Sound.Play(GameManager.Audio[2]);
        Effect.Create(GameManager.Effect[5],role.gameObject,transform.position);
        var go = Effect.Create(GameManager.Effect[5], role.gameObject, transform.position);
        Lib.Rotate(go.gameObject, 45);
        state++;
        if (state == 3)
            state = 1;
        if (state == 1)
        {
            py.Attack = py.GetComponent<ActorSwordAttack>();
            py.Skill2 = py.GetComponent<ActorAliveOrDeath>();
            SkillImage = img1;
        }
        if (state == 2)
        {
            py.Attack = py.GetComponent<ActorSpearAttack>();
            py.Skill2 = py.GetComponent<ActorLimitSpike>();
            SkillImage = img2;
        }
        if (state == 3)
        {
            py.Attack = py.GetComponent<ActorRiderAttack>();
            py.Skill2 = py.GetComponent<ActorJusticeAdjudication>();
            SkillImage = img3;
        }
    }

}
