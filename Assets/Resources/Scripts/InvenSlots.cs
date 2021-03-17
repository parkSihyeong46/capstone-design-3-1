using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenSlots : MonoBehaviour
{
    private Item item;
    private int slotNumber;
    private Image itemIcon;
    private Text itemCountText;

    private void Start()
    {
        itemIcon = gameObject.GetComponent<Image>();
        itemCountText = transform.GetChild(0).GetComponent<Text>();
    }

    public int SlotNumber
    {
        set { slotNumber = value; }
        get { return slotNumber; }
    }
    public void SetSlotsItem(Item item)
    {
        this.item = item;
    }

    public void UpdateSlotUI()
    {
        if (item == null)
            return;

        if(item.IsPrintCount)
            itemCountText.text = item.Count.ToString();

        itemIcon.sprite = item.ItemImage;
        itemIcon.enabled = true;
    }

    public void RemoveSlot()
    {
        itemCountText.text = "";
        item = null;
        itemIcon.enabled = false;
    }
}
