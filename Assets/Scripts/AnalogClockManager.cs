using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DG.Tweening;
using System;

public class AnalogClockManager : MonoBehaviour
{
    public RectTransform secondHand;
    public RectTransform minuteHand;
    public RectTransform hourHand;

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
            RotateHands();
        }
    }

    void SetTimeFromTimeManager()
    {
        currentSeconds = timeManager.ReturnSeconds();
        currentMinutes = timeManager.ReturnMinutes();
        currentHour = timeManager.ReturnHour();

        SetSecondHand(currentSeconds);
        SetMinuteHand(currentMinutes);
        SetHourHand(currentHour);
    }

    void RotateHands()
    {
        currentSeconds += 1;

        if (currentSeconds == 60)
        {
            currentSeconds = 0;
            currentMinutes += 1;
            SetMinuteHand(currentMinutes);
        }

        if (currentMinutes == 60)
        {
            currentMinutes = 0;
            currentHour += 1;

            if (currentHour == 12)
            {
                currentHour = 0;
            }

            SetHourHand(currentHour);
        }

        SetSecondHand(currentSeconds);

    }

    void SetHourHand(int hour)
    {
        hourHand.DORotate(new Vector3(0, 0, hour * (360 / 12) * -1), 1);
    }

    void SetMinuteHand(int minutes)
    {
        minuteHand.DORotate(new Vector3(0, 0, minutes * 6 * -1), 1);
    }

    void SetSecondHand(int seconds)
    {
        secondHand.DORotate(new Vector3(0, 0, seconds * 6 * -1), 1);
    }
}
