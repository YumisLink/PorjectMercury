using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Role
{
    public Jump jump;
    public Dash dash;
    public Attack attack;
    public override void Init()
    {
        base.Init();
        UiManager.player = this;
        PlayerGameObject = gameObject;
        jump = GetComponent<Jump>();
        dash = GetComponent<Dash>();
        attack = GetComponent<Attack>();
        for (var i = 7; i <= 16;i ++)
            Item.CreateItem(i, transform.position + new Vector3(5+i*1, 0,0));
    }
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        base.OnUpdate();
        if (Health <= 0)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z))
            dash.NextSkill();
        if (Input.GetKeyDown(KeyCode.C))
            jump.NextSkill();
        if (Input.GetKeyDown(KeyCode.X))
            attack.NextSkill();
    }
    public override void UnderAttack(Damage dam, Role from)
    {
        base.UnderAttack(dam, from);
        var k = Move.controller.velocity;
        var dis =Lib.GetPosision(from.gameObject, gameObject);
        dis.Normalize();
        k.x = dis.x * 10;
        k.y = dis.y * 10;
        k.y += 5;
        Move.controller.velocity = k;
        Move.CanMoveTime = 0.2f;
        GameManager.SpeedDownTime = 0.3f;
        UiManager.BlackTime = 0.2f;
        InvisibleTime = 0.85f;
    }
}
