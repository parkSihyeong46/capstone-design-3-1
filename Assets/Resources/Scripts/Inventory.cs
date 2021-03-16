using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public const int inventorySize = 36;
    private Item[] items = new Item[inventorySize];

    private void Start()
    {

    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
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
