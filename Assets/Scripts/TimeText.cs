using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

public class TimeText : MonoBehaviour
{
    public float fastForwardParticleSpeedCoFactor = 30;
    private int currentHour = 11; //By 24
    private int currentMinute = 17; //By 60
    private TextMeshProUGUI text;
    private BackgroundParticleSystem backgroundParticleSystem;
    
    private static TimeText instance;


    private void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
        backgroundParticleSystem = FindObjectOfType<BackgroundParticleSystem>();
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

        instance.backgroundParticleSystem.TurnBackToNormalSpeed();
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
        //instance.particleSystem.playbackSpeed = instance.fastForwardParticleSpeedCoFactor;

        instance.backgroundParticleSystem.OnFastForward();

        while(instance.currentHour != targetHour || instance.currentMinute != targetMinute)
        {
            IncreaseMinute();
            counter++;

            if(counter >= 2)
            {
                yield return new WaitForSeconds(0.016666f);
                counter = 0;
            }
        }

        instance.backgroundParticleSystem.TurnBackToNormalSpeed();
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
        backgroundParticleSystem.MultiplyPlaybackSpeed(6);

        float delta = 0.3f;
        float decay = 0.96f;

        while (true)
        {
            TimeText.IncreaseMinute();
            delta *= decay;
            backgroundParticleSystem.MultiplyPlaybackSpeed(1f / decay);
            //backgroundParticleSystem.playbackSpeed /= decay;
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
