using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public Item(ItemID itemId, string itemName, string itemExplain, ItemTypes itemType, Sprite itemImage,  int count)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.itemExplain = itemExplain;
        this.itemType = itemType;
        this.itemImage = itemImage;
        this.count = count;
    }

    private ItemID itemId;      // 아이템 고유 ID
    private string itemName;    // 이름
    private string itemExplain; // 설명
    private ItemTypes itemType; // 타입
    private Sprite itemImage;   // 이미지
    private int count = 1;      // 개수

    public Item DeepCopy()
    {
        return new Item(itemId, itemName, itemExplain, itemType, itemImage, count);
    }

    public ItemID ItemId
    {
        set { itemId = value; }
        get { return itemId; }
    }
    public string ItemName
    {
        set { itemName = value; }
        get { return itemName; }
    }
    public string ItemExplain
    {
        set { itemExplain = value; }
        get { return itemExplain; }
    }
    public Sprite ItemImage
    {
        set { itemImage = value; }
        get { return itemImage; }
    }
    public ItemTypes ItemType
    {
        set { itemType = value; }
        get { return itemType; }
    }
    public int Count
    {
        set { count = value; }
        get { return count; }
    }

    // 도구, 열매, 씨앗, 음식 순
    public enum ItemID
    {
        Axe,
        Pick,
        Hoe,
        WateringCans,
        Parsnip,
        Cauliflower,
        Strawberry,
        Seeds,
        ParsnipSeed,
        CauliflowerSeed,
        StrawberrySeed,
        Icecream,
        SurvivalHambuger,
        FriedEgg,
        CheeseCauliflower
    }
    // 도구, 씨앗, 음식 순
    public enum ItemTypes
    {
        Tool,
        Seed,
        Food,
        Equipment,
    }
}
