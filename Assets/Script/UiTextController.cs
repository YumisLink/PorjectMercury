using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]
public class Dialogue
{
    public Sprite From;
    public string Name;
    public string Det;
}
public class UiTextController : MonoBehaviour
{
    private static Queue<Dialogue> Dialogues = new Queue<Dialogue>();
    Text Text;
    Image Portrait;
    private Dialogue NowExecute = null;
    bool tk = false;
    private void Start()
    {
        Text = UiManager.Manager.txt;
        Portrait = UiManager.Manager.hero;
        NowExecute = null;


        Add("单机左键继续");
        Add("您好！这里是字幕测试！^^^^^^^^^^^接下来我将会给你介绍操作方式！");
        Add("方向键左右键移动");
        Add("X攻击，C跳跃，Z冲刺，S是技能");
        Add("A可以换脸，换脸之后S技能和攻击冲刺将会被替换成对应的效果");
        Add("R重开 ESC结束游戏！");
        Add("向右走，可以遇到一个传送门，进去就完事了。");
    }
    private void Update()
    {
        if (Dialogues.Count > 0 && NowExecute == null)
            Execute();
        if (Dialogues.Count > 0 && !GameManager.IsStop)
        {
            GameManager.StopGame();
        }
        if (NowExecute == null)
            return;
        if (Input.GetMouseButtonDown(0))
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
    private void FixedUpdate()
    {
        if (NowExecute == null)
            return;
        if (tk)
        {
            tk = false;
            return;
        }
        if (nowText < NowExecute.Det.Length)
        {
            if (NowExecute.Det[nowText] != '^')
                Text.text += NowExecute.Det[nowText];
            nowText++;
        }
        tk = true;
    }
    void NextDia()
    {
        if (NowExecute == null)
            return;
        if (nowText < NowExecute.Det.Length)
        {
            Text.text = NowExecute.Det.Replace("^","");
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
        GameManager.ContinueGame();
        NowExecute = null;
    }
    public static void Add(Sprite sprite,string txt)
    {
        Dialogues.Enqueue(new Dialogue(){
            Det = txt,
            From = sprite,
            Name = "???"
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
