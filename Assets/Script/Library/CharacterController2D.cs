using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Role))]
public class CharacterController2D : MonoBehaviour
{
    public bool GravityEffect = true;
    public float CanMoveTime = 0;
    public Rigidbody2D controller;
    public float GravityMult = 1;
    public bool CanMove = true;
    public bool Paqiang = false;
    public float LimitSpeed => Mathf.Min(2f,role.Properties.MoveSpeed) * 8;
    public float HitBackDefense = 25;
    public float BaseMoveSpeed = 250;


    private float LimitX;
    private float LimitY;
    private Role role;
    public int MoveDirection = 0;
    private Collider2D _collider;
    private Vector2 Velocity;
    public Vector2 Debug;
    /// <summary>
    /// 上一帧有没有碰到墙，意思就是true的时候是刚刚离开墙
    /// </summary>
    private bool IsLastFrameTouchWall;
    private void Start()
    {
        //BaseInit();
        controller = GetComponent<Rigidbody2D>();
        role = GetComponent<Role>();
        _collider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        Debug = controller.velocity;
        if (GameManager.IsStop)
            return;
        CanMoveTime -= Time.deltaTime;
        if (role.SkillState != "noon" || CanMoveTime > 0)
            CanMove = false;
        else
            CanMove = true;
    }
    public Vector2 DeletaMove = new Vector2();
    public Vector2 NowMove = new Vector2();
    public void Move(float x, float y)
    {
        Velocity += new Vector2(x, y);
    }
    private float FallTime = 0;
    private float limit = 0;
    private void FixedUpdate()
    {
        if (GameManager.IsStop)
        {
            return;
        }
        Velocity = controller.velocity;
        MoveUpdate();
        if (GravityEffect && !IsGrounded && Paqiang)
            if (IsLastFrameTouchWall && IsLeftTouch == 0)
                Velocity.y = -4f;
        IsLastFrameTouchWall = (IsLeftTouch != 0);
        if (GravityEffect && !IsGrounded)
        {
            if (FallTime >= 1.75f)
                limit = 18 * GravityMult;
            else
                limit = 10 * GravityMult;
            if (Velocity.y >= -limit)
            {
                if (Velocity.y - 25 * Time.fixedDeltaTime < -limit * 2)
                {
                    Velocity.y = -limit * 2;
                    FallTime = 0;
                }
                else
                {
                    Velocity.y -= 40 * Time.fixedDeltaTime;
                    FallTime += Time.fixedDeltaTime;
                }
            }
        }
        if (IsLeftTouch != 0 && Paqiang)
            Velocity.y = Mathf.Max(-2.4f, Velocity.y);
        controller.velocity = Velocity;
        //NowMove.y = 0;
    }




    public bool IsGrounded
    {
        get
        {
            var bound = _collider.bounds;
            var min = bound.min;
            var max = bound.max;
            var center = new Vector2((max.x + min.x) / 2, min.y);
            var other = Physics2D.OverlapCircle(center, 0.01f, 1 << LayerMask.NameToLayer("Land"));
            return other;
        }
    }
    public int IsLeftTouch
    {
        get
        {
            var bound = _collider.bounds;
            var min = bound.min;
            var max = bound.max;
            var center = new Vector2(min.x, (min.y + max.y)/2);
            var other = Physics2D.OverlapCircle(center, 0.1f, 1 << LayerMask.NameToLayer("Land"));
            if (other) return -1;
            center = new Vector2(max.x, (min.y + max.y)/2);  
            other = Physics2D.OverlapCircle(center, 0.1f, 1 << LayerMask.NameToLayer("Land"));
            if (other) return 1;
            return 0;
        }
    }
    /// <summary>
    /// 是否在地上
    /// </summary>
    public bool IsGround
    {
        get
        {
            if (_collider)
                return _collider.IsTouchingLayers(1 << LayerMask.NameToLayer("Land"));
            else
                return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mut">输入参数</param>
    public void Go(int mut)
    {
        MoveDirection = mut;

        if (role.Move.IsGrounded && role.SkillState == "noon" && mut != 0)
            role.anim.Play("Move");
        if (role.Move.IsGrounded && role.SkillState == "noon" && mut == 0)
            role.anim.Play("Idle");
    }
    private void MoveUpdate()
    {
        if (!CanMove)
            return;
        //如果摸到墙，那么让他向上的速度降低到0
        if (IsLeftTouch != 0 && Paqiang)
        {
            if (Velocity.y > 2)
                Velocity.y = 2;
        }
        //超过最大速度的阈值，不会对你的操作进行反应
        if (Velocity.x > LimitSpeed * 2f)
        {
            Velocity.x -= HitBackDefense * Time.fixedDeltaTime;
            return;
        }
        if (Velocity.x < -LimitSpeed * 2f)
        {
            Velocity.x += HitBackDefense * Time.fixedDeltaTime;
            return;
        }
        //如果没有输入的话，会减速
        if (MoveDirection == 0)
        {
            if (Velocity.x != 0)
                Velocity.x -= Velocity.x * 15 * Time.fixedDeltaTime;
            return;
        }
        //右
        if (MoveDirection == 1)
        {
            if (Velocity.x < LimitSpeed)
            {
                if (Velocity.x + BaseMoveSpeed * Time.fixedDeltaTime > LimitSpeed)
                    Velocity.x = LimitSpeed;
                else
                    Velocity.x += BaseMoveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                Velocity.x -= BaseMoveSpeed * 0.1f * Time.deltaTime;
            }
        }
        //左
        if (MoveDirection == -1)
        {
            if (Velocity.x > -LimitSpeed)
            {
                if (Velocity.x - BaseMoveSpeed * Time.fixedDeltaTime < -LimitSpeed)
                    Velocity.x = -LimitSpeed;
                else
                    Velocity.x -= BaseMoveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                Velocity.x += BaseMoveSpeed * 0.1f * Time.deltaTime;
            }
        }
        MoveDirection = 0;
    }
}

