using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenSlots : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Item item;
    private int slotNumber;
    private Image itemIcon;
    private Text itemCountText;

    private void Start()
    {
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemCountText = transform.GetChild(1).GetComponent<Text>();
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
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 255);
    }

    public void RemoveSlot()
    {
        itemCountText.text = "";
        item = null;
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 0);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item == null)
            return;

        DragSlot.instance.dragSlot = this;
        DragSlot.instance.DragSetImage(itemIcon);

        DragSlot.instance.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (item == null)
            return;

        DragSlot.instance.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
        {
            Inventory.Instance.SwitchItem(slotNumber, DragSlot.instance.dragSlot.slotNumber);
        }
    }
}
