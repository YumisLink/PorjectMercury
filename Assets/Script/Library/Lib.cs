using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lib 
{
    public static void SetFlipX(GameObject go)
    {
        var scale = go.transform.localScale;
        scale.x *= -1;
        go.transform.localScale = scale;
    }
    public static void Bigger(GameObject go,float x,float y)
    {
        var scale = go.transform.localScale;
        scale.x *= x;
        scale.x *= y;
        go.transform.localScale = scale;
    }
    public static void SetFlipY(GameObject go)
    {
        var scale = go.transform.localScale;
        scale.y *= -1;
        go.transform.localScale = scale;
    }
    public static float LimitAddDeleta(float Limit,float Start,float Deleta)
    {
        if (Start + Deleta >= Limit)
            return Limit - Start;
        else
            return Deleta;
    }
    /// <summary>
    /// 旋转一定角度，为+=
    /// </summary>
    /// <param name="go"></param>
    /// <param name="direction"></param>
    public static void Rotate(GameObject go,float direction)
    {
        var angle = go.transform.localEulerAngles;
        angle.z += direction;
        go.transform.localEulerAngles = angle;
    }
    public static void RotateX(GameObject go, float direction)
    {
        var angle = go.transform.localEulerAngles;
        angle.x += direction;
        go.transform.localEulerAngles = angle;
    }
    public static void SetRotate(GameObject go, float direction)
    {
        var angle = go.transform.localEulerAngles;
        angle.z = direction;
        go.transform.localEulerAngles = angle;
    }
    public static void SetMultScale(GameObject go, float x,float y)
    {
        var scale = go.transform.localScale;
        scale.x *= x;
        scale.y *= y;
        go.transform.localScale = scale;
    }
    public static float GetAngle(float x, float y)
    {
        float xy = Mathf.Sqrt(x * x + y * y);
        float ret = Mathf.Acos(x / xy) * 180 / Mathf.PI;
        if (y < 0)
            return -ret;
        return ret;
    }
    public static float GetAngle(GameObject A, GameObject B)
    {
        var a = A.transform.position;
        var b = B.transform.position;
        return GetAngle(b.x - a.x, b.y - a.y);
    }
    public static Vector2 GetPosision(GameObject A,GameObject B)
    {
        Vector2 ret = new Vector2();
        var a = A.transform.position;
        var b = B.transform.position;
        ret.x = b.x - a.x;
        ret.y = b.y - a.y;
        return ret;
    }
    /// <summary>
    /// 返回从左到右处于input的值的大小，如果输入的范围在low和high之间的话范围为0~1
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static float Fitting(float low,float high,float input)
    {
        high -= low;
        input -= low;
        return (input / high);
    }
    public static void MoveTo(GameObject go,Vector2 v2)
    {
        var t = go.transform.position;
        t.x = v2.x;
        t.y = v2.y;
        go.transform.position = t;
    }
}
