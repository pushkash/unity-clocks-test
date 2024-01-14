using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private DateTimeOffset dateTime;
    private int currentSeconds;
    private int currentMinutes;
    private int currentHour;

    public RequestTime requestTime;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(GetTime());
    }

    void Start()
    {
        InvokeRepeating("GetTimeOffset", 3600f, 3600f);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetTime()
    {
        yield return StartCoroutine(requestTime.GetRequest(requestTime.ResponseCallback));
        dateTime = requestTime.dateTime;
        SetVariables(dateTime);
    }

    public void SetVariables(DateTimeOffset dateTime)
    {
        currentSeconds = dateTime.Second;
        currentMinutes = dateTime.Minute;
        currentHour = dateTime.Hour;
    }

    public int ReturnSeconds()
    {
        return currentSeconds;
    }

    public int ReturnMinutes()
    {
        return currentMinutes;
    }

    public int ReturnHour()
    {
        return currentHour;
    }
}
