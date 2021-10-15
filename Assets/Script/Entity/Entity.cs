using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionType
{
    Player,Friend,Enemy
}

public class Entity : MonoBehaviour
{
    public FactionType faction;
    public virtual void Init() { }
    public virtual void OnUpdate() { }
    void Start()
    {
        Init();   
    }

    void Update()
    {
        if (GameManager.IsStop)
            return;
        OnUpdate();   
    }
}
