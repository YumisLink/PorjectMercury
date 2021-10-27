using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider :MonoBehaviour
{
    public GameObject child;
    private void Start()
    {
        child.GetComponent<ItemFloat>().fat = gameObject;
    }
}
