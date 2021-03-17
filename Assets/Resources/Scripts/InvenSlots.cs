using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlots : MonoBehaviour
{
    private Item item;
    [SerializeField]
    private Image itemIcon;

    public void SetSlotsItem(Item item)
    {
        this.item = item;
    }

    public void UpdateSlotUI()
    {
        if (item == null)
            return;

        itemIcon.sprite = item.ItemImage;
        itemIcon.enabled = true;
    }

    public void RemoveSlot()
    {
        item = null;
        itemIcon.enabled = false;
    }

}
