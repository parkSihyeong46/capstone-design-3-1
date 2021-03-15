using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory_Button : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text itemText;

    int myIndex;

    public void SetIndex_Button(int index)
    {
        myIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.icon;

        // 한 칸에 여러개 적재 가능한 아이템
        if(slot.item.stackable == true)
        {
            itemText.gameObject.SetActive(true);
            itemText.text = slot.count.ToString();
        }
        else
        {
            itemText.gameObject.SetActive(false);
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
        Item_Container inventory = GameManager.instance.itemContainer;
        GameManager.instance.itemDragDrop.OnClick(inventory.slots[myIndex]);
        transform.parent.GetComponent<Inventory_Panel>().Show();
    }
}
