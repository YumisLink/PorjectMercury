using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorController : Role
{
    public Skill Attack;
    public Skill Act;
    private float TimeDc = 0;
    public Vector2 v2dMove;
    public bool IsWeak = false;
    public override void Init()
    {
        base.Init();
        //UiManager.Bosses.Add(this);
        Attack = gameObject.AddComponent<DoctorAttack>();
        Act = gameObject.AddComponent<DoctorAct>();
    }
    public override void OnUpdate()
    {
        if (GetDistance().magnitude <= 20)
            IsWeak = true;
        if (!IsWeak)
            return;
        base.OnUpdate();
        if (Health <= 0)
        {
            return;
        }
        TimeDc += Time.deltaTime;
        var dis = GetDistance();
        if (Mathf.Abs(dis.x) >= 2 && SkillState == "noon")
        {
            if (dis.x > 0)
            {
                Move.Go(1);
                SetFaceToRight();
            }
            else
            {
                Move.Go(-1);
                SetFaceToLeft();
            }
        }
        else
        {
            Move.Go(0);
        }
        if (TimeDc >= 2.5f)
        {
            if (Mathf.Abs(dis.y) >= 2)
            {
                Act.NextSkill();
            }else if (Mathf.Abs(dis.x) >= 2 && Mathf.Abs(dis.x) <= 6)
            {
                if (Random.Range(0, 2) < 1)
                    Act.NextSkill();
                else 
                    Attack.NextSkill();
            }
            else Attack.NextSkill();
            TimeDc = 0;
            Move.controller.velocity = Vector2.zero;
        }



        
    }
    protected override void Dead()
    {
        base.Dead();
        var id = GameManager.ItemPool.GetItem();
        Item.CreateItem(id, transform.position);
        var r = GetComponent<SpriteRenderer>();
        var c = r.color;
        c.a = 0;
        r.color = c;
    }


}
