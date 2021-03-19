using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    [SerializeField] int toolBarSize = 11;
    int selectedItem;

    public Action<int> onChange; //메소드를 직접 실행하지 않고 대신해서 호출하는 역할(메소드의 대리)

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y; //마우스 휠 동작(위 아래 스크롤)
        
        //휠 스크롤 했을 때 동작
        if (delta != 0)
        {
            if(delta < 0)
            {
                selectedItem += 1;
                selectedItem = selectedItem >= toolBarSize ? 0 : selectedItem;
            }
            else
            {
                selectedItem -= 1;
                selectedItem = selectedItem < 0 ? toolBarSize - 1 : selectedItem;
            }
            onChange?.Invoke(selectedItem);
        }
    }

    internal void Set(int id)
    {
        selectedItem = id;  // 선택된 아이템 값(int)을 id(--에서 받아옴)로 변경
    }
}
