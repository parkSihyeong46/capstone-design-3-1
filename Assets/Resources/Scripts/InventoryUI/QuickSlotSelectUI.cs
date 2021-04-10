using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotSelectUI : MonoBehaviour
{
    Vector2 wheelInput;

    private const int FIRST_SELECT_UI_X = -302;
    private const int LAST_SELECT_UI_X = 303;

    public int selectSlotNumber = 0;

    private Transform quickSlotSelectUI;

    private void Start()
    {
        quickSlotSelectUI = this.transform;
    }

    private void Update()
    {
        wheelInput = Input.mouseScrollDelta;

        if (wheelInput.y > 0) // 휠 올렸을 때
        {
            quickSlotSelectUI.Translate(Vector3.left * 55);

            if (quickSlotSelectUI.localPosition.x < FIRST_SELECT_UI_X)  // 범위 초과
            {
                quickSlotSelectUI.localPosition = new Vector3(LAST_SELECT_UI_X, quickSlotSelectUI.localPosition.y, 0);
            }

            SetSelectItem();
        }
        else if (wheelInput.y < 0) // 휠 내렸을 때
        {
            quickSlotSelectUI.Translate(Vector3.right * 55);

            if (quickSlotSelectUI.localPosition.x > LAST_SELECT_UI_X)   // 범위 초과
            {
                quickSlotSelectUI.localPosition = new Vector3(FIRST_SELECT_UI_X, quickSlotSelectUI.localPosition.y, 0);
            }

            SetSelectItem();
        }
    }

    void SetSelectItem()
    {
        selectSlotNumber = (int)(quickSlotSelectUI.localPosition.x - FIRST_SELECT_UI_X) / 55;   // 선택중인 퀵슬롯 아이템 인덱스 찾기

        Player_Manager.instance.handItem = Inventory.Instance.GetItems()[selectSlotNumber]; // 선택중인 아이템을 playerManager에서 참조하도록 설정
    }
}
