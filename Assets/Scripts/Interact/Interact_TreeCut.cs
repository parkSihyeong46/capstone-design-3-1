using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_TreeCut : Interact
{
    public List<Item> spawnItemList = new List<Item>();

    private void Start()
    {
        itemID = Item.ItemID.Axe;
        spawnItemList.Add(ItemManager.Instance.GetItem((int)Item.ItemID.Wood).DeepCopy());
    }

    public override void DoInteract(Character character, Item.ItemID itemID)
    {
        Debug.Log("나무에 필요한 도구: " + this.itemID + "   들고있는 도구: " + itemID);

        Player_Manager player_Manager = character.GetComponent<Player_Manager>();
        
        if (itemID == this.itemID)
        {
            player_Manager.RunAnimation("Work");
            foreach (Item spawnItem in spawnItemList)
            {
                // 이 오브젝트 위치를 기반으로 아이템 생성
                ItemSpawnManager.Instance.SpawnItem(transform.position, spawnItem);
            }
            // 도구에 맞았으면 이 오브젝트를 Destroy 한다
            Destroy(gameObject);
        }
        else {return;}
    }

}
