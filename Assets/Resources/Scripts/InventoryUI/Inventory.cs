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
                instance = new Inventory();
            }
            return instance;
        }
    }

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public const int inventorySize = 36;
    private Item[] items = new Item[inventorySize];
    private int money = 1000;
    public int Money
    {
        set { money = value; }
        get { return money; }
    }
    public void AddItem(Item item, int count = 1)
    {
        if (item == null)
            return;

        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
                continue;

            if (!((items[i].ItemId == item.ItemId) && item.IsPrintCount))
                continue;

            if (items[i].Count <= items[i].MaxCount)
            {
                items[i].Count += count;

                if(onChangeItem != null)
                    onChangeItem.Invoke();

                return;
            }
        }

        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;

                if (onChangeItem != null)
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

        if (onChangeItem != null)
            onChangeItem.Invoke();
    }

    public void DeleteItem(int index, int count = 1)
    {
        if (!(0 <= index && index < inventorySize))
            return;

        if (items[index] == null)
            return;

        if(items[index].Count > count)
            items[index].Count -= count;
        else
            items[index] = null;

        if (onChangeItem != null)
            onChangeItem.Invoke();
    }

    public void SwitchItem(int index1, int index2)
    {
        if (index1 == index2)
            return;
        if (!(0 <= index1 && index1 < items.Length))
            return;
        if (!(0 <= index2 && index2 < items.Length))
            return;

        Item tempItem;

        tempItem = items[index1];
        items[index1] = items[index2];
        items[index2] = tempItem;

        if (onChangeItem != null)
            onChangeItem.Invoke();
    }
    public Item[] GetItems()
    {
        return items;
    }

}
