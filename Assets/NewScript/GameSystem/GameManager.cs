using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject player;
    private Inventory inventory;
    void Awake()
    {
        instance = this;
        inventory = Inventory.Instance;
    }
    private void Start()
    {
        // 장비 사용 테스트, 초심자세트 인벤에 생성
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Axe));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Pick));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Hoe));
        inventory.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.WateringCans));
    }


}
