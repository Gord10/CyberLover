using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeText : MonoBehaviour
{
    private int currentHour = 11; //By 24
    private int currentMinute = 17; //By 60
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        SetTimeInstant(11, 17);
        InvokeRepeating("IncreaseMinute", 0, 0.1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseMinute()
    {
        currentMinute++;
        if(currentMinute >= 60)
        {
            currentMinute %= 60;
            currentHour++;
            currentHour %= 24;
        }

        SetText();
    }

    //targetHour is by 24. We will determine whether the time is AM or PM based on targetHour
    public void SetTimeInstant(int targetHour, int targetMinute)
    {
        currentHour = targetHour;
        currentMinute = targetMinute;
        SetText();
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
