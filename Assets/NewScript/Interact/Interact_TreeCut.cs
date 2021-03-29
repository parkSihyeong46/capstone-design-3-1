using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_TreeCut : Interact
{
    BoxCollider2D boxCollider;
    Bounds bound;

    [SerializeField] GameObject pickUpDrops;     //아이템(눈에 보이는 아이템)이 떨어지는데
    [SerializeField] float spread = 0.7f;        //이만큼의 분산도를 갖고
    [SerializeField] Item item;                  //드롭할 아이템(정보. 눈에 보이지 않는 아이템)은 상호작용할 오브젝트에 따라 다름
    [SerializeField] int itemCountInOneDrop = 1; //한번 드롭할 때 개수(예를들면 통나무 한토막이 2개로 카운트 되는 것)
    [SerializeField] int dropCount = 5;          //드롭할 아이템 개수

    Item_Get itemGet;

    private void Start()
    {
        bound = GetComponent<BoxCollider2D>().bounds;
    }

    public override void DoInteract(Character character)
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 dropPosition = new Vector2(transform.position.x, transform.position.y + this.bound.size.y); //아이템이 나타날 좌표
            dropPosition.x += spread * UnityEngine.Random.value - spread / 2;   // 분산값만큼 x좌표를 랜덤으로 배치
            dropPosition.y += spread * UnityEngine.Random.value - spread / 2;   // 분산값만큼 y좌표를 랜덤으로 배치

            Item_SpawnManager.instance.SpawnItem(dropPosition, item, itemCountInOneDrop);
        }

        // 도구에 맞았으면 이 오브젝트를 Destroy 한다
        Destroy(gameObject);
    }
}
