using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Panel : MonoBehaviour
{
    [SerializeField] Item_Container inventory; // ScriptableObject로 생성한거
    [SerializeField] List<Inventory_Button> buttons; // 자식요소로 들어있는 버튼들(슬롯)

    private void Start()
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
        for(int i = 0; i < inventory.slots.Count; i++)
        {
            buttons[i].SetIndex_Button(i);
        }
    }

    public void Show()
    {
        for(int i = 0; i < inventory.slots.Count; i++)
        {
            // 인벤토리(패널 아님. 스크립터블오브젝트)에 아이템이 없을 때 
            if(inventory.slots[i].item == null)
            {
                buttons[i].Clean(); // 패널 안에 있는 인벤토리 칸 정리
            }
            else
            {
                buttons[i].Set(inventory.slots[i]);
            }
        }
    }
}
