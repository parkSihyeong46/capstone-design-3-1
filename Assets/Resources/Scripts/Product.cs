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
            if (-1 == cellItem.price)
                return;

            // 돈 빠지는 코드 추가 해야 함
            // 클릭하면 바로 추가되는게 아니라 스타듀밸리처럼 손에 들고있다가 원하는 위치에 놓을 수 있도록 수정하기
            Inventory.Instance.AddItem(cellItem.item);
        }
    }
}
