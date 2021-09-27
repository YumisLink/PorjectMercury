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
        state++;
        if (state == 4)
            state = 1;
        if (state == 1)
        {
            py.Attack = py.GetComponent<ActorSwordAttack>();
            SkillImage = img2;
        }
        if (state == 2)
        {
            py.Attack = py.GetComponent<ActorSpearAttack>();
            SkillImage = img3;
        }
        if (state == 3)
        {
            py.Attack = py.GetComponent<ActorRiderAttack>();
            SkillImage = img1;
        }
    }

}
