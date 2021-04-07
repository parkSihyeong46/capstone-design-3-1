using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SpawnManager : MonoBehaviour
{
    public static Item_SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject pickUpItemPrefeb;

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        GameObject gameObject = Instantiate(pickUpItemPrefeb, position, Quaternion.identity);   // Quaternion.identity은 Rotate를 0으로 한다는 의미
        gameObject.GetComponent<Item_Get>().Set(item, count);
    }
}
