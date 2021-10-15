using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EasyMobController : Role
{
    private EasyAttack _easyAtk;
    private float _jumpInterval = 2.0f;

    public override void Init()
    {
        base.Init();
        _easyAtk = gameObject.AddComponent<EasyAttack>();
    }

    void FixedUpdate()
    {
        _jumpInterval -= Time.fixedDeltaTime;
        var dis = GetDistance();
        if (SkillState == NormalState)
        {
            SetFaceToPlayer();
            if (Mathf.Abs(dis.y) > 2.0f)
            {
                if (_jumpInterval < 0.0f)
                {
                    HitBack(new Vector2(0, 20));
                    _jumpInterval = 2.0f;
                }
            }
        }
        if (dis.magnitude < 1.0f)
        {
            _easyAtk.WantSkill();
            Move.Go(0);
        }
        else
        {
            if (Move.IsGround)
            {
                Move.Go((int)Mathf.Sign(dis.x));
            }
        }
    }
}