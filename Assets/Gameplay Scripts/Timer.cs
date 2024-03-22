using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private bool isRunning;
    
    public GameObject spaceship;

    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime;
            string timerString = FormatTime(elapsedTime);
            timerText.text = timerString;

            if(spaceship == null)
            {
                PauseTimer();
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
        startTime = Time.time - GetTotalSeconds(timerText.text);
    }

    public void ResetTimer()
    {
        startTime = Time.time;
    }

    private float GetTotalSeconds(string timeString)
    {
        string[] timeParts = timeString.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);
        int milliseconds = int.Parse(timeParts[2]);

        return minutes * 60 + seconds + milliseconds / 1000f;
    }
}
