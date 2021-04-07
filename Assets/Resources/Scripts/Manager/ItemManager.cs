using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ItemManager
{
    private static ItemManager instance;
    private const string itemDataPath = "Data/itemData";
    private TextAsset textAsset;
    private List<Item> itemList = new List<Item>();

    public static ItemManager Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new ItemManager();
            }
            return instance;
        }
    }

    private ItemManager()
    {
        textAsset = Resources.Load<TextAsset>(itemDataPath);
        string[] line = textAsset.text.Substring(0, textAsset.text.Length - 1).Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            try
            {
                itemList.Add(new Item((Item.ItemID)Enum.Parse(typeof(Item.ItemID), row[0]),
                    row[1],
                    row[2],
                    (Item.ItemTypes)Enum.Parse(typeof(Item.ItemTypes), row[3]),
                    Resources.Load<Sprite>(row[4]),
                    row[5].Equals("TRUE"),
                    Int32.Parse(row[6]),
                    1));
            }
            catch
            {
                itemList.Add(null);
            }
        }
    }

    public Item GetItem(int index)
    {
        if (0 <= index && index < itemList.Count)
            return new Item(itemList[index].ItemId, itemList[index].ItemName, 
                itemList[index].ItemExplain, itemList[index].ItemType, 
                itemList[index].ItemImage, itemList[index].IsPrintCount, 
                itemList[index].MaxCount, itemList[index].Count);
        else
            return null;
    }
}
