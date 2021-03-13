using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private ItmeNames itemName;  // 이름
    private string itemExplain; // 설명
    private Sprite itemImage;   // 이미지
    private ItemTypes itemType;  // 타입

    public ItmeNames ItemName
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

    public Item DeepCopy()
    {
        Item deepCopyItem = new Item
        {
            itemName = itemName,
            itemExplain = itemExplain,
            itemImage = itemImage,
            itemType = itemType
        };

        return deepCopyItem;
    }
    // 도구, 열매, 씨앗, 음식 순
    public enum ItmeNames
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
