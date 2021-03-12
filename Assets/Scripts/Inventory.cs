using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int inventorySize = 36;
    private Item[] items = new Item[inventorySize];

    public Inventory()
    {
        for(int i = 0; i < inventorySize; i ++)
        {
            items[i] = new Item();
        }
    }

    private void Start()
    {
        items[0].Items = Item.ItmeValue.Axe;  
    }

    public Item[] GetItems()
    {
        return items;
    }

}
