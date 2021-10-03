using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoRotate : MonoBehaviour
{
    /// <summary>
    /// 旋转
    /// </summary>
    public float direction;
    public void Update()
    {
        Lib.Rotate(gameObject, direction * Time.deltaTime);    
    }
}
