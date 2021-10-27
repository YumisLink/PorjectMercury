using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffVoidCorrosion : Buff
{
    public override void Init()
    {
        Type = BuffType.VoidCorrosion;
        IsStack = true;
        BuffName = "虚空腐蚀";
    }
}
