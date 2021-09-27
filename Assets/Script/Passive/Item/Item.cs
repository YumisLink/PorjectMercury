using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Passive, Active, BlessAttack,BlessRushAttack,BlessRush,BlessJump
}
public enum ItemRare 
{ 
    Normal,Rare,Epic,Legend
}

public class Item : Passive
{
    /// <summary>
    /// 物品id
    /// </summary>
    public int Id;
    /// <summary>
    /// 物品名称
    /// </summary>
    public string Name;
    /// <summary>
    /// 物品详情
    /// </summary>
    public string Message => GameManager.ItemData[Id].ItemDescribe;
    /// <summary>
    /// 物品展示样子
    /// </summary>
    public Sprite Image;
    /// <summary>
    /// 道具类型 分为 主动 被动 祝福
    /// </summary>
    public ItemType Type;
    /// <summary>
    /// 物品稀有度
    /// </summary>
    public ItemRare Rare;
    /// <summary>
    /// 物品的数据
    /// </summary>
    public float[] Data;
    /// <summary>
    /// 物品冷却时间
    /// </summary>
    public float ItemCoolDown;
    /// <summary>
    /// 获得道具的时候会调用一次
    /// </summary>
    public virtual void GetItem() { }
    /// <summary>
    /// 道具刚被创建，赋值完角色之后调用一次
    /// </summary>
    public virtual void Init() { }
    /// <summary>
    /// 丢弃道具的时候会调用一次
    /// </summary>
    public virtual void DiscardItem() { }
    /// <summary>
    /// 点下使用物品的按钮之后会调用一次
    /// </summary>
    public virtual void UseItem() { }
    protected override void OnStart()
    {
        role.OnConflictItem(this);
        role.OnGetItem(this);
        GetItem();
        Init();
        role.AfterGetItem(this);
    }
    public override bool OnConflictItem(Item item)
    {
        if (item.Type == ItemType.Passive)
            return false;
        if (item.Type == Type)
            DestroyItem();
        return true;
    }
    public void DestroyItem()
    {
        DiscardItem();
        role.DeletePossiveQueue.Enqueue(this);
    }
    public static ItemFloat CreateItem(int id,Vector2 position)
    {
        var ret = GameManager.CreateItem();
        var Ic = ret.GetComponent<ItemCollider>();
        var IF = Ic.child.GetComponent<ItemFloat>();
        IF.ItemId = id;
        ret.transform.position = position;
        return IF;
    }
}
