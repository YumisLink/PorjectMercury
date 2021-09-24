using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class JsonReaders
{
    public static List<ItemJsonClass> ReadFromFile()
    {
        //return new List<ItemJsonClass>();
        //Dictionary<string, string> ret = new Dictionary<string, string>();
        List<ItemJsonClass> ret = new List<ItemJsonClass>();
        string paths = Path.Combine(Application.streamingAssetsPath, "SkillData.json");
        StreamReader sr = new StreamReader(paths);
        string str = sr.ReadToEnd();


        NMBZenMeHaiYaoYiGeClass cl = JsonMapper.ToObject<NMBZenMeHaiYaoYiGeClass>(str);

        return cl.ItemJsonClass;
    }
}
public class ItemJsonClass
{
    public int id;
    public string ClassName;
    public string ItemName;
    public string ItemQuality;
    public string ItemDescribe;
    public string ItemType;
    public double[] Data;
    public override string ToString()
    {
        string str = ",";
        foreach (var a in Data)
            str += (a.ToString() + ",");
        return $"[id:{id},ClassName:{Type.GetType(ClassName).FullName},Quality:{Enum.Parse(typeof(ItemRare), ItemQuality)},ItemDesc:{ItemDescribe}]" + str;
    }
}
public class NMBZenMeHaiYaoYiGeClass
{
    public List<ItemJsonClass> ItemJsonClass;
}
