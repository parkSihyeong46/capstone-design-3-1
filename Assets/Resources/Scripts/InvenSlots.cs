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
    private static int beginDragSlotNumber = -1;
    private bool isDragging = false;

    private Vector3 savePosition;
    private void Start()
    {
        itemCountText = transform.GetChild(0).GetComponent<Text>();
        itemIcon = transform.GetChild(1).GetComponent<Image>();
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

        savePosition = transform.position;
        beginDragSlotNumber = slotNumber;
        itemIcon.raycastTarget = false;
        isDragging = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        transform.position = savePosition;
        itemIcon.raycastTarget = true;

        Inventory.Instance.SwitchItem(beginDragSlotNumber, slotNumber);
    }
    public void OnDrop(PointerEventData eventData)
    {
        beginDragSlotNumber = slotNumber;
    }
}
