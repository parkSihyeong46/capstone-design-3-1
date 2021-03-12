using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Button : MonoBehaviour
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
}
