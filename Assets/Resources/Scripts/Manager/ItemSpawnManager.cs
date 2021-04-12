using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager
{
    private static ItemSpawnManager instance;
    private List<GameObject> spawnItemList = new List<GameObject>();
    private GameObject parentObject;

    private const string spawnItemPrefabPath = "Prefabs/spawnItem/SpawnItem";

    public static ItemSpawnManager Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new ItemSpawnManager();
            }
            return instance;
        }
    }

    private ItemSpawnManager() 
    {
        parentObject = new GameObject("SpawnItems");
    }

    public void SpawnItem(Vector3 position, Item item)
    {
        if (item == null)
            return;

        GameObject newSpawnItem = Object.Instantiate(Resources.Load<GameObject>(spawnItemPrefabPath));
        newSpawnItem.transform.parent = parentObject.transform;

        spawnItemList.Add(newSpawnItem);

        newSpawnItem.transform.position = position;
        newSpawnItem.GetComponent<SpriteRenderer>().sprite = item.ItemImage;
    }
}
