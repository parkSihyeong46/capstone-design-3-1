using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text itemText;
    [SerializeField] Image highlight;

    int myIndex;

    public void SetIndex_Button(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.icon;   // 아이템칸 이미지를 아이템 아이콘으로

        // 한 칸에 여러개 적재 가능한 아이템
        if(slot.item.stackable == true)
        {
            itemText.gameObject.SetActive(true);
            icon.gameObject.SetActive(true);
            itemText.text = slot.count.ToString();
        }
        else
        {
            itemText.gameObject.SetActive(false);   // 개수가 단 하나뿐이므로 숫자 표시 X
            icon.gameObject.SetActive(true);        // 이미지는 당연히 보여줘야함

        }
    }

    // 인벤토리 칸 비우기
    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);

        itemText.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Item_Panel itemPanel = transform.parent.GetComponent<Item_Panel>();
        itemPanel.OnClick(myIndex);
    }

    public void Highlight(bool act)
    {
        highlight.gameObject.SetActive(act);
    }
}
