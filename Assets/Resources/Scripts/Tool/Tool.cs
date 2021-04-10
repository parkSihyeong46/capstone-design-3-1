using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool
{
    public Tool(Item.ItemID itemId, string itemName, int useStamina, int workLoad)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.useStamina = useStamina;
        this.workLoad = workLoad;
    }

    private Item.ItemID itemId;     // 아이템 고유 ID
    private string itemName;        // 이름
    private int useStamina;         // 사용 스태미나 양
    private int workLoad;           // 작업량

    public Item.ItemID ItemId
    {
        set { itemId = value; }
        get { return itemId; }
    }
    public string ItemName
    {
        set { itemName = value; }
        get { return itemName; }
    }
    public int UseStamina
    {
        set { useStamina = value; }
        get { return useStamina; }
    }
    public int WorkLoad
    {
        set { workLoad = value; }
        get { return workLoad; }
    }

}
