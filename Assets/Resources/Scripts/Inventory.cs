using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private static Inventory instance;
    public static Inventory Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new Inventory();
            }
            return instance;
        }
    }

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public const int inventorySize = 36;
    private Item[] items = new Item[inventorySize];

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        for (int i = 0; i < inventorySize; i++)
        {
            if(items[i] != null)
            {
                if (items[i].ItemId == item.ItemId && item.IsPrintCount)
                {
                    if (items[i].Count <= items[i].MaxCount)
                    {
                        items[i].Count += 1;
                        onChangeItem.Invoke();
                        return;
                    }
                }
            }
            else
            {
                items[i] = item;
                onChangeItem.Invoke();
                return;
            }
        }
    }

    public void SetItem(Item item, int index)
    {
        if (!(0 <= index && index < inventorySize))
            return;

        items[index] = item;
        onChangeItem.Invoke();
    }

    public void DeleteItem(int index)
    {
        if (!(0 <= index && index < inventorySize))
            return;

        items[index] = null;
        onChangeItem.Invoke();
    }

    public Item[] GetItems()
    {
        return items;
    }

}
