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
    public static List<GameObject> Roles;
    public static List<GameObject> Rooms;



    public static GameManager Manager;
    public static Camera Camera;
    public static Canvas Canvas;
    public static float SpeedDownTime;
    public static CinemachineConfiner VirtualCamera;
    public static CinemachineVirtualCamera cm;
    private static GameObject ItemCreater;
    public static List<Room> AllRooms = new List<Room>();


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
        init();

        LoadRoom();
        LoadPlayer();
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



    void init()
    {
        Effect = Effects;
        UI = UIs;
        Particle = Particles;

        ItemCreater = itemCreater;
        Camera = privateCamera;
        Canvas = privateCanvas;
        ItemImage = Images;
        SkillImage = skillImg;
        VirtualCamera = PrivateVirtualCamera;
        cm = VirtualCamera.GetComponent<CinemachineVirtualCamera>();
        Roles = Role;
        Rooms = Room;
    }
    public void LoadOnce()
    {
        ItemData = JsonReaders.ReadFromFileItem();
        SkillData = JsonReaders.ReadFromFileSkill();
    }
    void LoadPlayer()
    {
        var player = GameObject.Instantiate(GameManager.Roles[4]);
        player.transform.position = Vector3.zero;
        cm.Follow = player.transform;
    }
    void LoadRoom()
    {
        var rm1 = GameManager.CreateRoom(GameManager.Rooms[0]);
        rm1.transform.position = Vector3.zero;

        var rm2 = GameManager.CreateRoom(GameManager.Rooms[2]);
        rm2.transform.position = Vector3.zero + new Vector3(50,0,0);
        rm1.RightGate.LinkTo(rm2.LeftGate);


        var rm3 = GameManager.CreateRoom(GameManager.Rooms[2]);
        rm3.transform.position = Vector3.zero + new Vector3(100, 0, 0);
        rm2.RightGate.LinkTo(rm3.LeftGate);


        var rm4 = GameManager.CreateRoom(GameManager.Rooms[2]);
        rm4.transform.position = Vector3.zero + new Vector3(150, 0, 0);
        rm3.RightGate.LinkTo(rm4.LeftGate);


        var rm5 = GameManager.CreateRoom(GameManager.Rooms[1]);
        rm5.transform.position = Vector3.zero + new Vector3(250, 0, 0);
        rm2.RightGate.LinkTo(rm5.LeftGate);

        VirtualCamera.m_BoundingShape2D = rm1.Limit;
        DeleteGate();
    }
    public static void DeleteGate()
    {
        foreach(var a in AllRooms)
        {
            if (a.LeftGate != null)
                if (a.LeftGate.Link == null)
                    Destroy(a.LeftGate.gameObject);
            if (a.RightGate != null)
                if (a.RightGate.Link == null)
                    Destroy(a.RightGate.gameObject);
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
    public static Room CreateRoom(GameObject room)
    {
        var rm =  Instantiate(room).GetComponent<Room>();
        AllRooms.Add(rm);
        return rm;
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
    public List<GameObject> Role = new List<GameObject>();
    public  List<GameObject> Room = new List<GameObject>();
}