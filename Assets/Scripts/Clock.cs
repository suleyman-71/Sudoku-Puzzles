using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    int _hour = 0;
    int _minute = 0;
    int _seconds = 0;

    private TextMeshProUGUI _textClock;
    private float _deltaTime;
    private bool _stopClock = false;

    public static Clock instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }

        instance= this;

        _textClock= GetComponent<TextMeshProUGUI>();
        _deltaTime= 0;
    }
    void Start()
    {
        _stopClock= false;
    }

    
    void Update()
    {
        if (_stopClock == false)
        {
            _deltaTime += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(_deltaTime);

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            _textClock.text = hour + ":" + minute + ":" + seconds;
        }
    }

    string LeadingZero(int n)
    { 
        return n.ToString().PadLeft(2, '0');
    }

    public void OnGameOver()
    {
        _stopClock= true;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    public TextMeshProUGUI GetCurrentTimeText()
    {
        return _textClock;
    }
}
