using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public Vector2 StartTransform;
    private Transform cameras;
    private Vector2 StartCam = new Vector2();
    /// <summary>
    /// 是否锁定Y轴
    /// </summary>
    public bool LockY = false;
    public float MoveSpeed = 1;
    private float cnt = 0;
    private bool Trigger = true;
    private void Reinit()
    {
        StartCam.x = cameras.position.x;
        StartCam.y = cameras.position.y;
        transform.position = StartTransform;
    }
    private void Start()
    {
        StartTransform = new Vector2(transform.position.x,transform.position.y);
        cameras = GameManager.Camera.transform;
        StartCam.x = cameras.position.x;
        StartCam.y = cameras.position.y;
    }
    public void ReInit()
    {
        cnt = 0.05f;
        Trigger = true;
    }
    private void FixedUpdate()
    {
        if (cnt < 0 && Trigger)
        {
            Trigger = false;
            Reinit();
        }
        cnt -= Time.deltaTime;
        //Vector2 moving = new Vector2(, 0);
        transform.position = (StartTransform + new Vector2((cameras.position.x - StartCam.x) * MoveSpeed, 0));
    }
}
