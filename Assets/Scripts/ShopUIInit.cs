using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIInit : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryObject;
    [SerializeField]
    private GameObject[] itemObject = new GameObject[Inventory.inventorySize];

    private Item[] items = new Item[Inventory.inventorySize];

    private void OnEnable()
    {
        Debug.Log("캐릭터 인벤 정보를 통해 아이템 배열을 받아옴");
        Debug.Log("받아온 아이템 배열을 통해 아이템 없는 자리의 아이템 이미지 disabled");
        Debug.Log("아이템 있는 자리는 enabled, 해당 아이템으로 이미지 설정\n");

        Debug.Log("상인 정보 를 통해 물품 프리팹 생성");
        Debug.Log("contents에 자식으로 productButton 프리팹 생성 후 이미지, 상품이름, 가격 설정");

        items = inventoryObject.GetComponent<Inventory>().GetItems();

        for (int i = 0; i < Inventory.inventorySize; i++)
        {
            if (items[i].Items == Item.ItmeValue.None)
                itemObject[i].SetActive(false);
            else
                itemObject[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        Debug.Log("contents 자식 요소 모두 삭제");
    }
}
