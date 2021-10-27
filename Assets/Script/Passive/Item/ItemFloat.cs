using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    bool up = false;
    float TimeCount = -1;
    public int ItemId = 0;
    public GameObject fat;

    [Obsolete]
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = GameManager.ItemImage[ItemId];
        var child = Instantiate(GameManager.Particle[2],gameObject.transform);
        child.transform.position = transform.position;
        var p = child.GetComponent<ParticleSystem>();
        p.startColor = GameManager.FindColor((ItemRare)Enum.Parse(typeof(ItemRare),GameManager.ItemData[ItemId].ItemQuality));
    }
    void Update()
    {
        TimeCount += Time.deltaTime;
        if (TimeCount >= 1)
            TimeCount = -1;
        Move(Mathf.Sin(Mathf.PI * TimeCount) * 0.3f);
    }
    private void Move(float A)
    {
        var tr = transform.position;
        tr.y += (up?A:-A) * Time.deltaTime;
        transform.position = tr;
    }
    public void Delete()
    {
        GameManager.DestoryItem(fat);
    }
    public void GetItem(Role role)
    {
        role.GetItem(ItemId);
        Delete();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject);
        if (collision.gameObject.TryGetComponent<Player>(out Player role)){
            GetItem(role);
        }
    }
}
