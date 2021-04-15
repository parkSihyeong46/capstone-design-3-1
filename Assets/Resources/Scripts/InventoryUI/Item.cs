using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public Item(ItemID itemId, string itemName, string itemExplain, 
        ItemTypes itemType, Sprite itemImage,
         bool isPrintCount, int maxCount, int count)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.itemExplain = itemExplain;
        this.itemType = itemType;
        this.itemImage = itemImage;
        this.isPrintCount = isPrintCount;
        this.maxCount = maxCount;
        this.count = count;
    }

    private ItemID itemId;      // 아이템 고유 ID
    private string itemName;    // 이름
    private string itemExplain; // 설명
    private ItemTypes itemType; // 타입
    private Sprite itemImage;   // 이미지
    private bool isPrintCount;  // 아이템 개수 출력 유무
    private int maxCount = 1;   // 소지 제한 개수
    private int count = 1;      // 개수

    public Item DeepCopy()
    {
        return new Item(itemId, itemName, itemExplain, itemType, itemImage, isPrintCount, maxCount, count);
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
    public bool IsPrintCount
    {
        set { isPrintCount = value; }
        get { return isPrintCount; }
    }
    public int MaxCount
    {
        set { maxCount = value; }
        get { return maxCount; }
    }
    public int Count
    {
        set { count = value; }
        get { return count; }
    }
    
    // 도구, 열매, 씨앗, 음식 순
    public enum ItemID
    {
        Axe = 0,
        Pick = 1,
        Hoe = 2,
        WateringCans = 3,
        Parsnip = 4,
        Cauliflower = 5,
        Strawberry = 6,
        Seeds = 7,
        ParsnipSeed = 8,
        CauliflowerSeed = 9,
        StrawberrySeed = 10,
        Icecream = 11,
        SurvivalHambuger = 12,
        FriedEgg = 13,
        CheeseCauliflower = 14,
        Wood = 15,
        Rock = 16,
    }
    // 도구, 씨앗, 음식, 재료 순
    public enum ItemTypes
    {
        Tool = 0,
        Seed = 1,
        Food = 2,
        Equipment = 3,
        Material = 4,
    }
}
