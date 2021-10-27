using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextType
{
    Damage,name
}

public class Font : MonoBehaviour
{
    /// <summary>
    /// Canvas所在的Camera
    /// </summary>
    private Camera Camera;

    /// <summary>
    /// 跟随的目标
    /// </summary>
    public Transform Target;

    /// <summary>
    /// 世界空间中，UI位置的偏移量
    /// </summary>
    public Vector2 Offset;

    /// <summary>
    /// Canvas的Render Mode是<see cref="RenderMode.WorldSpace"/>该值可以为空
    /// </summary>
    private RectTransform Root;

    /// <summary>
    /// 字体的类型
    /// </summary>
    public TextType Type;

    public Vector2 v2d;

    private Text text;

    private float TimeCount;

    private void Start()
    {
        Camera = GameManager.Camera;
        Root = GameManager.Canvas.transform as RectTransform;
        text = GetComponent<Text>();
        TimeCount = 0;
    }
    private Vector2 Salve(Vector2 v2d)
    {
        Vector2 ret;
        var sp = RectTransformUtility.WorldToScreenPoint(Camera, v2d);
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(Root, sp, null, out ret))
            ret = new Vector2(-10000, -10000);
        return ret;
    }
    private void Update()
    {
        TimeCount += Time.deltaTime;
        if (Type == TextType.Damage)
        {
            if (TimeCount <= 0.5f)
            {
                var scale = transform.localScale;
                scale.y -= 1 * Time.deltaTime;
                scale.x -= 1* Time.deltaTime;
                transform.localScale = scale;
            }
            else
            {
                Offset.y += 5 * Time.deltaTime;
            }
            if (TimeCount >= 1)
            {
                var color = text.color;
                color.a -= Time.deltaTime * 3;
                text.color = color;
            }
            if (TimeCount >= 1.3f)
            {
                Destroy(gameObject);
                Destroy(this);
            }
        }
    }
    private void LateUpdate()
    {
        RectTransform t = transform as RectTransform;
        if (Target)
            v2d = Target.position;
        t.anchoredPosition = Salve(v2d + Offset);
    }
}
