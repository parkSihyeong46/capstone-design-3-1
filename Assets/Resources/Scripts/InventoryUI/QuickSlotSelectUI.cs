using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class QuickSlotSelectUI : MonoBehaviour
{
    Vector2 wheelInput;

    private const int FIRST_SLOT_NUMBER = 0;
    private const int LAST_SLOT_NUMBER = 11;

    private const int FIRST_SELECT_UI_X = -302;
    private const int OTHER_SELECT_UI_DISTANCE = 55;    // 다른 select ui와의 간격

    public int selectSlotNumber = 0;

    private Transform quickSlotSelectUI;
    private Dictionary<KeyCode, Action> keyDictionary;

    private void Start()
    {
        quickSlotSelectUI = this.transform;
        SetSelectItem(selectSlotNumber);
        InitKeyDictionary();
    }
    void InitKeyDictionary()
    {
        keyDictionary = new Dictionary<KeyCode, Action>
        {
            // 상단 키패드
            {KeyCode.Alpha1, KeyDown_1 },
            {KeyCode.Alpha2, KeyDown_2 },
            {KeyCode.Alpha3, KeyDown_3 },
            {KeyCode.Alpha4, KeyDown_4 },
            {KeyCode.Alpha5, KeyDown_5 },
            {KeyCode.Alpha6, KeyDown_6 },
            {KeyCode.Alpha7, KeyDown_7 },
            {KeyCode.Alpha8, KeyDown_8 },
            {KeyCode.Alpha9, KeyDown_9 },
            {KeyCode.Alpha0, KeyDown_0 },
            {KeyCode.Minus, KeyDown_Minus },
            {KeyCode.Equals, KeyDown_Equals },
            // 우측 키패드
            {KeyCode.Keypad1, KeyDown_1 },
            {KeyCode.Keypad2, KeyDown_2 },
            {KeyCode.Keypad3, KeyDown_3 },
            {KeyCode.Keypad4, KeyDown_4 },
            {KeyCode.Keypad5, KeyDown_5 },
            {KeyCode.Keypad6, KeyDown_6 },
            {KeyCode.Keypad7, KeyDown_7 },
            {KeyCode.Keypad8, KeyDown_8 },
            {KeyCode.Keypad9, KeyDown_9 },
            {KeyCode.Keypad0, KeyDown_0 },
            {KeyCode.KeypadMinus, KeyDown_Minus },
            {KeyCode.KeypadEquals, KeyDown_Equals },
        };
    }
    private void Update()
    {
        SetSelectNumberFromKeyboard();

        wheelInput = Input.mouseScrollDelta;

        if (wheelInput.y > 0) // 휠 올렸을 때
        {
            SetSelectItem(--selectSlotNumber);
        }
        else if (wheelInput.y < 0) // 휠 내렸을 때
        {
            SetSelectItem(++selectSlotNumber);
        }
    }

    void SetSelectItem(int selectSlotNumber)
    {
        if (selectSlotNumber < FIRST_SLOT_NUMBER)
        {
            this.selectSlotNumber = LAST_SLOT_NUMBER;
        }
        else if (selectSlotNumber > LAST_SLOT_NUMBER)
        {
            this.selectSlotNumber = FIRST_SLOT_NUMBER;
        }
        else
        {
            this.selectSlotNumber = selectSlotNumber;
        }

        float newPositionX = (float)FIRST_SELECT_UI_X + ((float)this.selectSlotNumber * OTHER_SELECT_UI_DISTANCE);
        quickSlotSelectUI.localPosition = new Vector3(newPositionX, quickSlotSelectUI.localPosition.y, 0);

        Player_Manager.instance.handItem = Inventory.Instance.GetItems()[this.selectSlotNumber]; // 선택중인 아이템을 playerManager에서 참조하도록 설정
    }
    void SetSelectNumberFromKeyboard()
    {
        if (!Input.anyKeyDown)
            return;

        foreach(var dic in keyDictionary)
        {
            if(Input.GetKeyDown(dic.Key))
            {
                dic.Value();
            }
        }
    }

    void KeyDown_1() { SetSelectItem(0); }
    void KeyDown_2() { SetSelectItem(1); }
    void KeyDown_3() { SetSelectItem(2); }
    void KeyDown_4() { SetSelectItem(3); }
    void KeyDown_5() { SetSelectItem(4); }
    void KeyDown_6() { SetSelectItem(5); }
    void KeyDown_7() { SetSelectItem(6); }
    void KeyDown_8() { SetSelectItem(7); }
    void KeyDown_9() { SetSelectItem(8); }
    void KeyDown_0() { SetSelectItem(9); }
    void KeyDown_Minus() { SetSelectItem(10); }
    void KeyDown_Equals() { SetSelectItem(11); }
}
