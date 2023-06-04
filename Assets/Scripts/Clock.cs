using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Clock : MonoBehaviour
{
    private int hour_ = 0;
    private int minute_ = 0;
    private int second_ = 0;
    private Text textClock;
    float delta_time;
    private bool stop_clock_ = false;

    public static Clock instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;

        textClock = GetComponent<Text>();
        delta_time = 0;
    }

    void Start()
    {
        stop_clock_ = false;
    }

    void Update()
    {
        if (GameSettings.Instance.GetPaused() == false && stop_clock_ == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string second = LeadingZero(span.Seconds);

            textClock.text = hour + ":" + minute + ":" + second;
        }
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public void OnGameOver()
    {
        stop_clock_ = true;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    public static string GetCurrentTime()
    {
        return instance.delta_time.ToString();
    }

    public Text GetCurrentTimeText()
    {
        return textClock;
    }
}
