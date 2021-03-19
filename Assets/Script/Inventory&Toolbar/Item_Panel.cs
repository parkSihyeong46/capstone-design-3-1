using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//인벤토리나 툴바의 기본이 되는 스크립트

public class Item_Panel : MonoBehaviour
{
    public Item_Container inventory; // ScriptableObject로 생성한거
    public List<Inventory_Button> buttons; // 자식요소로 들어있는 버튼들(슬롯)

    private void FixedUpdate()
    {
        Init(); // FixedUpdate로 계속 업데이트 해 줌으로써 아이템을 획득하거나 할 때 실시간으로 인벤토리 & 툴바에 반영
    }

    public void Init()
    {
        SetIndex_Panel();
        Show();
    }

    private void OnEnable()
    {
        Show();
    }

    void SetIndex_Panel()
    {
        // 인벤토리 칸 수만큼 버튼 인덱스 부여
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            buttons[i].SetIndex_Button(i);
        }
    }


    public void Show()
    {
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            // 인벤토리(패널 아님. 스크립터블오브젝트)에 아이템이 없을 때 패널 안에 있는 인벤토리 칸 정리. 아니면 버튼에 인벤토리에 있는 아이템 넣어줌
            if (inventory.slots[i].item == null)
            {
                buttons[i].Clean();
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
    
    public virtual void OnClick(int id)
    {
        // 클릭했을 때 사용하는 함수. 다른 스크립트에서 오버라이드해서 사용
    }
}
