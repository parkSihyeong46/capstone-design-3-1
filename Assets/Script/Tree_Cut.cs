using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Cut : Tool_Hit
{
    [SerializeField] GameObject pickUpDrops;
    [SerializeField] int dropCount;
    [SerializeField] float spread = 0.7f;

    Item_Get itemGet;

    public override void Hit()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 dropPosition = transform.position;
            dropPosition.x += spread * UnityEngine.Random.value - spread / 2;
            dropPosition.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(pickUpDrops);
            go.transform.position = dropPosition;
            
        }


        // 도구에 맞았으면 이 오브젝트를 Destroy 한다
        Destroy(gameObject);
    }
}
