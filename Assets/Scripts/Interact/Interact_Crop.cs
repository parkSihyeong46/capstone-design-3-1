using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Crop : Interact
{
    Crop_Grow crop; //작물이 가지고있는 스크립트
    bool checkCrop; //성장했는지 확인

    private void Start()
    {
        crop = GetComponent<Crop_Grow>();
    }

    private void FixedUpdate()
    {
        checkCrop = crop.isFullyGrown;
    }

    public override void DoInteract(Character character, Item.ItemID itemID)
    {
        Player_Manager player_Manager = character.GetComponent<Player_Manager>();

        Debug.Log("도구 가리지 않고 상관없이 상호작용");

        player_Manager.RunAnimation("PickUp_Item");

        if (checkCrop) //다 자란 경우
        {
            Inventory.Instance.AddItem(ItemManager.Instance.GetItem((int)Item.ItemID.Cauliflower).DeepCopy());
            GetItemUIManager.Instance.PrintUI(ItemManager.Instance.GetItem((int)Item.ItemID.Cauliflower));
        }
        Destroy(gameObject);    //다 자랐든 안 자랐든 Destroy
    }
}
