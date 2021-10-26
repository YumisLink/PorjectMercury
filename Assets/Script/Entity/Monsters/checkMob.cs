using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkMob : Role
{
    private EasyAttack _easyAtk;
    private float _jumpInterval = 2.0f;
    public Image ig;
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
    public int cnt = 0;
    public override void UnderAttack(Damage dam, Role from)
    {
        cnt++;
        if (cnt >= 15)
        {
            UiTextController.end = true;
            UiTextController.Add("心中亦有戏,"); 
            UiTextController.Add("目中皆无人。");
        }
        Lib.SetMultScale(gameObject, 1.4f, 1.4f);
    }
}