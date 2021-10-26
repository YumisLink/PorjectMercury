using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class NormalText : MonoBehaviour
{
    public float TimeCount = 0;
    public static bool Dead = false;
    private static Queue<Dialogue> Dialogues = new Queue<Dialogue>();
    public Text Text;
    private Dialogue NowExecute = null;
    public Image sum;
    int tk = 0;
    private void Start()
    {
        NowExecute = null;
    }
    public static void Init()
    {
        Add("爷爷是一名变脸艺术家。");
        Add("他的一生都奉献给了川剧变脸艺术。");
        Add("爷爷临终前喊我到床边，他喊着我的小名对我说，他刚刚做了一个很长很长的冒险的梦，在梦里……");
    }
    private void Update()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount > 5)
        {
            Init();
            TimeCount = -999999;
        }
        if (Dialogues.Count > 0 && !GameManager.IsStop)
        {
            GameManager.StopGame();
        }
        if (Dialogues.Count > 0 && NowExecute == null)
            Execute();
        if (NowExecute == null)
            return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            NextDia();
        }

    }

    void Execute()
    {
        NowExecute = Dialogues.Peek();
        Dialogues.Dequeue();
    }
    int nowText = 0;
    float ct2 = 0;
    private void FixedUpdate()
    {
        if (NowExecute == null)
            return;
        if (tk < 5)
        {
            tk ++;
            return;
        }
        if (nowText < NowExecute.Det.Length)
        {
            if (NowExecute.Det[nowText] != '^')
                Text.text += NowExecute.Det[nowText];
            nowText++;
            if (ct2 >= 1 && NowExecute.Det.Length - nowText > 3)
            {
                Sound.Play(GameManager.Audio[4]);
                ct2 -= 1f;
            }
            ct2+=1;
        }
        tk =0;
    }
    void NextDia()
    {
        if (NowExecute == null)
            return;
        if (nowText < NowExecute.Det.Length)
        {
            Text.text = NowExecute.Det.Replace("^", "");
            nowText = NowExecute.Det.Length;
        }
        else
        {
            Text.text = "";
            NowExecute = null;
            nowText = 0;
            if (Dialogues.Count == 0)
                EndDialogues();
        }
    }
    void EndDialogues()
    {
        gameObject.AddComponent<UiEffect_001>();
        sum.gameObject.AddComponent<UiEffect_001>();
        UiTextController.Add("豆子村？我回到了……豆子村？(按下方向键左右键移动");
        GameManager.ContinueGame();
        NowExecute = null;
    }
    public static void Add(string txt, string name)
    {
        Dialogues.Enqueue(new Dialogue()
        {
            Det = txt,
            From = null,
            Name = name
        });
    }
    public static void Add(string txt)
    {
        Dialogues.Enqueue(new Dialogue()
        {
            Det = txt,
            From = null,
            Name = "???"
        });
    }
}
