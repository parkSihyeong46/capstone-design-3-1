using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Cut : Interactable
{
    [SerializeField] GameObject pickUpDrops;
    [SerializeField] int dropCount;
    [SerializeField] float spread = 0.7f;

    Item_Get itemGet;
    Bounds bound;

    public override void Interact(Character character)
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 dropPosition = new Vector2(transform.position.x, transform.position.y - this.bound.extents.y * 10f);
            dropPosition.x += spread * UnityEngine.Random.value - spread / 2;
            dropPosition.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(pickUpDrops);
            go.transform.position = dropPosition;
            
        }

        // 도구에 맞았으면 이 오브젝트를 Destroy 한다
        Destroy(gameObject);
    }
}
