using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Cut : Tool_Hit
{
    public override void Hit()
    {
        // 도구에 맞았으면 이 오브젝트를 Destroy 한다
        Destroy(gameObject);
    }
}
