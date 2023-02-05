using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerText = null;
    private string TimerInString = "";
    private int TheMinus;

    private void Start()
    {
        TimerText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        int TimerInSeconds = (int)Time.timeSinceLevelLoad - TheMinus;
        TimerInString = "" + (TimerInSeconds);
        TimerText.text = TimerInString;

    }
    public void TheTiming()
    {
        TheMinus = (int)Time.timeSinceLevelLoad;
    }
}
