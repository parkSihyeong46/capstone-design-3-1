using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class DayTime_Controller : MonoBehaviour
{
    const float secondsInDay = 86400f;

    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color nightLightColor;
    [SerializeField] float timeScale = 60f;

    float time;
    float Hours { get { return time / 3600f; } }
    float Minutes { get { return time % 3600f / 60f; } }

    //[SerializeField] Text text;
    //[SerializeField] Light2D globalLight;
    private int day;

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
        //globalLight.color = c;
        if (time > secondsInDay)
        {
            NextDay();
        }
    }

    void NextDay()
    {
        time = 0;
        day++;
    }
}
