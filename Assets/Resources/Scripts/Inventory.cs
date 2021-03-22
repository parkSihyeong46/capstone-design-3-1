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
            if (items[i] != null)
            {
                if ((items[i].ItemId == item.ItemId) && item.IsPrintCount)
                {
                    if (items[i].Count <= items[i].MaxCount)
                    {
                        items[i].Count += count;
                        onChangeItem.Invoke();  // 게임매니저를 통해서 인벤, 상점 열린지 확인 후 invoke 할 수 있도록 if문 추가 해야 함
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                onChangeItem.Invoke(); // 게임매니저를 통해서 인벤, 상점 열린지 확인 후 invoke 할 수 있도록 if문 추가 해야 함
                return;
            }
        }
    }

    public void SetItem(Item item, int index)
    {
        if (!(0 <= index && index < inventorySize))
            return;

        items[index] = item;
        onChangeItem.Invoke(); // 게임매니저를 통해서 인벤, 상점 열린지 확인 후 invoke 할 수 있도록 if문 추가 해야 함
    }

    public void DeleteItem(int index, int count = 1)
    {
        if (!(0 <= index && index < inventorySize))
            return;

        if(items[index].Count > count)
            items[index].Count -= count;
        else
            items[index] = null;

        onChangeItem.Invoke(); // 게임매니저를 통해서 인벤, 상점 열린지 확인 후 invoke 할 수 있도록 if문 추가 해야 함
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

        onChangeItem.Invoke(); // 게임매니저를 통해서 인벤, 상점 열린지 확인 후 invoke 할 수 있도록 if문 추가 해야 함
    }
    public Item[] GetItems()
    {
        return items;
    }

}
