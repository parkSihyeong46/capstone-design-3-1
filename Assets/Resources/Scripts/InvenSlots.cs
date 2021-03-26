using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvenSlots : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    private Item item;
    private int slotNumber;
    private Image itemIcon;
    private Text itemCountText;
    private InventoryUI.UILocation location;
    private DragSlot dragslot;

    private void Start()
    {
        location = transform.parent.parent.GetComponent<InventoryUI>().GetUILocation();
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemCountText = transform.GetChild(1).GetComponent<Text>();
        dragslot = transform.parent.parent.GetComponent<InventoryUI>().GetDragSlot();
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

    public void OnBeginDrag(PointerEventData eventData) // 드래그 시작 시
    {
        if (item == null)
            return;

        dragslot.dragSlot = this;
        dragslot.DragSetImage(itemIcon);
        dragslot.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)  // 드래그 중일 경우
    {
        if (item == null)
            return;

        dragslot.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)   // 드래그 중지 시
    {
        dragslot.SetColor(0);
        dragslot.dragSlot = null;
    }
    public void OnDrop(PointerEventData eventData)  // 드래그 드롭 시 
    {
        if(dragslot.dragSlot != null)
        {
            Inventory.Instance.SwitchItem(slotNumber, dragslot.dragSlot.slotNumber);    // 아이템 스위치
        }
    }

    public void OnPointerClick(PointerEventData eventData)  // 클릭 처리
    {
        if (item == null)
            return;

        if(InventoryUI.UILocation.Shop == location) // 상점 UI의 인벤토리 인 경우
        {
            if (eventData.button == PointerEventData.InputButton.Right) // 우클릭 시
            {
                // 아직 아이템 시세를 반영 안해서 debug로 일단 처리 중
                Debug.Log(item.ItemName + " 팔기");
                // 캐릭터 돈 ++
                Inventory.Instance.DeleteItem(slotNumber);
            }
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("onMuseEnter");
    }
    private void OnMouseOver()
    {
        Debug.Log("onMuseOver");
    }
    private void OnMouseExit()
    {
        Debug.Log("onmouseExit");
    }
}
