using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager
{
    private static ItemSpawnManager instance;
    private List<GameObject> fieldItemList = new List<GameObject>();
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

    public void SpawnItem(Vector3 position, Item item, int maxCount = 1, int minCount = 1)
    {
        if (item == null)
            return;

        if (maxCount <= 0)
            return;

        if (minCount < 0)
            return;

        if(maxCount < minCount)
        {
            int tempNumber;

            tempNumber = maxCount;
            maxCount = minCount;
            minCount = tempNumber;
        }

        int count = Random.Range(minCount, maxCount+1); // mincount ~ maxCount 값 랜덤 생성

        for(int i = 0; i < count; i ++)   // 위에서 생성한 아이템 생성 개수만큼 아이템 생성
        {
            GameObject newSpawnItem = Object.Instantiate(Resources.Load<GameObject>(spawnItemPrefabPath));  // 필드아이템 오브젝트 생성
            newSpawnItem.transform.parent = parentObject.transform; // 부모 오브젝트 설정, (하이어라키 정리)

            fieldItemList.Add(newSpawnItem);

            newSpawnItem.GetComponent<SpriteRenderer>().sprite = item.ItemImage;    // 필드아이템 이미지 변경
            newSpawnItem.transform.position = position; // 위치 설정
            newSpawnItem.GetComponent<ItemSpawnAnimation>().StartSpawnAnimation();  // 스폰 애니메이션 시작
        }
    }
}
