using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class JsonReaders
{
    public static List<ItemJsonClass> ReadFromFileItem()
    {
        string paths = Path.Combine(Application.streamingAssetsPath, "ItemData.json");
        StreamReader sr = new StreamReader(paths);
        string str = sr.ReadToEnd();
        NMBZenMeHaiYaoYiGeClass cl = JsonMapper.ToObject<NMBZenMeHaiYaoYiGeClass>(str);
        return cl.ItemJsonClass;
    }

    public static Dictionary<string,SkillMap> ReadFromFileSkill()
    {
        string paths = Path.Combine(Application.streamingAssetsPath, "SkillData.json");
        StreamReader sr = new StreamReader(paths);
        string str = sr.ReadToEnd();
        SkillJsondeClass cl = JsonMapper.ToObject<SkillJsondeClass>(str);
        Dictionary<string, SkillMap> map = new Dictionary<string, SkillMap>();
        foreach(var a in cl.SkillJsonClass)
        {
            SkillMap sm = new SkillMap
            {
                CoolDown = (float)a.CoolDown,
                ReleaseTime = (float)a.ReleaseTime,
                SkillState = a.SkillState,
                SkillDetail = a.SkillDetail,
                SkillName = a.SkillName,
                SkillType = a.SkillType
            };
            sm.Data = new float[a.Data.Length];
            for (var i = 0; i < sm.Data.Length; i++)
                sm.Data[i] = (float)a.Data[i];
            map.Add(a.ClassName, sm);
        }
        return map;
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
public class SkillJsonClass
{
    public string SkillName;
    public string ClassName;
    public string SkillDetail;
    public double ReleaseTime;
    public double CoolDown;
    public string SkillState;
    public string SkillType;
    public double[] Data;

}

public class SkillMap
{
    public string SkillName;
    public string SkillDetail;
    public float ReleaseTime;
    public float CoolDown;
    public string SkillType;
    public string SkillState;
    public float[] Data;
    public override string ToString()
    {
        return $"{SkillName},{ReleaseTime},{CoolDown}";
    }
}
public class NMBZenMeHaiYaoYiGeClass
{
    public List<ItemJsonClass> ItemJsonClass;
}

public class SkillJsondeClass
{
    public List<SkillJsonClass> SkillJsonClass;
}