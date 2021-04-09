using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Text weekText;
    private Text dayText;
    private Text curTimeText;
    private Text ampmText;

    // 현재 시간 가져오는 코드 필요함
    private void Awake()
    {
        weekText = transform.GetChild(1).GetComponent<Text>();
        dayText = transform.GetChild(2).GetComponent<Text>();
        curTimeText = transform.GetChild(3).GetComponent<Text>();
        ampmText = transform.GetChild(4).GetComponent<Text>();
    }

    private void SetWeekText()
    {
        // switch의 0 나중에 현재 요일로 바꾸기
        int week = 0;
        switch(week)    // 0~ 6 나중에 enum으로 바꾸기 월~일
        {
            case 0:
                weekText.text = "월";
                break;
            case 1:
                weekText.text = "화";
                break;
            case 2:
                weekText.text = "수";
                break;
            case 3:
                weekText.text = "목";
                break;
            case 4:
                weekText.text = "금";
                break;
            case 5:
                weekText.text = "토";
                break;
            case 6:
                weekText.text = "일";
                break;
        }
    }
    private void SetDayText()
    {
        int day = 1;    // day 나중에 받아오기
        dayText.text = day.ToString();
    }
    private void SetCurTimeText()
    {
        int hour = 12;
        int minute = 30;
        curTimeText.text = hour + ":" + minute;
    }
    private void SetAmPmText()
    {
        bool isAm = false;

        if(isAm)
        {
            ampmText.text = "오전";
        }
        else
        {
            ampmText.text = "오후";
        }
        
    }
}
