using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Panel : Item_Panel
{
    public override void OnClick(int id)
    {
        GameManager.instance.itemDragDrop.OnClick(inventory.slots[id]);
        Show();
    }
}
