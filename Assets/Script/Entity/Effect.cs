using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Effect, Missile
}
public class Effect : Entity
{
    public override void Init()
    {

    }
    public override void OnUpdate()
    {
        Duration -= Time.deltaTime;
        Moves();
        if (Duration <= 0)
            Death();
        if (ContinueAttack)
            ContAtk();
    }
    private void ContAtk()
    {
        DeleteClist();
        foreach (var a in list)
        {
            if (!CList.Contains(a))
            {
                CList.Add(a);
                ClearFloat.Add(Time.time + 0.5f);
                Damage.DealDamage(damage, Master.GetComponent<Role>(), a);
            }
        }
    }
    private void DeleteClist()
    {
        while(ClearFloat.Count > 0)
        {
            if (ClearFloat[0] > Time.time)
                break;
            ClearFloat.RemoveAt(0);
            CList.RemoveAt(0);
        }
    }
    private void Moves()
    {
        if (Move)
        {
            transform.position += new Vector3(Star.x, Star.y) * Time.deltaTime;
            Star += Delt * Time.deltaTime;
            return;
        }
        if (Follow)
        {
            transform.position = Master.transform.position + FollowPosition;
            return;
        }
    }
    public void Death()
    {
        Destroy(gameObject);
        Destroy(this);
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (OnlyEffect)
            return;
        if (collision.gameObject == Master)
            return;
        if (collision.gameObject.TryGetComponent<Role>(out var ent))
        {
            if (ContinueAttack)
            {
                list.Add(ent);
            }
            else
            {
                list.Add(ent);
                if (damage != null)
                    if (Master.GetComponent<Role>().faction != ent.faction)
                    {
                        Damage.DealDamage(damage,Master.GetComponent<Role>(), ent);
                        TriggerTimes--;
                    }
                Master.GetComponent<Role>().OnSucceedAttack(ent);
            }
        }
        if (collision.gameObject.TryGetComponent<Land>(out var lad))
        {
            if (land.Contains(lad))
                return;
            land.Add(lad);
            if (lad.CanAttacked)
            {
                var eff = Effect.Create(GameManager.Effect[2], Master, collision.transform.position);
                var direction = Lib.GetAngle(collision.transform.position.x - Master.transform.position.x, collision.transform.position.y - Master.transform.position.y);
                if (direction < 0)
                    direction += 180;
                if (direction >= 45 && direction < 135)
                    Lib.Rotate(eff.gameObject, 90);
                if (direction >= 135 && direction < 225)
                    Lib.Rotate(eff.gameObject, 180);
                if (direction >= 225 && direction < 315)
                    Lib.Rotate(eff.gameObject, 270);
                //Lib.Rotate(eff.gameObject, direction);
                Master.GetComponent<Role>().OnSucceedAttack(lad);
                eff.SetOnlyEffect();
            }
        }
        if (TriggerTimes == 0)
        {
            Death();
        }
    }











    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (OnlyEffect)
            return;
        if (ContinueAttack)
            if (collision.gameObject.TryGetComponent<Role>(out var ent))
                list.Remove(ent);
    }










    /// <summary>
    /// 使得这个东西可以根据一定的XY进行移动，并且带有变化。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="Mx"></param>
    /// <param name="y"></param>
    /// <param name="My"></param>
    public void SetMove(Vector2 start, Vector2 delt)
    {
        SetMove(start);
        Delt = delt;
    }
    /// <summary>
    /// 使得这个东西可以根据一定的XY进行移动。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="Mx"></param>
    /// <param name="y"></param>
    /// <param name="My"></param>
    public void SetMove(Vector2 move)
    {
        Move = true;
        Star = move;
    }
    /// <summary>
    /// 设置为纯特效，在该情况下，该特效不会参与任何碰撞！
    /// </summary>
    public void SetOnlyEffect()
    {
        OnlyEffect = true;
    }
    /// <summary>
    /// 设置改特效将会跟随 创建者
    /// </summary>
    public void SetFollow()
    {
        Follow = true;
        FollowPosition = new Vector3();
    }
    /// <summary>
    /// 设置特效会跟随创建者 在posisition的位置
    /// </summary>
    /// <param name="position">跟随位置的相对位移</param>
    public void SetFollow(Vector2 position)
    {
        Follow = true;
        FollowPosition = position;
    }
    public void SetDamage(Damage dam)
    {
        damage = dam;
        CanDealDamage = true;
    }
    public void SetTriggerTimes(int t)
    {
        TriggerTimes = t;
    }

    public void SetContinueAttack()
    {
        ContinueAttack = true;
        CList = new List<Role>();
        ClearFloat = new List<float>();
    }








    /// <summary>
    /// 是否只是一个特效，而不进行任何判定。
    /// </summary>
    private bool OnlyEffect = false;
    /// <summary>
    /// 特效的创建者
    /// </summary>
    public GameObject Master;
    /// <summary>
    /// 类型
    /// </summary>
    public EffectType Type;
    /// <summary>
    /// 剩余持续时间
    /// </summary>
    public float Duration = 1;
    /// <summary>
    /// 伤害
    /// </summary>
    public Damage damage;
    /// <summary>
    /// 是否能够造成伤害
    /// </summary>
    public bool CanDealDamage = false;
    private int TriggerTimes = -1;
    public Rigidbody2D rig;
    public Collision2D coll;
    public List<Role> list = new List<Role>();
    public List<Role> CList;
    public List<float> ClearFloat;
    public List<Land> land = new List<Land>();
    /// <summary>
    /// 速度，和加速度
    /// </summary>
    public Vector2 Star, Delt;

    private Vector3 FollowPosition;
    private bool Follow = false;
    private bool Move = false;
    private bool ContinueAttack = false;



















    /// <summary>
    /// 创建一个特效 返回创建的特效
    /// </summary>
    /// <param name="gameObject">创建的预制体</param>
    /// <param name="position">创建的位置</param>
    /// <param name="master">创建者</param>
    /// <param name="duration">持续时间</param>
    /// <param name="Angle">角度</param>
    /// <returns></returns>
    public static Effect Create(GameObject gameObject, GameObject master, float duration, Vector2 position, float Angle)
    {
        var ret = Create(gameObject, master, duration, position);
        var ang = ret.transform.eulerAngles;
        ang.z = Angle;
        ret.transform.eulerAngles = ang;
        return ret;
    }
    /// <summary>
    /// 创建一个特效 返回创建的特效
    /// </summary>
    /// <param name="gameObject">创建的预制体</param>
    /// <param name="position">创建的位置</param>
    /// <param name="master">创建者</param>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    public static Effect Create(GameObject gameObject, GameObject master, float duration, Vector2 position)
    {
        var ret = Create(gameObject, master, duration);
        ret.transform.position = position;
        return ret;
    }
    /// <summary>
    /// 创建一个特效 返回创建的特效
    /// </summary>
    /// <param name="gameObject">创建的预制体</param>
    /// <param name="master">创建者</param>
    /// <param name="duration">持续时间</param>
    /// <returns></returns>
    public static Effect Create(GameObject gameObject, GameObject master, float duration)
    {
        var go = Instantiate(gameObject);
        var ret = go.GetComponent<Effect>();
        if (ret == null)
        {
            Destroy(go);
            return null;
        }
        ret.Master = master;
        ret.Duration = duration;
        return ret;
    }
    /// <summary>
    /// 创建一个特效 返回创建的特效
    /// </summary>
    /// <param name="gameObject">创建的预制体</param>
    /// <param name="master">创建者</param>
    /// <returns></returns>
    public static Effect Create(GameObject gameObject, GameObject master)
    {
        var go = Instantiate(gameObject);
        var ret = go.GetComponent<Effect>();
        if (ret == null)
        {
            Destroy(go);
            return null;
        }
        ret.Master = master;
        return ret;
    }
    /// <summary>
    /// 创建一个特效 返回创建的特效
    /// </summary>
    /// <param name="gameObject">创建的预制体</param>
    /// <param name="master">创建者</param>
    /// <returns></returns>
    public static Effect Create(GameObject gameObject, GameObject master, Vector2 position)
    {
        var go = Instantiate(gameObject);
        go.transform.position = position;
        var ret = go.GetComponent<Effect>();
        if (ret == null)
        {
            Destroy(go);
            return null;
        }
        ret.Master = master;
        return ret;
    }

}