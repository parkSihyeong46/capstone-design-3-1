using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InvenSlots : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Item item;
    private int slotNumber;
    private Image itemIcon;
    private Text itemCountText;
    private InventoryUI.UILocation location;
    private DragSlot dragslot;
    private GameObject toolTipObject;
    private ToolTip toolTipScript;
    private bool isMouseOver = false;
    private void Awake()
    {
        toolTipObject = GameObject.FindWithTag("ToolTip");
        toolTipScript = toolTipObject.GetComponent<ToolTip>();
    }
    private void Start()
    {
        location = transform.parent.parent.GetComponent<InventoryUI>().GetUILocation();
        itemIcon = transform.GetChild(0).GetComponent<Image>();
        itemCountText = transform.GetChild(1).GetComponent<Text>();
        dragslot = transform.parent.parent.GetComponent<InventoryUI>().GetDragSlot();
    }
    private void Update()
    {
        if (!isMouseOver)
            return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = new Vector3(mousePosition.x + 20, mousePosition.y + 20, mousePosition.z);

        toolTipObject.transform.position = mousePosition;
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
                string itemId = item.ItemId.ToString();
                itemId = itemId.Substring(0,1).ToLower() + itemId.Substring(1, itemId.Length-1);   // json으로 받아오는 변수 이름의 첫글자가 소문자, 프로그램 변수는 대문자라서 변환 필요

                int itemPrice = PriceDataManager.instance.GetPrice(itemId);
                if (-1 == itemPrice)
                    return;

                OnPointerExit(null);
                Inventory.Instance.Money += itemPrice;
                Inventory.Instance.DeleteItem(slotNumber);
                OnPointerEnter(null);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null)
            return;

        isMouseOver = true;
        toolTipScript.SetColor(1);
        toolTipScript.SetToolTipItem(this.item);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null)
            return;

        isMouseOver = false;
        toolTipScript.SetColor(0);
    }
}
