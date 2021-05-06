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
    private const int EXHAUSTED_HOUR = 2;   // 탈진 시간
    private const int WAKEUP_HOUR = 6;   // 기상 시간

    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color nightLightColor;
    [SerializeField] float timeScale = 60f;
    [SerializeField] private GameObject optionTab;

    float time;
    public float Hours { get { return time / 3600f; } }
    public float Minutes { get { return time % 3600f / 60f; } }

    [SerializeField] Light2D globalLight;
    [SerializeField]
    SleepUI sleepUI;

    public WeekDay week = WeekDay.Monday;   // 요일
    public const int maxMonthDay = 28; // 28일 == 1달
    public Month month = Month.Spring;
    public int day = 1;
    public int year = 1;

    private void Start()
    {
        time = 3600f * WAKEUP_HOUR;     // 게임 시작 시간 6시로 초기화
    }

    void Update()
    {
        // 옵션, 상점 열려있지 않을 때, 애니메이션 시간이 아닐 경우 시간이 가도록 설정
        if (!(optionTab.activeSelf) &&     // 인벤토리 null ??
            !(GameManager.instance.isOpenShop) &&
            !(sleepUI.isPlayAnimation) &&
            !(GameManager.instance.isOpenTalkPanel))  
        {
            TimePass();
        }
    }

    void TimePass()
    {
        time += Time.deltaTime * timeScale;

        int hour = (int)Hours;
        int min = (int)Minutes;

        float v = nightTimeCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayLightColor, nightLightColor, v);
        globalLight.color = c;
        if (time > secondsInDay)
        {
            NextDay();

            if (onChangeDay != null)
                onChangeDay.Invoke();
        }

        if(hour == EXHAUSTED_HOUR)
        {
            Sleep();
        }

        if (0 != ((int)Minutes % 5)) // 현재 분 != 5의배수 종료
            return;

        if (onChangeTime != null)   // 5분마다 UI 갱신
            onChangeTime.Invoke();
    }

    void NextDay()
    {
        time = 0f;
        day++;

        week += 1;
        if ((int)week > (int)WeekDay.Sunday)
        {
            week = WeekDay.Monday;
        }

        if (day < maxMonthDay)  // 29일 이전이면 무시
            return;

        day = 0;
        month += 1;    // 다음달 변경
        if(Month.Winter == month)  // 겨울 다음달은 봄
        {
            year += 1;
            month = Month.Spring;
        }
    }

    public void Sleep()
    {
        // sleepUI.PlaySleepAnimation();  이거 키면 2시 될때 다음날로 넘어가는 애니메이션 실행됨
        time = 3600f * WAKEUP_HOUR;
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