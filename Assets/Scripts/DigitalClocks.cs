using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitalClocks : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    public TimeManager timeManager;

    private int currentSeconds;
    private int currentMinutes;
    private int currentHour;

    private int nextUpdate = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetTimeFromTimeManager", 0.5f, 3600f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate += 1;
            UpdateTime();
        }
    }

    void SetTimeFromTimeManager()
    {
        currentSeconds = timeManager.ReturnSeconds();
        currentMinutes = timeManager.ReturnMinutes();
        currentHour = timeManager.ReturnHour();

        SetTimer(currentHour, currentMinutes, currentSeconds);
    }

    void UpdateTime()
    {
        currentSeconds += 1;

        if (currentSeconds == 60)
        {
            currentSeconds = 0;
            currentMinutes += 1;
        }

        if (currentMinutes == 60)
        {
            currentMinutes = 0;
            currentHour += 1;
        }

        if (currentHour == 24)
        {
            currentHour = 0;
        }
        
        SetTimer(currentHour, currentMinutes, currentSeconds);
    }

    void SetTimer(int hour, int minutes, int seconds)
    {
        string str_hour, str_minutes, str_seconds;

        if (seconds < 10)
        {
            str_seconds = seconds.ToString("D2");
        }
        else
        {
            str_seconds = seconds.ToString();
        }

        if (minutes < 10)
        {
            str_minutes = minutes.ToString("D2");
        }
        else
        {
            str_minutes = minutes.ToString();
        }

        if (hour < 10)
        {
            str_hour = hour.ToString("D2");
        }
        else
        {
            str_hour = hour.ToString();
        }

        timeText.text = str_hour + ":" + str_minutes + ":" + str_seconds;
    }
}
