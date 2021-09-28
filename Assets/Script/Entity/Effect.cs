using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : Entity
{
    private bool OnlyEffect = false;
    public GameObject Master;
    /// <summary>
    /// 剩余持续时间
    /// </summary>
    public float Duration = 1;
    public Damage damage;
    public bool CanDealDamage = false;
    public Rigidbody2D rig;
    public Collision2D coll;
    public List<Entity> list = new List<Entity>();
    public List<Land> land = new List<Land>();

    public bool Follow;
    private Vector3 FollowPosition;
    public override void Init()
    {

    }
    public override void OnUpdate()
    {
        Duration -= Time.deltaTime;
        if (Follow)
            transform.position = Master.transform.position + FollowPosition;
        if (Duration <= 0)
            Death();
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
            list.Add(ent);
            if (damage != null)
                if (Master.GetComponent<Role>().faction != ent.faction)
                    Damage.DealDamage(damage,Master.GetComponent<Role>(), ent);
            Master.GetComponent<Role>().OnSucceedAttack(ent);
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
    }
    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (OnlyEffect)
            return;
        //var ent = collision.gameObject.GetComponent<Entity>();
        //list.Remove(ent);
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








    /// <summary>
    /// 创建一个特效 返回创建的特效
    /// </summary>
    /// <param name="gameObject">创建的预制体</param>
    /// <param name="position">创建的位置</param>
    /// <param name="master">创建者</param>
    /// <param name="duration">持续时间</param>
    /// <param name="Angle">角度</param>
    /// <returns></returns>
    public static Effect Create(GameObject gameObject, GameObject master, float duration, Vector2 position,float Angle)
    {
        var ret = Create(gameObject, master, duration,position);
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
    public static Effect Create(GameObject gameObject,GameObject master,float duration, Vector2 position)
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
