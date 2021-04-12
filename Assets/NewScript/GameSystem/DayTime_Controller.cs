using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTime_Controller : MonoBehaviour
{
    public delegate void OnChangeUI();
    public OnChangeUI onChangeDay;
    public OnChangeUI onChangeTime;

    const float secondsInDay = 86400f;

    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color nightLightColor;
    [SerializeField] float timeScale = 60f;

    float time;
    public float Hours { get { return time / 3600f; } }
    public float Minutes { get { return time % 3600f / 60f; } }

    //[SerializeField] Text text;
    [SerializeField] Light2D globalLight;

    public WeekDay week = WeekDay.Monday;   // 요일
    public const int maxMonthDay = 28; // 28일 == 1달
    public Month month = Month.Spring;
    public int day = 1;
    
    void Update()
    {
        TimePass();
    }

    void TimePass()
    {
        time += Time.deltaTime * timeScale;

        int hour = (int)Hours;
        int min = (int)Minutes;

        //text.text = hour.ToString("00") + ":" + min.ToString("00");
        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if (time > secondsInDay)
        {
            NextDay();

            if(onChangeDay != null)
                onChangeDay.Invoke();
        }

        if (0 != ((int)Minutes % 5)) // 현재 분 != 5의배수 종료
            return;

        if (onChangeTime != null)   // 5분마다 UI 갱신
            onChangeTime.Invoke();
    }

    void NextDay()
    {
        time = 0;
        day++;

        week += 1;
        if ((int)week > (int)WeekDay.Sunday)
        {
            week = WeekDay.Monday;
        }

        if (day <= maxMonthDay)  // 29일 이전이면 무시
            return;

        month += 1;    // 다음달 변경
        if((int)month > (int)Month.Winter)  // 겨울 다음달은 봄
        {
            month = Month.Spring;
        }
    }

    public enum Month
    {
        Spring = 0,
        Summer = 1,
        Fall = 2,
        Winter = 3,
    }
    public enum WeekDay
    {
        Monday = 0,
        Tuesday = 1,
        Wednesday = 2,
        Thursday = 3,
        Friday = 4,
        Saturday = 5,
        Sunday = 6,
    }
}