using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int minute;
    private int hour;

    [SerializeField]
    private float minuteToRealtime = 1.5f; // 0.5 realtime seconds is equal to 1 minute in game time. This should be equal to 3 minutes;
    private float timer;

    private bool dayStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        minute = 0;
        hour = 7;
        timer = minuteToRealtime;
        dayStarted = false;
        UpdateTime();
    }

    private void OnEnable()
    {
        GameEvents.onCallPerson += StartDay;
    }

    private void OnDisable()
    {
        GameEvents.onCallPerson -= StartDay;
    }

    // Update is called once per frame
    void Update()
    {
        if (dayStarted)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0f)
        {
            minute++;
            if (minute >= 60)
            {
                hour++;
                minute = 0;
            }
            timer = minuteToRealtime;
            UpdateTime();
        }
    }

    void StartDay()
    {
        if (!dayStarted)
        {
            dayStarted = true;
        }
    }

    void UpdateTime()
    {
        GameEvents.onTimePassed?.Invoke(hour, minute);
    }
}
