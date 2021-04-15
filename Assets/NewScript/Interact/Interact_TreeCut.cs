using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_TreeCut : Interact
{
    public List<Item> spawnItemList = new List<Item>();

    private void Start()
    {
        useTool = UseTool.Axe;
        spawnItemList.Add(ItemManager.Instance.GetItem(15).DeepCopy());
    }

    public override void DoInteract(Character character)
    {
        foreach (Item spawnItem in spawnItemList)
        {
            // 이 오브젝트 위치를 기반으로 아이템 생성
            ItemSpawnManager.Instance.SpawnItem(transform.position, spawnItem);
        }

        // 도구에 맞았으면 이 오브젝트를 Destroy 한다
        Destroy(gameObject);
    }

}
