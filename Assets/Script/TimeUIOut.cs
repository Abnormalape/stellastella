using System;
using Unity;
using UnityEngine;
using UnityEngine.UI;
class TimeUIOut : MonoBehaviour
{
    GameManager gameManager;
    string hourText;
    string minuteText;
    string AMPM;

    int weekNum;
    int day;
    string[] weekday = new string[] { "일", "월", "화", "수", "목", "금", "토" };

    private void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
    }

    private void Update()
    {
        HourMinuteText();
        DayWeekNumText();
    }

    void HourMinuteText()
    {
        if (gameManager.currentHour >= 13 && gameManager.currentHour <= 23)
        {
            hourText = (gameManager.currentHour-12).ToString();
        }
        else if (gameManager.currentHour >= 24)
        {
            hourText = $"0{(gameManager.currentHour - 24).ToString()}";
        }
        else if (gameManager.currentHour >= 10)
        {
            hourText = $"{(gameManager.currentHour).ToString()}";
        }
        else
        {
            hourText = $"0{(gameManager.currentHour).ToString()}";
        }

        if (gameManager.currentMinute == 0)
        {
            minuteText = "00";
        }
        else
        {
            minuteText = gameManager.currentMinute.ToString();
        }

        AMPM = gameManager.ampm;

        transform.Find("HourMinute").GetComponent<Text>().text = $"{hourText}:{minuteText} {AMPM}";
    }

    void DayWeekNumText()
    {
        string today;
        day = gameManager.currentDay;
        weekNum = day % 7; //1일때 월요일 0일때 일요일
        today = weekday[weekNum];

        transform.Find("DayWeekNum").GetComponent<Text>().text = $"{day},{today}";
    }
}

