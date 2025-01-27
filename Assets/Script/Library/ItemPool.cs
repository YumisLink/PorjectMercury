﻿using System.Collections;
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
        UsePool.RemoveAt(41);
        UsePool.RemoveAt(40);
        UsePool.RemoveAt(39);
        UsePool.RemoveAt(38);
        UsePool.RemoveAt(35);
        UsePool.RemoveAt(32);
        UsePool.RemoveAt(23);
        UsePool.RemoveAt(15);
        UsePool.RemoveAt(14);
        UsePool.RemoveAt(13);
        UsePool.RemoveAt(12);
        UsePool.RemoveAt(7);
        UsePool.RemoveAt(3);
    }
}
