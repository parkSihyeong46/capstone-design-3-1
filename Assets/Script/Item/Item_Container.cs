using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class ItemSlot
{
    public Item item;
    public int count;
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class Item_Container : ScriptableObject
{
    public List<ItemSlot> slots;
}
