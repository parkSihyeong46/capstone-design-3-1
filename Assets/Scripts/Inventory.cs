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
        items[0] = new Item();
        items[0].ItemName = Item.ItmeNames.FriedEgg;
        items[3] = new Item();
        items[3].ItemName = Item.ItmeNames.Axe;
    }

    public void TestAdd()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                Debug.Log(i + "과연");
                items[i] = new Item();
                onChangeItem.Invoke();
                return;
            }
        }
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
