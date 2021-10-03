using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 特效
    /// </summary>
    public static List<GameObject> Effect;


    /// <summary>
    /// UI
    /// </summary>
    public static List<GameObject> UI;


    /// <summary>
    /// 粒子效果
    /// </summary>
    public static List<GameObject> Particle;

    /// <summary>
    /// 物品的贴图
    /// </summary>
    public static List<Sprite> ItemImage;



    public static bool SuccessInit;
    public static List<Item> Item = new List<Item>();
    public static List<Role> AllRoles = new List<Role>();
    public static List<Effect> AllEffects = new List<Effect>();
    public static List<GameObject> AllFallingItem = new List<GameObject>();
    public static List<Sprite> SkillImage;
    public static List<int> NormalItemList = new List<int>();



    public static GameManager Manager;
    public static Camera Camera;
    public static Canvas Canvas;
    public static float SpeedDownTime;
    public static CinemachineConfiner VirtualCamera;
    public static CinemachineVirtualCamera cm;
    private static GameObject ItemCreater;


    //public static Dictionary<string, string> ItemData = new Dictionary<string, string>();
    //public static Dictionary<string, string> SkillData = new Dictionary<string, string>();
    public static List<ItemJsonClass> ItemData;
    public static Dictionary<string, SkillMap> SkillData;

    public static ItemPool ItemPool = new ItemPool();

    public static PolygonCollider2D Pc2d;


    void Awake()
    {
        ItemPool.StartPool.Clear();
        if (!SuccessInit)
        {
            LoadOnce();
        }
        //物品池
        NormalItemList.Clear();
        foreach (var a in ItemData)
            NormalItemList.Add(a.id);
        ItemPool.Init(NormalItemList);
        ItemPools = ItemPool.StartPool;

        SuccessInit = true;
        Manager = this;
        Debug.Log(Manager);
        Effect = Effects;
        UI = UIs;
        Particle = Particles;

        ItemCreater = itemCreater;
        Camera = privateCamera;
        Canvas = privateCanvas;
        ItemImage = Images;
        SkillImage = skillImg;
        Pc2d = pc2d;
        VirtualCamera = PrivateVirtualCamera;
        cm = VirtualCamera.GetComponent<CinemachineVirtualCamera>();
    }
    public void LoadOnce()
    {
        ItemData = JsonReaders.ReadFromFileItem();
        SkillData = JsonReaders.ReadFromFileSkill();

    }
    public float NowScreenBiger;
    void Update()
    {
        if (SpeedDownTime > 0)
        {
            Time.timeScale = 0.05f;
            SpeedDownTime -= 20 * Time.deltaTime;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void FixedUpdate()
    {
        var a = Input.GetAxis("Mouse ScrollWheel");
        if (a < -0.01f)
        {
            if (NowScreenBiger < 0)
                NowScreenBiger = 0;
            NowScreenBiger += 2f * Time.fixedDeltaTime;
            if (NowScreenBiger >= 0.5f)
                NowScreenBiger = 0.5f;
        }
        if (a > 0.01f)
        {
            if (NowScreenBiger > 0)
                NowScreenBiger = 0;
            NowScreenBiger -= 2f * Time.fixedDeltaTime;
            if (NowScreenBiger <= -0.5f)
                NowScreenBiger = -0.5f;
        }
        cm.m_Lens.OrthographicSize += NowScreenBiger;
        if (a > -0.1f && a < 0.1f)
        {
            NowScreenBiger -= NowScreenBiger * 2f * Time.fixedDeltaTime;
        }
    }






    public  static Color FindColor(ItemRare IR)
    {
        if (IR == ItemRare.Epic)
            return Colors.Purple;
        if (IR == ItemRare.Legend)
            return Colors.Golden;
        if (IR == ItemRare.Rare)
            return Colors.Bule;
        return Color.white;
    }
    public static Item GetItem(int id,Role Creater)
    {
        Item item;
        Type tp;
        tp = Type.GetType(ItemData[id].ClassName);
        item = (Item)Creater.gameObject.AddComponent(tp);
        item.Id = id;
        item.Name = ItemData[id].ItemName;
        item.Rare = (ItemRare)Enum.Parse(typeof(ItemRare), ItemData[id].ItemQuality);
        item.Type = (ItemType)Enum.Parse(typeof(ItemType), ItemData[id].ItemType);
        item.Data = new float[ItemData[id].Data.Length];
        for (int i = 0; i < item.Data.Length; i++)
            item.Data[i] = (float)ItemData[id].Data[i];
        UiManager.ShowItemDetail(5,item.Message);
        return item;
    }
    /// <summary>
    /// 创建角色都用这个创建
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Role CreateEntity(GameObject gameObject)
    {
        var go = Instantiate(gameObject);
        if (go.TryGetComponent<Role>(out var role))
        {
            AllRoles.Add(role);
            return role;
        }
        else;
        {
            Debug.LogError("创建单位不包含Role");
            return null;
        }
    }
    /// <summary>
    /// 创建特效都用这个创建
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Effect CreateEffects(GameObject gameObject)
    {
        var go = Instantiate(gameObject);
        if (go.TryGetComponent<Effect>(out var Effects))
        {
            AllEffects.Add(Effects);
            return Effects;
        }
        else;
        {
            Debug.LogError("创建单位不包含Effect");
            return null;
        }
    }
    /// <summary>
    /// 创建一个掉落的物品模板
    /// </summary>
    /// <returns></returns>
    public static GameObject CreateItem()
    {
        var go = Instantiate(ItemCreater);
        AllFallingItem.Add(go);
        return go;
    }
    public static void DestoryItem(GameObject go)
    {
        AllFallingItem.Remove(go);
        Destroy(go);
    }
    /// <summary>
    /// 删除特效
    /// </summary>
    /// <param name="eff"></param>
    public static void DestoryEffect(Effect eff)
    {
        AllEffects.Remove(eff);
        Destroy(eff.gameObject);
    }
    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="role"></param>
    public static void DestoryRole(Role role)
    {
        AllRoles.Remove(role);
        Destroy(role.gameObject);
    }
    public PolygonCollider2D pc2d;
    public Camera privateCamera;
    public Canvas privateCanvas;
    public GameObject itemCreater;
    public CinemachineConfiner PrivateVirtualCamera;
    public List<GameObject> Effects = new List<GameObject>();
    public List<GameObject> UIs = new List<GameObject>();
    public List<GameObject> Particles = new List<GameObject>();
    public List<Sprite> Images = new List<Sprite>();
    public List<Sprite> skillImg = new List<Sprite>();
    public List<int> ItemPools;
}