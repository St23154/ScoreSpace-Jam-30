using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minuteur : MonoBehaviour
{
    public static bool _chronoOn;
    public static float _time;
    public static float _bestTime;
    public TextMeshProUGUI timerText;

    void Awake()
    {
        _bestTime = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.FindWithTag("TimerText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_chronoOn)
        {
            _time += Time.deltaTime;
            updateChrono(_time);
        }
        else
        {
            updateChrono(_bestTime);
        }
    }

    void updateChrono(float _chrono)
    {
        _chrono += 1;
        float min = Mathf.FloorToInt(_chrono / 60);
        float sec = Mathf.FloorToInt(_chrono % 60);

        timerText.text = string.Format("well done, your best time : {00} : {1:00}", min, sec);
    }

    public void startTimer()
    {
        _chronoOn = true;
    }

    public void stopTimer()
    {
        _chronoOn = false;
        _bestTime = _time;
    }
}
