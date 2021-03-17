using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private UILocation uiLocation;
    private InvenSlots[] invenSlot;

    private void Start()
    {
        Inventory.Instance.onChangeItem += RedrawUI;
        switch(uiLocation)
        {
            case UILocation.Inven:
                var list = new List<InvenSlots>();

                for (int i = 0; i < transform.childCount; i ++)
                    list.AddRange(transform.GetChild(i).GetComponentsInChildren<InvenSlots>());

                invenSlot = list.ToArray();
                break;
            case UILocation.Shop:
                invenSlot = GetComponentsInChildren<InvenSlots>();
                break;
        }

        for(int i = 0; i <Inventory.inventorySize; i ++)
        {
            invenSlot[i].SlotNumber = i;
        }

        RedrawUI();
    }

    public void RedrawUI()
    {
        Item[] items = Inventory.Instance.GetItems();

        for (int i = 0; i < Inventory.inventorySize; i++)
        {
            invenSlot[i].RemoveSlot();
        }

        for(int i = 0; i < Inventory.inventorySize; i++)
        {
            if (items[i] == null)
                continue;

            invenSlot[i].SetSlotsItem(items[i].DeepCopy());
            invenSlot[i].UpdateSlotUI();
        }
    }
    public enum UILocation
    {
        Inven,
        Shop,
    }
}
