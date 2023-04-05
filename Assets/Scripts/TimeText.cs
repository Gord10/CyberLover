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
    private static TimeText instance;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        instance = this;
        //InvokeRepeating("IncreaseMinute", 0, 0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    //targetHour is by 24. We will determine whether the time is AM or PM based on targetHour
    public static void SetTimeInstant(int targetHour, int targetMinute)
    {
        instance.currentHour = targetHour;
        instance.currentMinute = targetMinute;
        instance.SetText();
    }

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
    }
}
