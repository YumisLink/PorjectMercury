using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Role), typeof(CharacterController2D))]
public class PlayerInput : MonoBehaviour
{
    /// <summary>
    /// 控制器
    /// </summary>
    CharacterController2D controller;
    /// <summary>
    /// 角色
    /// </summary>
    Role role;
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        role = GetComponent<Role>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            role.SetFaceToLeft();
            controller.Go(-1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            role.SetFaceToRight();
            controller.Go(1);
        }else controller.Go(0);
    }
}
