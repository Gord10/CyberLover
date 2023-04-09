using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L0VerDialogueRunner : MonoBehaviour
{
    private TimeText timeText;

    private void Awake()
    {
        timeText = FindObjectOfType<TimeText>();
    }

    public void OnDialogueComplete()
    {
        print("On Dialogue Complete");
        if(timeText)
        {
            timeText.IncreaseMinuteInstance();
        }
    }
}
