using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepUI : MonoBehaviour
{
    [SerializeField]
    private DayTime_Controller dayTime_Controller;
    private Text messageText;
    private Animator animator;

    public bool isPlayAnimation = false;

    private void Start()
    {
        messageText = transform.GetChild(3).GetChild(0).GetComponent<Text>();
        animator = transform.GetComponent<Animator>();
    }

    public void UpdateTimeText()
    {
        string month = "봄";

        switch(dayTime_Controller.month)
        {
            case DayTime_Controller.Month.Spring:
                month = "봄";
                break;
            case DayTime_Controller.Month.Summer:
                month = "여름";
                break;
            case DayTime_Controller.Month.Fall:
                month = "가을";
                break;
            case DayTime_Controller.Month.Winter:
                month = "겨울";
                break;
        }

        SetTimeText(dayTime_Controller.year, month, dayTime_Controller.day);
    }

    public void SetTimeText(int year, string season, int day)
    {
        messageText.text = year + "년째, " + season + "의 " + day + "일째";
    }

    public void PlaySleepAnimation()
    {
        animator.Play("FadeOut");
    }

    public void SetTrueIsPlayAnimation()
    {
        isPlayAnimation = true;
    }

    public void SetFalseIsPlayAnimation()
    {
        isPlayAnimation = false;
    }

}
