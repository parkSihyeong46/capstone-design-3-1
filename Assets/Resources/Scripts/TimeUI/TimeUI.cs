using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [SerializeField]
    private DayTime_Controller dayTime_Controller;

    private Transform hourHand;
    private Text weekText;
    private Text dayText;
    private Text curTimeText;
    private Text ampmText;

    private int hour;
    private int minute;
    private float rotateAngle;
    // 현재 시간 가져오는 코드 필요함
    private void Awake()
    {
        hourHand = transform.GetChild(0);
        weekText = transform.GetChild(1).GetComponent<Text>();
        dayText = transform.GetChild(2).GetComponent<Text>();
        curTimeText = transform.GetChild(3).GetComponent<Text>();
        ampmText = transform.GetChild(4).GetComponent<Text>();
    }

    private void OnEnable()
    {
        dayTime_Controller.onChangeDay += UpdateWeekText;
        dayTime_Controller.onChangeDay += UpdateDayText;

        dayTime_Controller.onChangeTime += UpdateHourHand;
        dayTime_Controller.onChangeTime += UpdateCurTimeText;
        dayTime_Controller.onChangeTime += UpdateAmPmText;
    }

    private void OnDisable()
    {
        dayTime_Controller.onChangeDay -= UpdateWeekText;
        dayTime_Controller.onChangeDay -= UpdateDayText;

        dayTime_Controller.onChangeTime -= UpdateHourHand;
        dayTime_Controller.onChangeTime -= UpdateCurTimeText;
        dayTime_Controller.onChangeTime -= UpdateAmPmText;
    }

    private void UpdateWeekText()
    {  
        switch ((int)dayTime_Controller.week)
        {
            case 0:
                weekText.text = "월.";
                break;
            case 1:
                weekText.text = "화.";
                break;
            case 2:
                weekText.text = "수.";
                break;
            case 3:
                weekText.text = "목.";
                break;
            case 4:
                weekText.text = "금.";
                break;
            case 5:
                weekText.text = "토.";
                break;
            case 6:
                weekText.text = "일.";
                break;
        }
    }
    private void UpdateDayText()
    {
        int day = dayTime_Controller.day;
        dayText.text = day.ToString();
    }
    private void UpdateCurTimeText()
    {
        int hour = (int)dayTime_Controller.Hours;
        int minute = (int)dayTime_Controller.Minutes;

        if (hour > 12)
            hour -= 12;

        if(minute < 10)
        {
            curTimeText.text = hour + ":" + "0" + minute;
        }
        else
        {
            curTimeText.text = hour + ":" + minute;
        }
    }
    private void UpdateAmPmText()
    {
        bool isAm = ((int)dayTime_Controller.Hours < 12) ? true : false;

        if (isAm)
        {
            ampmText.text = "오전";
        }
        else
        {
            ampmText.text = "오후";
        }   
    }
    private void UpdateHourHand()
    {
        hour = (int)dayTime_Controller.Hours;
        minute = (int)dayTime_Controller.Minutes;

        if (2 > hour)
            hour += 24;

        if (2 <= hour && 6 > hour)  // 2 ~ 5시
        {
            hourHand.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }

        rotateAngle = ((hour * 9f) + (minute * 0.15f)) - 54;
        hourHand.rotation = Quaternion.Euler(0, 0, 180 - rotateAngle);
    }
}
