using System.Collections;
using TMPro;
using UnityEngine;

public class Minuteur : MonoBehaviour
{
    private static bool _chronoOn;
    public static float _time;
    public static float _bestTime = 999999;
    private TextMeshProUGUI timerText;
    public Leaderboard leaderboard;

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
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        else
        {
            timerText.text = string.Format("Best Time: {0:00}:{1:00}", min, sec);
        }
    }

    public void startTimer()
    {
        _chronoOn = true;
    }

    // public void stopTimer()
    // {
    //     _chronoOn = false;
    //     if (_time < _bestTime)
    //     {
    //         _bestTime = _time;
            
    //         yield return leaderboard.SubmitScoreRoutine(System.Convert.ToInt32(_bestTime));

    //     }
    // }
    public IEnumerator stopTimer()
    {
        _chronoOn = false;
        if (_time < _bestTime)
        {
            _bestTime = _time;
            
            yield return leaderboard.SubmitScoreRoutine(System.Convert.ToInt32(_bestTime));

        }
    }
}
