using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar_Panel : Item_Panel
{
    [SerializeField] ToolBar toolBar;
    int selectedSlotIndex;

    private void Start()
    {
        Init();                         //시작할 때 툴바
        toolBar.onChange += Highlight;  //델리게이트 체인(onChange에 Highlight 메소드 연결. 연결을 끊으려면 -= 해줌). 액션이 실행될 때마다 Highlight 실행
        Highlight(0);                   //초기 선택 슬롯은 0번째 슬롯
    }

    //클릭했을 때 툴바 세팅, 하이라이트 표시
    public override void OnClick(int id)
    {
        toolBar.Set(id);    // ToolBar의 Set함수 실행
        Highlight(id);      // 선택된 아이템칸(툴바)에 하이라이트 표시
    }

    int currentSelectedItem;    //아직 아무 값도 들어있지 않음

    public void Highlight(int id)
    {   
        //스크롤 할 때마다 

        buttons[currentSelectedItem].ToolbarHighlight(false);  //현재 선택된 하이라이트 해제
        currentSelectedItem = id;                              //선택될 아이템으로 교체
        buttons[currentSelectedItem].ToolbarHighlight(true);   //선택된 아이템에 하이라이트 표시
    }
}
