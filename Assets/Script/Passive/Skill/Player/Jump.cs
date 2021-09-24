using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Skill
{
    public int JumpCount = 2;
    public float Jumping = 0;
    private int TouchWall;
    public bool EnterTheSky = false;
    public int LastTouch = 0;
    public Effect particle;
    public Effect go;
    public Color color = Color.white;
    public override void Init()
    {
        ReleaseTime = 0.0f;
        CoolDown = 0.2f;
        SkillState = "Jump";
    }
    public override void Before()
    {
        role.Move.GravityEffect = false;
        if (JumpCount == 1)
        {
            go = Effect.Create(GameManager.Effect[0], gameObject, transform.position);
            particle = Effect.Create(GameManager.Particle[0], gameObject, transform.position);
            go.GetComponent<SpriteRenderer>().color = color;
            particle.GetComponent<ParticleSystem>().startColor = color;
            go.SetFollow();
            particle.SetFollow();
            if (role.FaceTo == 1)
                Lib.SetFlipX(go.gameObject);
        }
        JumpCount--;
        Jumping = 0.45f;
        TouchWall = role.Move.IsLeftTouch;
    }
    public override bool CanUse()
    {
        if (Jumping > 0)
            return false;
        if (!Input.GetKey(KeyCode.C))
            return false;
        if (role.SkillState != "noon")
            return false;
        if (JumpCount <= 0)
            return false;
        if (CoolTime > 0)
            return false;
        return true;
    }
    public override void OnUpdate()
    {
        UsingTime += (1 - role.Properties.AttackSpeed) * Time.deltaTime;
        if (Jumping <= 0 && go)
        {
            go.Death();
            particle.GetComponent<ParticleSystem>().Stop();
        }
        if (role.Move.IsGrounded && CoolTime <= 0)
            JumpCount = 2;
        if (LastTouch == 1 && role.Move.IsLeftTouch == 0)
        {
            JumpCount = 1;
        }
        if (role.Move.IsLeftTouch != 0 && CoolTime <= 0)
        {
            JumpCount = 2;
            LastTouch = 1;
        }
        else
        {
            LastTouch = 0;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextSkill();
        }
    }
    protected override void OnFixedUpdate()
    {
        if (Jumping <= 0)
        {
            if (role.Move.CanMove && role.SkillState == "noon" && !role.Move.IsGround && role.Move.IsLeftTouch == 0)
                role.anim.Play("Fall");
        }
        if (Jumping > 0)
        {
            if (role.SkillState == "noon")
                role.anim.Play("Jump");
            Jumping -= Time.fixedDeltaTime;
            //如果是你主动松开 则会让速度立即下降到0
            if ((!Input.GetKey(KeyCode.C) && Jumping <= 0.35f && Jumping >= 0.1f) || role.SkillState != "noon")
            {
                var Velocity2 = role.Move.controller.velocity;
                if (Velocity2.y >= 8)
                    Velocity2.y -= 8;
                else
                    Velocity2.y = 0;
                role.Move.controller.velocity = Velocity2;
                Jumping = 0;
            }
            //这里是因为弹墙出来，修改的使得这个时候可以开始移动
            if (Jumping <= 0.35f)
                role.Move.CanMove = true;
            //最后的0.05秒不允许操作，并且会开始缓慢下落
            if (Jumping <= 0.05f)
            {
                var Velocity2 = role.Move.controller.velocity;
                Velocity2.y -= 25 * Time.deltaTime;
                role.Move.controller.velocity = Velocity2;
                role.Move.GravityEffect = true;
            }
            else
            {
                //跳跃过程
                var Velocity = role.Move.controller.velocity;
                if (Velocity.y > 8)
                    Velocity.y -= 9.8f * Time.fixedDeltaTime;
                if (Velocity.y < 8)
                {
                    Velocity.y += 100 * Time.fixedDeltaTime;
                    if (Velocity.y > 8)
                        Velocity.y = 8;
                }
                //下面两句话是离开墙的时候

                if (TouchWall != 0 && Jumping > 0.35f)
                {
                    Velocity.x = TouchWall * -15;
                    if (TouchWall == -1)
                        role.SetFaceToLeft();
                    else
                        role.SetFaceToRight();
                }
                if (TouchWall != 0 && Jumping > 0.3f)
                {
                    if (Jumping <= 0.35f)
                        if (role.Move.MoveDirection == TouchWall)
                            Velocity.x = TouchWall * 8;

                    Velocity.y = 8;
                }

                role.Move.controller.velocity = Velocity;
            }
        }
    }
}
