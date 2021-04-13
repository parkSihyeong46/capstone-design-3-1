using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IPointerClickHandler
{
    public Shop.CellItem cellItem;

    public void RepaintPrintData(Shop.CellItem cellItem)
    {
        if(cellItem != null)
        {
            transform.GetChild(0).GetComponent<Text>().text = cellItem.item.ItemName;
            if (-1 != cellItem.price)
                transform.GetChild(1).GetComponent<Text>().text = cellItem.price.ToString();
            transform.GetChild(2).GetComponent<Image>().sprite = cellItem.item.ItemImage;

            this.cellItem = cellItem;
        }
        else
        {
            transform.GetChild(1).GetComponent<Text>().text = "load error";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if (0 >= cellItem.price)
                return;

            if (cellItem.price > Inventory.Instance.Money)  // 돈 부족하면 종료
                return;

            Inventory.Instance.Money -= cellItem.price;
            Inventory.Instance.AddItem(cellItem.item);

            // 스타듀 밸리처럼 구입하면 손에 들고있다가 옮기는 것도 추가하면 좋을듯
        }      
    }
}
