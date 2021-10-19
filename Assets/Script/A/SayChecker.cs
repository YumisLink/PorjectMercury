using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SayChecker : Checker
{
    public List<string> str = new List<string>();
    public List<GameObject> summon = new List<GameObject>();
    protected override void Execute(Role k)
    {
        foreach (var a in str)
        {
            UiTextController.Add(a);
        }
        var par = gameObject.transform.parent.GetComponent<Room>();
        foreach(var a in summon)
        {
            var g = GameManager.CreateEntity(a);
            g.transform.position = transform.position;
            par.AllMonsters.Add(g);
        }
        if (summon.Count > 0)
            par.CloseTheGate();
    }
}
