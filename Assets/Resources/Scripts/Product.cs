using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Product : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    public Item ProductItem
    {
        set { item = value; }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            // 돈 빠지는 코드 추가 해야 함
            // 클릭하면 바로 추가되는게 아니라 스타듀밸리처럼 손에 들고있다가 원하는 위치에 놓을 수 있도록 수정하기
            Inventory.Instance.AddItem(item);
        }
    }
}
