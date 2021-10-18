using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool
{
    /// <summary>
    /// 物品池的名字
    /// </summary>
    public string PoolName;
    public List<int> StartPool = new List<int>();
    private List<int> UsePool = new List<int>();
    public int GetItem()
    {
        int a = Random.Range(0, UsePool.Count);
        int ret = UsePool[a];
        UsePool.RemoveAt(a);
        return ret;
    }
    public void Init(List<int> ls)
    {
        StartPool.Clear();
        foreach(var a in ls)
            StartPool.Add(a);
        UsePool = StartPool;
        UsePool.RemoveAt(48);
        UsePool.RemoveAt(47);
        UsePool.RemoveAt(46);
        UsePool.RemoveAt(45);
        UsePool.RemoveAt(44);
        UsePool.RemoveAt(43);
        UsePool.RemoveAt(41);
        UsePool.RemoveAt(38);
        UsePool.RemoveAt(35);
        UsePool.RemoveAt(15);
        UsePool.RemoveAt(12);
        UsePool.RemoveAt(10);
        UsePool.RemoveAt(7);
        UsePool.RemoveAt(3);
    }
}
