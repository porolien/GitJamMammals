using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timing_Text : MonoBehaviour
{
    public Text text;
    public float time;
    private void Start()
    {
        text = GetComponent<Text>();
        time = Time.realtimeSinceStartup;
    }

}
