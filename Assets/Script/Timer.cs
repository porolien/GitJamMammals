using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerText = null;
    private string TimerInString = "";

    private void Start()
    {
        TimerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        int TimerInSeconds = (int)Time.timeSinceLevelLoad;
        TimerInString = "" + (TimerInSeconds);
        TimerText.text = TimerInString;

    }
}
