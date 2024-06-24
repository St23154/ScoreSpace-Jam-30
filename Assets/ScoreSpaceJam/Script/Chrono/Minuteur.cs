using System;
using System.Collections;
using Dan.Enums;
using TMPro;
using UnityEngine;

public class Minuteur : MonoBehaviour
{
    private static bool _chronoOn;
    public static float _time;
    public static float _bestTime = 999999;
    public static int tempTime;
    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GameObject.FindWithTag("TimerText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (_chronoOn)
        {
            _time += Time.deltaTime;
            UpdateChrono(_time);
        }
        else
        {
            UpdateChrono(_bestTime);
        }
    }

    void UpdateChrono(float chrono)
    {
        float min = Mathf.FloorToInt(chrono / 60);
        float sec = Mathf.FloorToInt(chrono % 60);
        if (_chronoOn)
        {
            //timerText.text = string.Format("{0:00}:{1:00}", min, sec);
            timerText.text = string.Format("{0:00}", chrono);

        }
        else
        {
            //timerText.text = string.Format("Best Time: {0:00}:{1:00}", min, sec);
            timerText.text = string.Format("{0:00}", chrono);
            
        }
    }

    public void startTimer()
    {
        _chronoOn = true;
    }

    public void stopTimer()
    {
        _chronoOn = false;
        tempTime = System.Convert.ToInt32(_time);
        if (_time < _bestTime)
        {
            _bestTime = _time;
            tempTime = System.Convert.ToInt32(_bestTime);

        }
    }

}