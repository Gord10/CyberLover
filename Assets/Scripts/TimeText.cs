using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class TimeText : MonoBehaviour
{
    private int currentHour = 11; //By 24
    private int currentMinute = 17; //By 60
    private TextMeshProUGUI text;
    private ParticleSystem particleSystem;
    
    private static TimeText instance;


    private void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
        particleSystem = FindObjectOfType<ParticleSystem>();
    }

    private void Awake()
    {
        instance = this;
        Init();
    }

    [YarnCommand("IncreaseMinute")]
    public static void IncreaseMinute()
    {
        instance.IncreaseMinuteInstance();
    }

    public void IncreaseMinuteInstance()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute %= 60;
            currentHour++;
            currentHour %= 24;
        }

        SetText();
    }

    [YarnCommand("SetTimeInstant")]
    public static void SetTimeInstant(int targetHour, int targetMinute)
    {
        if(!instance)
        {
            instance = FindObjectOfType<TimeText>();
            instance.Init();
        }

        instance.currentHour = targetHour; //targetHour is by 24. We will determine whether the time is AM or PM based on targetHour
        instance.currentMinute = targetMinute;
        instance.SetText();
    }

    //currentHour is by 24. We will determine whether the time is AM or PM based on currentHour
    private void SetText()
    {
        bool isAM = currentHour <= 12;
        int hourTextInt = currentHour % 12;
        if (hourTextInt == 0)
        {
            hourTextInt = 12;
        }

        string str = hourTextInt.ToString("00");

        str += ":";
        str += currentMinute.ToString("00");

        str += (isAM) ? " AM" : " PM";
        text.text = str;
        //print(text.text);
    }

    [YarnCommand("SetTime")]
    public static IEnumerator SetTime(int targetHour, int targetMinute)
    {
        int counter = 0;
        while(instance.currentHour != targetHour || instance.currentMinute != targetMinute)
        {
            IncreaseMinute();
            counter++;

            if(counter >= 3)
            {
                yield return new WaitForSeconds(0.016666f);
                counter = 0;
            }
        }
    }

    [YarnCommand("HideTime")]
    public static void Hide()
    {
        instance.text.enabled = false;
    }

    [YarnCommand("ShowTime")]
    public static void Show()
    {
        instance.text.enabled = true;
    }

    public static void StartAbyss()
    {
        instance.StartCoroutine(instance.StartAbyssInstance());
    }

    private IEnumerator StartAbyssInstance()
    {
        float delta = 0.3f;
        float decay = 0.95f;

        while (true)
        {
            TimeText.IncreaseMinute();
            delta *= decay;
            particleSystem.playbackSpeed /= decay;
            yield return new WaitForSecondsRealtime(delta);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public IEnumerator IncreaseMinutesLineerSpeedInfinitely(float delay)
    {
        while (true)
        {
            TimeText.IncreaseMinute();
            yield return new WaitForSecondsRealtime(delay);
        }
    }
}
