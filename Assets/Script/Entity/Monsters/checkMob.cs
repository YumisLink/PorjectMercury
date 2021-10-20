using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkMob : Role
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
        if (GameManager.IsStop)
            return;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
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
        if (dis.magnitude < 3.0f)
        {
            _easyAtk.WantSkill();
        }
        else
        {
            if (Move.IsGround)
            {
                Move.Go((int)Mathf.Sign(dis.x));
            }
        }
    }
    public override void UnderAttack(Damage dam, Role from)
    {
        Lib.SetMultScale(gameObject, 1.1f, 1.1f);
    }
}